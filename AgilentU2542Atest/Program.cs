using AgilentU2442A;
//using NationalInstruments.VisaNS;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;



//using agilent = Agilent.AgilentU254x.Interop;
using Ivi.Visa.Interop;


namespace AgilentU2542Atest
{
    
    class Program
    {
        #region
        //static void Main(string[] args)
        //{
        //    var dev = new agilent.AgilentU254xClass();
        //    string options = "Simulate=false, Cache=false, QueryInstrStatus=true";
        //    dev.Initialize("ADC", true, true, options);

            

        //    //var status = dev.Status;
        //    var analogIn = dev.AnalogIn;
        //    var acqusition = dev.AnalogIn.Acquisition;
        //    var channels = analogIn.Channels;

        //    Console.WriteLine("Data alignment {0}, resolution {1}, type {2}", analogIn.DataAlignment, analogIn.DataResolution, analogIn.DataType);

        //    for (int i = 1; i <= analogIn.Channels.Count; i++)
        //    {
        //        var name = channels.get_Name(i);
        //        Console.WriteLine("channel {0}, name {1}", i, name);
        //        var channel = channels.get_Item(name);
        //        channel.Configure(agilent.AgilentU254xAnalogPolarityEnum.AgilentU254xAnalogPolarityBipolar, 10.0, true);

        //    }

        //    var sample_rate = 500000;
        //    var points = 50000;

        //    analogIn.MultiScan.Configure(sample_rate, -1);

        //    Console.WriteLine(analogIn.MultiScan.TimePerScan);
        //    Console.WriteLine(acqusition.BufferSize);

        //    acqusition.BufferSize = points;
        //    Console.WriteLine(acqusition.BufferSize);
        //    //GC.AddMemoryPressure( sizeof(short) * sample_rate);
        //    //short[] data = new short[points];
        //    //var data = new double[points];
        //    var nBytes = points * sizeof(double);

        //    //var d = new double[points];
        //    //[MarshalByRefObject]
        //    //double[] data = {0.0};
        //    double[] data = new double[points];

        //    try
        //    {
        //        acqusition.Start();
        //        for (int i = 0; i < 10000; )
        //        {

        //            var status = acqusition.BufferStatus;
        //            switch (status)
        //            {
        //                case Agilent.AgilentU254x.Interop.AgilentU254xBufferStatusEnum.AgilentU254xBufferStatusDataReady:
        //                    {
        //                        #region test structures commented
        //                        //https://stackoverflow.com/questions/537573/how-to-get-intptr-from-byte-in-c-sharp
        //                        //https://stackoverflow.com/questions/31854195/how-to-convert-the-intptr-to-an-array
        //                        //acqusition.Fetch(ref data);
        //                        //array[0] = (demo)System.Runtime.InteropServices.Marshal.PtrToStructure(PPtr, typeof(demo));
        //                        //IntPtr unmanagedPtr = Marshal.AllocHGlobal(nBytes);
        //                        //data = (double[])System.Runtime.InteropServices.Marshal.PtrToStructure(unmanagedPtr, typeof(double[]));
        //                        #endregion

        //                        try
        //                        {
        //                            acqusition.FetchScale(data);
        //                        }
        //                        catch(Exception e)
        //                        {
        //                            GC.Collect();
        //                        }


        //                        //dev.DirectIO
        //                        //Marshal.FreeHGlobal(unmanagedPtr);

        //                        //System.Runtime.InteropServices.Marshal.Release(data);
        //                        //dev.Clear();
        //                        i++;
        //                        Console.WriteLine("i={0}", i);

        //                        //data = null;

        //                        //GC.Collect();

        //                    }
        //                    break;
        //                case Agilent.AgilentU254x.Interop.AgilentU254xBufferStatusEnum.AgilentU254xBufferStatusEmpty:
        //                    break;
        //                case Agilent.AgilentU254x.Interop.AgilentU254xBufferStatusEnum.AgilentU254xBufferStatusFragment:
        //                    break;
        //                case Agilent.AgilentU254x.Interop.AgilentU254xBufferStatusEnum.AgilentU254xBufferStatusOverRun:
        //                    break;
        //                default:
        //                    break;
        //            }

