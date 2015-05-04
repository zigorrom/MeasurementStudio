using AgilentU2442A;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgilentTest
{
    class Program
    {
        static void Main(string[] args)
        {
            AgilentU2542A ag = new AgilentU2542A("Agilent", "asrdasd", "USB0::0x0957::0x1718::TW52524501::0::INSTR");
            var a = ag.GetAnalogInputChannel(ChannelEnum.AI_CH101);
            a.SampleRate = 50000;
            a.PointsPerShot = 500000;
            Console.WriteLine(a.AnalogRead(1000));
            Stopwatch sw = new Stopwatch();
            sw.Start();
            a.StartAcquisition(); 
            System.Threading.Thread.Sleep(600000);
            a.StopAcquisition();
            sw.Stop();
            Console.WriteLine(sw.ElapsedMilliseconds);
            Console.ReadKey();
        }
    }
}
