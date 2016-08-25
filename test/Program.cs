
using NationalInstruments.VisaNS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.SelfHost;
using System.Web.Http;
using Hioki3432;
using Instruments;
using System.ServiceModel;
using MeasurementStudioWebApi;
using System.Threading;
using AgilentU2442A_IVIdriver;

namespace test
{



    class Program
    {
        static void Main(string[] args)
        {
            
            AgilentU2542A a = new AgilentU2542A("agilent", "a", "USB0::0x0957::0x1718::TW52524501::INSTR");
            var ch1 = a.GetAnalogInputChannel(ChannelEnum.AI_CH101);
            var ch2 = a.GetAnalogInputChannel(ChannelEnum.AI_CH102);
            var ch3 = a.GetAnalogInputChannel(ChannelEnum.AI_CH103);
            var ch4 = a.GetAnalogInputChannel(ChannelEnum.AI_CH104);
            Console.WriteLine("Enter waiting time in ms");
            var time = int.Parse(Console.ReadLine());
            a.Aquisition.SampleRate = 500000;
            a.Aquisition.SamplesPerShot = 100000;
            ch1.ChannelEnable = true;
            ch2.ChannelEnable = true;
            ch3.ChannelEnable = true;
            ch4.ChannelEnable = true;

            //for (int i = 0; i < 10; i++)
            //{
                Console.WriteLine("Cycle {0} starts!");
                a.Aquisition.StartAcquisition();
                Thread.Sleep(time);
                a.Aquisition.StopAcquisition();
            //}
            Console.WriteLine("Aquisition finished -> ");
            Console.WriteLine("Channel1: {0}",ch1.count);
            Console.WriteLine("Channel2: {0}", ch2.count);
            Console.WriteLine("Channel3: {0}", ch3.count);
            Console.WriteLine("Channel4: {0}", ch4.count);

            Console.ReadKey();
        }
    
    }
}