        //            Console.WriteLine(status);
                    
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        acqusition.Stop();
        //        Console.WriteLine(e);
        //        Console.WriteLine("extracting error...");
        //        int errCode = 999;
        //        string errMsg = null;
        //        while (errCode != 0)
        //        {
        //            dev.Utility.ErrorQuery(ref errCode, ref errMsg);
        //            Console.WriteLine("ErrorQuery: {0}, {1}", errCode, errMsg);
        //        }

        //    }
        //    finally
        //    {
        //        acqusition.Stop();
        //    }



        //    //Console.CancelKeyPress += Console_CancelKeyPress;
        //    System.Threading.Thread.Sleep(10000);
        //}


        //static void Main(string[] args)
        //{

            

        //    var dev = new agilent.AgilentU254x();

        //    dev.Initialize("ADC", true, true);
        //    //var status = dev.Status;
        //    var analogIn = dev.AnalogIn;
        //    var acqusition = dev.AnalogIn.Acquisition;
        //    var channels = analogIn.Channels;

        //    Console.WriteLine("Data alignment {0}, resolution {1}, type {2}", analogIn.DataAlignment, analogIn.DataResolution, analogIn.DataType);

        //    for (int i = 1; i <= analogIn.Channels.Count; i++)
        //    {
        //        var name = channels.get_Name(i);
        //        Console.WriteLine("channel {0}, name {1}", i, name);
        //        var channel = channels.get_Item(name);
        //        channel.Configure(agilent.AgilentU254xAnalogPolarityEnum.AgilentU254xAnalogPolarityBipolar, 10.0, true);

        //    }

        //    var sample_rate = 500000;
        //    var points = 50000;

        //    analogIn.MultiScan.Configure(sample_rate, -1);

        //    Console.WriteLine(analogIn.MultiScan.TimePerScan);
        //    Console.WriteLine(acqusition.BufferSize);

        //    var buffer_size = 102400; ///4 * points;

        //    acqusition.BufferSize = buffer_size;
        //    Console.WriteLine(acqusition.BufferSize);
        //    //GC.AddMemoryPressure( sizeof(short) * sample_rate);
        //    //short[] data = new short[points];
        //    var data = new double[buffer_size];
        //    //string data;

        //    //var d = new double[points];
        //    //double[] data 
        //    //dev.DriverOperation.Cache = false;
        //    //dev.DriverOperation.Simulate = true;
        //    dev.System.DirectIO.SetBufferSize(BufferMask.IO_IN_BUF, buffer_size);
        //    try
        //    {
        //        acqusition.Start();
        //        for (int i = 0; i < 10000; )
        //        {

        //            var status = acqusition.BufferStatus;
        //            switch (status)
        //            {
        //                case Agilent.AgilentU254x.Interop.AgilentU254xBufferStatusEnum.AgilentU254xBufferStatusDataReady:
        //                    {

        //                        acqusition.FetchScale(ref data);
                                
        //                        //data = dev.System.DirectIO.IO.ReadString(buffer_size);
        //                        Console.WriteLine("first element {0}", data[0]);
        //                        //i++;

        //                        //Marshal.re;

        //                        //Console.WriteLine("i={0}", i);

        //                        Console.WriteLine(status);
        //                    }
        //                    break;
        //                case Agilent.AgilentU254x.Interop.AgilentU254xBufferStatusEnum.AgilentU254xBufferStatusEmpty:
        //                    break;
        //                case Agilent.AgilentU254x.Interop.AgilentU254xBufferStatusEnum.AgilentU254xBufferStatusFragment:
        //                    break;
        //                case Agilent.AgilentU254x.Interop.AgilentU254xBufferStatusEnum.AgilentU254xBufferStatusOverRun:
        //                    break;
        //                default:
        //                    break;
        //            }

                    
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e);

        //    }
        //    finally
        //    {
        //        acqusition.Stop();
        //    }



            
        //}

        #endregion




        static string FormatCommand(string command)
        {
            return FormatCommand(command, CultureInfo.CurrentCulture);
        }

        static string FormatCommand(string command, CultureInfo info)
        {
            //return String.Format(info, "{0}\n", command.TrimEnd('\n'));
            return command.TrimEnd('\n');
        }

       

