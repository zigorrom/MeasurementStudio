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
            int bufferSize = 400100;
            session.DefaultBufferSize = bufferSize;
            session.TerminationCharacterEnabled = true;
            session.TerminationCharacter = 0xA;
            
            session.Write("*RST");
            session.Write("ROUT:ENAB ON,(@101:104)");
            session.Write("ACQ:SRAT 500000");
            session.Write("ACQ:POIN 50000");
            session.Write("WAV:POIN 50000");

            session.Write("RUN");
            int counter = 0;
            int cycles = 10000;
            string status = string.Empty;
            string data = string.Empty;
            
            ushort[] array = new ushort[bufferSize];
            byte[] data_query = ASCIIEncoding.ASCII.GetBytes("WAV:DATA?"); //BinaryEncoding.RawLittleEndian
            while(counter<cycles)
            {
                session.Write("WAV:STAT?");
                status = session.ReadString();
                if (status == "DATA\n")
                {
                    data = session.Query("WAV:DATA?");
                    Console.WriteLine("data length {0}", data.Length);
                }
                Console.WriteLine(status);
                counter++;
            }
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
