//using AgilentU2542A;
//using AgilentU2442A;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AgilentTest
{
    class Program
    {
    
        static void Main(string[] args)
        {

            var a = new Keithley24xx.Keithley24xx("asfas", "asd", "GPIB0::16::INSTR");
            a.SwitchOn();
            Thread.Sleep(3000);
            a.SwitchOff();
            //Agilent2542A ag = new Agilent2542A("a","a","USB0::0x0957::0x1718::TW52524501::0::INSTR");
            
            //AgilentU2542A ag = new AgilentU2542A("Agilent", "asrdasd", "USB0::0x0957::0x1718::TW52524501::0::INSTR");
            //var a = ag.GetAnalogInputChannel(ChannelEnum.AI_CH101);
            //a.SampleRate = 500000;
            //a.PointsPerShot = 500000;
            //a.AquisitionVoltageRange = VoltageRangeEnum.V10;
            //a.AquisitionVoltagePolarity = PolarityEnum.Bipolar;
            ////a.AquisitionVoltageRange = VoltageRangeEnum.V1_25;
            //a.VoltageRange = VoltageRangeEnum.V10;
            //a.VoltagePolarity = PolarityEnum.Bipolar;
            //a.DataSetReady += a_DataSetReady;
            //Console.WriteLine(a.AnalogRead(1000));

            //Stopwatch sw = new Stopwatch();
            //sw.Start();
            //a.StartAcquisition();

            //System.Threading.Thread.Sleep(120000);
            //a.StopAcquisition();
            //sw.Stop();
            //Console.WriteLine(sw.ElapsedMilliseconds);
            //Console.ReadKey();
        }

        //static void a_DataSetReady(object sender, EventArgs e)
        //{
        //    var channel = (AnalogInputChannel)sender;
        //    double[] data;
        //    channel.DequeueData(out data);
        //    Console.WriteLine(data.Length);
        //}
    }
}