        /// <summary>
        /// GPIB implementation
        /// </summary>
        /// <param name="args"></param>
        //static void Main(string[] args)
        //{
        //    var currentInfo = CultureInfo.CreateSpecificCulture("en-US");
        //    currentInfo.NumberFormat = new NumberFormatInfo() { NumberDecimalSeparator = ".", NumberGroupSeparator = "" };
        //    var session = (UsbSession)ResourceManager.GetLocalManager().Open("ADC", AccessModes.ExclusiveLock, 1000);
        //    //var srq = MessageBasedSessionEventType.ServiceRequest;
        //    //session.EnableEvent(srq, EventMechanism.Queue);
        //    System.Console.WriteLine("is 488.2 compliant: {0}", session.Is4882Compliant);
        //    //UsbSession s = (UsbSession)session;
            
        //    Console.WriteLine("Protocol used: {0}", session.IOProtocol);
            

        //    session.Clear();
        //    session.LockResource();

        //    var sample_rate = 500000;
        //    var points_per_sample = 50000;
        //    var nchan = 1;

        //    const int header_size = 10;


        //    int ByteBufferSize = 10000000; //points_per_sample * 2 * nchan + header_size;

        //    ///Critical section: the device buffer should fit the arriving data!!!
        //    session.DefaultBufferSize = ByteBufferSize;
        //    //session.TerminationCharacter = 0x0A;
        //    //session.TerminationCharacterEnabled = true;
        //    //session.TerminationCharacterEnabled = false;
        //    //session.Timeout = 25000;

        //    string ResetCommand = FormatCommand("*RST");
        //    string ClearStatusCommand = FormatCommand("*CLS");
        //    string DisableAllChannels = FormatCommand("ROUT:ENAB OFF, (@101:104);");
        //    string EnableAllChannels = FormatCommand("ROUT:ENAB ON, (@101);"); //:104)");
        //    string SetSampleRateCommand = FormatCommand(String.Format("ACQ:SRAT {0};", sample_rate));
        //    string SetSingleShotPoints = FormatCommand(String.Format("ACQ:POIN {0};", points_per_sample));
        //    string SetContinuosAcqusitionPoints = FormatCommand(String.Format("WAV:POIN {0};", points_per_sample));

        //    string TriggerSourceCommand = FormatCommand("TRIG:SOUR NONE;");
        //    string TrigerPositionCommand = FormatCommand("");



        //    string RunCommand = FormatCommand("RUN;");
        //    string StopCommand = FormatCommand("STOP;");

        //    string QueryStatusCommand = FormatCommand("WAV:STAT?;");
        //    string QueryDataCommand = FormatCommand("WAV:DATA?;");

        //    const string DataArrivedStatus = "DATA\n";
        //    const string DataOverloadStatus = "OVER\n";
        //    const int StatusMessageLength = 5;


        //    session.Write(ResetCommand);
        //    session.Write(ClearStatusCommand);
        //    session.Write(DisableAllChannels);
        //    session.Write(EnableAllChannels);
        //    session.Write(SetSampleRateCommand);
        //    session.Write(SetSingleShotPoints);
        //    session.Write(SetContinuosAcqusitionPoints);


        //    int counter = 0;
        //    int minute = 60;


        //    int cycles = sample_rate * 30 * minute;
        //    byte[] status_bytes;
        //    string status = string.Empty;

        //    byte[] block_header;
        //    int block_length = 0;

        //    byte[] data;
        //    //string data = string.Empty;
        //    string header = string.Empty;
        //    //byte[] array = null;




        //    try
        //    {
        //        session.Write(RunCommand);
        //        while (counter++ < cycles)
        //        {
        //            session.Write(QueryStatusCommand);
        //            status_bytes = session.ReadByteArray(); //StatusMessageLength);
        //            status = Encoding.ASCII.GetString(status_bytes);
        //            //status = session.ReadString();
        //            //Console.WriteLine(status);

        //            switch (status)
        //            {
        //                case DataArrivedStatus:
        //                    {
        //                        session.Write(QueryDataCommand);
        //                        //data = session.ReadString();
        //                        block_header = session.ReadByteArray(header_size);
        //                        block_length = BitConverter.ToInt32(block_header, 2);

        //                        data = session.ReadByteArray(block_length);
        //                        //array = session.ReadByteArray();
        //                        header = Encoding.ASCII.GetString(block_header);

        //                        //header = data.Substring(0, header_size);
        //                        //header = Encoding.ASCII.GetString(data, 0, header_size);
        //                        //Console.WriteLine(counter);
        //                        Console.WriteLine("status {0}, counter {1}, length {2}, header {3}\n ", status.TrimEnd('\n'), counter, data.Length, header);//, data.Substring(0,header_size));

