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
            a.SampleRate = 500000;
            a.PointsPerShot = 500000;
            a.AquisitionVoltageRange = VoltageRangeEnum.V10;
            a.AquisitionVoltagePolarity = PolarityEnum.Bipolar;
            //a.AquisitionVoltageRange = VoltageRangeEnum.V1_25;
            a.VoltageRange = VoltageRangeEnum.V10;
            a.VoltagePolarity = PolarityEnum.Bipolar;
            Console.WriteLine(a.AnalogRead(1000));
            //char lsb = (char)0xe0;
            //char msb = (char)0x31;
            //Console.WriteLine(lsb);
            //Console.WriteLine((int)lsb);
            //Console.WriteLine(msb);
            //Console.WriteLine((int)msb);
            //Console.WriteLine(((int)msb << 8) | (int)lsb);
            Stopwatch sw = new Stopwatch();
            sw.Start();
            a.StartAcquisition();

            System.Threading.Thread.Sleep(10000);
            a.StopAcquisition();
            sw.Stop();
            Console.WriteLine(sw.ElapsedMilliseconds);
            //Console.ReadKey();
        }
    }
}
