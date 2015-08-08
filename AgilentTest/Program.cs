
using InstrumentHandlerNamespace;
using Keithley24xxNamespace;
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
       //     var ih = InstrumentHandler.Instance;
            

           // var a = new Keithley24xx("asfas", "asd", "GPIB0::16::INSTR");
           // //a.ConfigureSourceDC(ke2400Constants.SourceFunctionModeVoltage, 0, 0, false, 0, ke2400Constants.StandbyHighImpedance);
           // a.SetSpeed(MeasurementSpeed.Middle);
           // //a.SetVoltageLimit(10);
           // //a.SetCurrentLimit(10);
           // //a.SetCurrentAndVoltageMeasurement();
           // //var count = 100000000;
           // var list = new List<row>();
           // var sw = new Stopwatch();

           // a.SwitchOn();
           // a.SetSourceVoltage(3);
           // sw.Start();
           // for (; sw.ElapsedMilliseconds<30000;)
           // {
           //     var r = new row();
           //     a.MeasureAll(out r.volt, out r.curr, out r.res);
           //     r.time = sw.ElapsedMilliseconds;
           //     list.Add(r);
           // }
           // sw.Stop();
           //// var sdsd = a.MeasureCurrent(1000, 0);
           
            
           //// Thread.Sleep(3000);
           // a.SwitchOff();
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