        //                    } break;
        //                case DataOverloadStatus:
        //                    {
                                
        //                        //session.EnableEvent(MessageBasedSessionEventType.ServiceRequest, EventMechanism.Queue);

        //                        throw new OutOfMemoryException("buffer overload on the device side");
                                
        //                    }

        //            }
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e);

        //        //Console.WriteLine("status byte {0}", session.ReadStatusByte());
        //        Console.WriteLine();
        //        Console.WriteLine("status before exception: {0}", status);
        //        session.Write(StopCommand);
        //    }
        //    finally
        //    {
        //        session.Clear();
        //        session.UnlockResource();
        //        session.Dispose();
        //    }

        //    Console.ReadKey();
        //}


        /// <summary>
        /// IVI implementation
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            var currentInfo = CultureInfo.CreateSpecificCulture("en-US");
            currentInfo.NumberFormat = new NumberFormatInfo() { NumberDecimalSeparator = ".", NumberGroupSeparator = "" };
            var resMgr = new ResourceManager();
            var session = (IMessage)resMgr.Open("ADC");

            var ioobj = new FormattedIO488();
            ioobj.IO = session;
            
            //var session = (MessageBasedSession)ResourceManager.GetLocalManager().Open("ADC", AccessModes.ExclusiveLock, 1000);
            //UsbSession s = (UsbSession)session;


            //session.IOProtocol = IOProtocol.HS488;
            //ioobj.FlushWrite();
            //ioobj.FlushRead();

            //session.Clear();
            //session.LockResource();

            var sample_rate = 500000;
            var points_per_sample = 50000;
            var nchan = 4;

            const int header_size = 10;
            //int ByteBufferSize = points_per_sample * 2 * nchan + header_size;

            //ioobj.SetBufferSize(BufferMask.IO_IN_BUF, ByteBufferSize);


            ///Critical section: the device buffer should fit the arriving data!!!
            //session.DefaultBufferSize = ByteBufferSize;
            //session.TerminationCharacter = 0x0A;
            //session.TerminationCharacterEnabled = true;
            //session.Timeout = -1;

            string ResetCommand = FormatCommand("*RST");
            string ClearStatusCommand = FormatCommand("*CLS");
            string EnableAllChannels = FormatCommand("ROUT:ENAB ON, (@101:104)");
            string SetSampleRateCommand = FormatCommand(String.Format("ACQ:SRAT {0}", sample_rate));
            string SetSingleShotPoints = FormatCommand(String.Format("ACQ:POIN {0}", points_per_sample));
            string SetContinuosAcqusitionPoints = FormatCommand(String.Format("WAV:POIN {0}", points_per_sample));

            string RunCommand = FormatCommand("RUN");
            string StopCommand = FormatCommand("STOP");

            string QueryStatusCommand = FormatCommand("WAV:STAT?");
            string QueryDataCommand = FormatCommand("WAV:DATA?");

            const string DataArrivedStatus = "DATA\n";
            const string DataOverloadStatus = "OVER\n";


            ioobj.WriteString(ResetCommand);
            ioobj.WriteString(ClearStatusCommand);
            ioobj.WriteString(EnableAllChannels);
            ioobj.WriteString(SetSampleRateCommand);
            ioobj.WriteString(SetSingleShotPoints);
            ioobj.WriteString(SetContinuosAcqusitionPoints);


            int counter = 0;
            int minute = 60;


            int cycles = sample_rate * 30 * minute;
            string status = string.Empty;
            //byte[] data;
            string data = string.Empty;
            string header = string.Empty;
            //byte[] array = null;




            try
            {
                ioobj.WriteString(RunCommand);

                while (counter++ < cycles)
                {
                    ioobj.WriteString(QueryStatusCommand);
                    status = ioobj.ReadString();


                    switch (status)
                    {
                        case DataArrivedStatus:
                            {
                                ioobj.WriteString(QueryDataCommand);
                                data = ioobj.ReadString();
                                //data = session.ReadString();
                                //data = session.ReadByteArray();
                                //array = session.ReadByteArray();
                                //header = Encoding.ASCII.GetString(array, 0, header_size);
                                
                                header = data.Substring(0, header_size);
                                //header = Encoding.ASCII.GetString(data, 0, header_size);
                                //Console.WriteLine(counter);
                                Console.WriteLine("status {0}, counter {1}, length {2}, header {3}\n ", status.TrimEnd('\n'), counter, data.Length, header);//, data.Substring(0,header_size));

                            } break;
                        case DataOverloadStatus:
                            {
                                throw new OutOfMemoryException("buffer overload on the device side");
                            }

                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                ioobj.WriteString(StopCommand);
                //session.Write(StopCommand);
            }
            finally
            {
                session.Close();
                //session.UnlockResource();
                //session.Dispose();
            }
        }




        /// <summary>
        /// IVI base implementation
        /// </summary>
        /// <param name="args"></param>
        //static void Main(string[] args)
        //{
        //    var currentInfo = CultureInfo.CreateSpecificCulture("en-US");
        //    currentInfo.NumberFormat = new NumberFormatInfo() { NumberDecimalSeparator = ".", NumberGroupSeparator = "" };
        //    //var resMgr = new ResourceManager();
        //    //var session = (IMessage)resMgr.Open("ADC", AccessMode.EXCLUSIVE_LOCK);
        //    int session;
        //    int result = 0;

        //    result = visa32.viOpenDefaultRM(out session);

        //    if (result == visa32.VI_SUCCESS)
        //    {
        //        int wg;
                
        //        int resWG = visa32.viOpen(session, "ADC", visa32.VI_NULL, visa32.VI_NULL, out wg);

        //        if(resWG == visa32.VI_SUCCESS)
        //        {
        //            Console.WriteLine("success");
        //            string str = string.Empty;
        //            visa32.viPrintf(wg, "*IDN?\n");
        //            visa32.viRead(wg, out str, 200);
        //            Console.WriteLine("IDN: {0}", str);

        //        }

        //        if (resWG == visa32.VI_SUCCESS)
        //        {
        //            visa32.viClose(resWG);
        //        }
        //    }

        //    return;


        //    //var ioobj = new FormattedIO488();
        //    //ioobj.IO = session;
        //    //ioobj.FlushRead();
        //    //var session = (MessageBasedSession)ResourceManager.GetLocalManager().Open("ADC", AccessModes.ExclusiveLock, 1000);
        //    //UsbSession s = (UsbSession)session;


        //    //session.IOProtocol = IOProtocol.HS488;
        //    //ioobj.FlushWrite();
        //    //ioobj.FlushRead();

        //    //session.Clear();
        //    //session.LockResource();

        //    var sample_rate = 500000;
        //    var points_per_sample = 50000;
        //    var nchan = 4;

        //    const int header_size = 10;
        //    int ByteBufferSize = points_per_sample * 2 * nchan + header_size;

        //    //ioobj.SetBufferSize(BufferMask.IO_IN_BUF, ByteBufferSize);


        //    ///Critical section: the device buffer should fit the arriving data!!!
        //    //session.DefaultBufferSize = ByteBufferSize;
        //    //session.TerminationCharacter = 0x0A;
        //    //session.TerminationCharacterEnabled = true;
        //    //session.Timeout = -1;

        //    string ResetCommand = FormatCommand("*RST");
        //    string ClearStatusCommand = FormatCommand("*CLS");
        //    string EnableAllChannels = FormatCommand("ROUT:ENAB ON, (@101:104)");
        //    string SetSampleRateCommand = FormatCommand(String.Format("ACQ:SRAT {0}", sample_rate));
        //    string SetSingleShotPoints = FormatCommand(String.Format("ACQ:POIN {0}", points_per_sample));
        //    string SetContinuosAcqusitionPoints = FormatCommand(String.Format("WAV:POIN {0}", points_per_sample));

        //    string RunCommand = FormatCommand("RUN");
        //    string StopCommand = FormatCommand("STOP");

        //    string QueryStatusCommand = FormatCommand("WAV:STAT?");
        //    string QueryDataCommand = FormatCommand("WAV:DATA?");

        //    const string DataArrivedStatus = "DATA\n";
        //    const string DataOverloadStatus = "OVER\n";


           


        //    int counter = 0;
        //    int minute = 60;


        //    int cycles = sample_rate * 30 * minute;
        //    //string status = string.Empty;
        //    //byte[] data;
        //    string data = string.Empty;
        //    string header = string.Empty;
        //    //byte[] array = null;




            
        //}

    }
}
    
