using AgilentU2442A;
using NationalInstruments.VisaNS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgilentU2542Atest
{
    
    class Program
    {
        static void Main(string[] args)
        {
            MessageBasedSession session = new MessageBasedSession("ADC");
            var sample_rate = 500000;
            var points_per_sample = 50000;
            var nchan = 4;
            int bufferSize = points_per_sample*nchan+10;
            session.DefaultBufferSize = bufferSize;
            session.TerminationCharacterEnabled = true;
            session.TerminationCharacter = (byte)'\n';
            //BinaryEncoding.RawLittleEndia
            session.Write("*RST");
            session.Write("ROUT:ENAB ON,(@101:104)");
            session.Write(String.Format("ACQ:SRAT {0}",sample_rate));
            session.Write(String.Format("ACQ:POIN {0}",points_per_sample));
            session.Write(String.Format("WAV:POIN {0}",points_per_sample));

            session.Write("RUN");
            int counter = 0;
            int cycles = 10000;
            string status = string.Empty;
            //string data = string.Empty;
            //byte[] result = null;
            ushort[] array = null;
            //byte[] data_query = ASCIIEncoding.ASCII.GetBytes("WAV:DATA?"); //BinaryEncoding.RawLittleEndian

            var reader = new MessageBasedSessionReader(session);
            reader.BinaryEncoding = BinaryEncoding.RawLittleEndian;

            const int SAMPLE_NUMER = 50000*4;
            string dataQuery = "WAV:DATA?";
            while(counter++<cycles)
            {
                session.Write("WAV:STAT?");
                status = session.ReadString();

                if (status == "DATA\n")
                {
                    //data = session.Query(dataQuery);
                    session.Write(dataQuery);
                    array = reader.ReadUInt16s(SAMPLE_NUMER);

                    //Console.WriteLine("data length {0}", array.Length);
                    //Console.WriteLine(status);
                }
                else if(status == "OVER\n")
                {
                    Console.WriteLine(status);
                    break;

                }

                Console.WriteLine(counter);
            }
            Console.WriteLine("Done");
            session.Write("STOP");
            Console.ReadKey();
            //var agilent = new AgilentU2542A("asdasd", "", "ADC");
            //var ch1 = agilent.GetAnalogInputChannel(ChannelEnum.AI_CH101);
            //ch1.ChannelEnable = ChannelEnableEnum.Enabled;

            //ch1.SampleRate = 500000;
            //ch1.PointsPerShot = 50000;
            //ch1.DataSetReady += ch1_DataSetReady;
            //Console.WriteLine(agilent.Query(agilent.CommandSet.IDNQuery()));
            //ch1.StartAcquisition();
            //System.Threading.Thread.Sleep(100000);
            //ch1.StopAcquisition();
            //for (int i = 0; i < 0xFFFFFFFE; i++)
            //{
            //ss
            //}
        }
        static long counter = 0;
        static void ch1_DataSetReady(object sender, EventArgs e)
        {
            var a = (AgilentU2442A.AnalogInputChannel)sender;
            double[] data;
            a.DequeueData(out data);
            counter++;
            Console.WriteLine("b{0},d{1};", counter, data.Length);
        }
    }
}
