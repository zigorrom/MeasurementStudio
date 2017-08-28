using AgilentU2442A;
using NationalInstruments.VisaNS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using agilent = Agilent.AgilentU254x.Interop;

namespace AgilentU2542Atest
{
    
    class Program
    {
        


        static void Main(string[] args)
        {
            var dev = new agilent.AgilentU254x();
            
            dev.Initialize("ADC", true, true);
            //var status = dev.Status;
            var analogIn = dev.AnalogIn;
            var acqusition = dev.AnalogIn.Acquisition;
            var channels = analogIn.Channels;

            Console.WriteLine("Data alignment {0}, resolution {1}, type {2}", analogIn.DataAlignment, analogIn.DataResolution, analogIn.DataType);

            for (int i = 1; i <= analogIn.Channels.Count; i++)
			{
                var name = channels.get_Name(i);
                Console.WriteLine("channel {0}, name {1}", i,name);
                var channel = channels.get_Item(name);
                channel.Configure(agilent.AgilentU254xAnalogPolarityEnum.AgilentU254xAnalogPolarityBipolar, 10.0, true);
                
            }

            var sample_rate = 500000;
            var points = 50000;

            analogIn.MultiScan.Configure(sample_rate, -1);
            
            Console.WriteLine(analogIn.MultiScan.TimePerScan);
            Console.WriteLine(acqusition.BufferSize);

            acqusition.BufferSize = points;
            Console.WriteLine(acqusition.BufferSize);
            //GC.AddMemoryPressure( sizeof(short) * sample_rate);
            //short[] data = new short[points];
            var data = new double[points];
            var d = new double[points];
            try
            {
                acqusition.Start();
                for (int i = 0; i < 10000; )
                {
                    
                    var status = acqusition.BufferStatus;
                    switch (status)
                    {
                        case Agilent.AgilentU254x.Interop.AgilentU254xBufferStatusEnum.AgilentU254xBufferStatusDataReady:
                            {
                                //acqusition.Fetch(ref data);
                                acqusition.FetchScale(ref data);
                                i++;
                                Console.WriteLine("i={0}",i);
                                
                                data = null;
                               
                                //GC.Collect();
                                   
                            }
                            break;
                        case Agilent.AgilentU254x.Interop.AgilentU254xBufferStatusEnum.AgilentU254xBufferStatusEmpty:
                            break;
                        case Agilent.AgilentU254x.Interop.AgilentU254xBufferStatusEnum.AgilentU254xBufferStatusFragment:
                            break;
                        case Agilent.AgilentU254x.Interop.AgilentU254xBufferStatusEnum.AgilentU254xBufferStatusOverRun:
                            break;
                        default:
                            break;
                    }
                    
                    Console.WriteLine(status);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                acqusition.Stop();
            }

            

            Console.CancelKeyPress += Console_CancelKeyPress;
            System.Threading.Thread.Sleep(10000);
        }

       

        static void Console_CancelKeyPress(object sender, ConsoleCancelEventArgs e)
        {
            throw new NotImplementedException();
        }

        //static void Main(string[] args)
        //{
        //    //MessageBasedSession session = new MessageBasedSession("ADC");
        //    var session = (MessageBasedSession)ResourceManager.GetLocalManager().Open("ADC");
        //    session.Clear();
        //    var sample_rate = 500000;
        //    var points_per_sample = 50000;
        //    var nchan = 4;

        //    const int header_size = 20;

        //    int bufferSize = (points_per_sample + header_size) * nchan;

        //    ///Critical section: the device buffer should fit the arriving data!!!

        //    session.DefaultBufferSize = bufferSize;
        //    session.Timeout = 10000;
        //    //session.TerminationCharacterEnabled = true;
        //    //session.TerminationCharacter = (byte)'\n';

        //    //BinaryEncoding.RawLittleEndia
        //    session.Write("*RST\n");
        //    session.Write("*CLS\n");
        //    session.Write("ROUT:ENAB ON,(@101:104)\n");
        //    session.Write(String.Format("ACQ:SRAT {0}\n",sample_rate));
        //    session.Write(String.Format("ACQ:POIN {0}\n",points_per_sample));
        //    session.Write(String.Format("WAV:POIN {0}\n",points_per_sample));

        //    session.Write("RUN\n");
        //    int counter = 0;
        //    int minute = 60;


        //    int cycles = sample_rate * 30 * minute;
        //    string status = string.Empty;
        //    string data = string.Empty;
        //    //byte[] result = null;
        //    //ushort[] array = null;
        //    //byte[] data_query = ASCIIEncoding.ASCII.GetBytes("WAV:DATA?"); //BinaryEncoding.RawLittleEndian
        //    byte[] array = null;
        //    //var reader = new MessageBasedSessionReader(session);
        //    //reader.BinaryEncoding = BinaryEncoding.RawLittleEndian;
        //    try
        //    {
        //        using (StreamWriter fs = new StreamWriter(new FileStream("temp_file.txt", FileMode.Create, FileAccess.Write, FileShare.Read)))
        //        {
        //            const int SAMPLE_NUMER = 50000 * 4;
        //            string dataQuery = "WAV:DATA?\n";
        //            while (counter++ < cycles)
        //            {
        //                session.Write("WAV:STAT?\n");
        //                status = session.ReadString();
        //                //Console.WriteLine(session.ReadStatusByte());

        //                if (status == "DATA\n")
        //                {
        //                    //data = session.Query(dataQuery);
        //                    session.Write(dataQuery);
        //                    data = session.ReadString();
                            
        //                    //fs.Write(data);
        //                    //array = session.ReadByteArray();
        //                    //array = reader.ReadUInt16s(SAMPLE_NUMER);
        //                    //array = reader.ReadBytes(bufferSize);
        //                    //Console.WriteLine("data length {0}", array.Length);
        //                    //Console.WriteLine(status);
        //                }
        //                else if (status == "OVER\n")
        //                {
        //                    Console.WriteLine(status);
        //                    break;

        //                }

        //                Console.WriteLine("status {0}, counter {1}, length {2}\n ", status.TrimEnd('\n'), counter, data.Length);//, data.Substring(0,header_size));
        //            }
        //        }
        //    }catch(Exception e)
        //    {
        //        session.Write("STOP\n");
        //        Console.WriteLine(e.ToString());

        //    }


        //    Console.WriteLine("Done");
        //    session.Write("STOP\n");
        //    Console.ReadKey();
        
            
        //}

        //static void Main(string[] args)
        //{
        //    var agilent = new AgilentU2542A("asdasd", "", "ADC");
        //    var ch1 = agilent.GetAnalogInputChannel(ChannelEnum.AI_CH101);
        //    ch1.ChannelEnable = ChannelEnableEnum.Enabled;

        //    ch1.SampleRate = 500000;
        //    ch1.PointsPerShot = 50000;
        //    ch1.DataSetReady += ch1_DataSetReady;
        //    Console.WriteLine(agilent.Query(agilent.CommandSet.IDNQuery()));
        //    ch1.StartAcquisition();
        //    System.Threading.Thread.Sleep(100000);
        //    ch1.StopAcquisition();
        //    //for (int i = 0; i < 0xFFFFFFFE; i++)
        //    //{
        //    //ss
        //    //}
        //}

        //static long counter = 0;
        //static void ch1_DataSetReady(object sender, EventArgs e)
        //{
        //    var a = (AgilentU2442A.AnalogInputChannel)sender;
        //    double[] data;
        //    a.DequeueData(out data);
        //    counter++;
        //    Console.WriteLine("b{0},d{1};", counter, data.Length);
        //}
    }
}
