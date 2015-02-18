using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net.Sockets;

namespace Devices
{
    class TelnetConnection : IDisposable
    {
        #region TelnetConnection settings

        TcpClient m_Client;
        NetworkStream m_Stream;
        bool m_IsOpen = false;
        string m_Hostname;
        int m_ReadTimeout = 1000; // ms
        
        public delegate void ConnectionDelegate();
        public event ConnectionDelegate Opened;
        public event ConnectionDelegate Closed;
        public bool IsOpen { get { return m_IsOpen; } }

        #endregion

        #region Constructor / Destructor

        public TelnetConnection() { }
        public TelnetConnection(bool open) 
            : this("localhost", true) { }
        public TelnetConnection(string host, bool open)
        {
            if (open)
                Open(host);
        }

        ~TelnetConnection()
        {
            Dispose();
        }

        #endregion

        #region Functionality

        private void CheckOpen()
        {
            if (!IsOpen)
                throw new Exception("Connection not open.");
        }

        public string Hostname
        {
            get { return m_Hostname; }
        }

        public int ReadTimeout
        {
            get { return m_ReadTimeout; }
            set 
            {
                m_ReadTimeout = value;
                if (IsOpen) 
                    m_Stream.ReadTimeout = value; 
            }
        }

        public void Write(string str)
        {
            CheckOpen();
            byte[] bytes = System.Text.ASCIIEncoding.ASCII.GetBytes(str);
            m_Stream.Write(bytes, 0, bytes.Length);
            m_Stream.Flush();
        }

        public void WriteLine(string str)
        {
            CheckOpen();
            byte[] bytes = System.Text.ASCIIEncoding.ASCII.GetBytes(str);
            m_Stream.Write(bytes, 0, bytes.Length);
            WriteTerminator();
        }

        private void WriteTerminator()
        {
            byte[] bytes = System.Text.ASCIIEncoding.ASCII.GetBytes("\r\n\0");
            m_Stream.Write(bytes, 0, bytes.Length);
            m_Stream.Flush();
        }

        public string Read()
        {
            CheckOpen();
            return System.Text.ASCIIEncoding.ASCII.GetString(ReadBytes());
        }

        /// <summary>
        /// Reads bytes from the socket and returns them as a byte[].
        /// </summary>
        /// <returns></returns>
        public byte[] ReadBytes()
        {
            int i = m_Stream.ReadByte();
            byte b = (byte)i;
            int bytesToRead = 0;
            var bytes = new List<byte>();
            if ((char)b == '#')
            {
                bytesToRead = ReadLengthHeader();
                if (bytesToRead > 0)
                {
                    i = m_Stream.ReadByte();
                    if ((char)i != '\n') // discard carriage return after length header.
                        bytes.Add((byte)i);
                }
            }
            if (bytesToRead == 0)
            {
                while (i != -1 && b != (byte)'\n')
                {
                    bytes.Add(b);
                    i = m_Stream.ReadByte();
                    b = (byte)i;
                }
            }
            else
            {
                int bytesRead = 0;
                while (bytesRead < bytesToRead && i != -1)
                {
                    i = m_Stream.ReadByte();
                    if (i != -1)
                    {
                        bytesRead++;
                        // record all bytes except \n if it is the last char.
                        if (bytesRead < bytesToRead || (char)i != '\n')
                            bytes.Add((byte)i);
                    }
                }
            }
            return bytes.ToArray();
        }

        private int ReadLengthHeader()
        {
            int numDigits = Convert.ToInt32(new string(new char[] { (char)m_Stream.ReadByte() }));
            string bytes = "";
            for (int i = 0; i < numDigits; ++i)
                bytes = bytes + (char)m_Stream.ReadByte();
            return Convert.ToInt32(bytes);
        }

        public void Open(string hostname)
        {
            if (IsOpen)
                Close();
            m_Hostname = hostname;
            m_Client = new TcpClient(hostname, 5025);
            m_Stream = m_Client.GetStream();
            m_Stream.ReadTimeout = ReadTimeout;
            m_IsOpen = true;
            if (Opened != null)
                Opened();
        }

        public void Open(string hostname, int port)
        {
            if (IsOpen)
                Close();
            m_Hostname = hostname;
            m_Client = new TcpClient(hostname, port);
            m_Stream = m_Client.GetStream();
            m_Stream.ReadTimeout = ReadTimeout;
            m_IsOpen = true;
            if (Opened != null)
                Opened();
        }

        public void Close()
        {
            if (!m_IsOpen)
                return;
            m_Stream.Close();
            m_Client.Close();
            m_IsOpen = false;
            if (Closed != null)
                Closed();
        }

        #endregion

        #region Correctly disposing the instance

        public void Dispose()
        {
            Close();
        }

        #endregion
    }

    public class LAN_Device : IExperimentalDevice, IDisposable
    {
        #region LAN settings

        private string _HostName;
        public string HostName { get { return _HostName; } }
        private int _Port;
        public int Port { get { return _Port; } }

        TelnetConnection _TheConnection;

        #endregion

        #region Constructor / Destructor

        public LAN_Device(string __HostName, int __Port)
        {
            _HostName = __HostName;
            _Port = __Port;
            _TheConnection = new TelnetConnection();
            InitDevice();
        }

        ~LAN_Device()
        {
            Dispose();
        }

        #endregion

        #region IExperimentalDevice implementation

        public bool InitDevice()
        {
            try
            {
                if (!_TheConnection.IsOpen)
                    _TheConnection.Open(_HostName, _Port);

                if (_TheConnection.IsOpen)
                    return true;
                else
                    return false;
            }
            catch { return false; }
        }

        public bool SendCommandRequest(string RequestString)
        {
            try
            {
                _TheConnection.WriteLine(RequestString);
                return true;
            }
            catch { return false; }
        }

        public string ReceiveDeviceAnswer()
        {
            var LAN_DeviceResponce = string.Empty;

            try { LAN_DeviceResponce = _TheConnection.Read(); }

            catch
            {
                LAN_DeviceResponce = string.Empty;
            }

            return LAN_DeviceResponce;
        }

        public string RequestQuery(string Query)
        {
            SendCommandRequest(Query);
            return ReceiveDeviceAnswer();
        }

        #endregion

        #region Correctly disposing the instance

        public void Dispose()
        {
            _TheConnection.Dispose();
        }

        #endregion
    }
}
