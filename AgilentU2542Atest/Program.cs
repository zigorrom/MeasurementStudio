﻿using AgilentU2442A;
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
            //var md = new 
            //var agilent = new AgilentU2542A("asdasd", "", "ADC");

            ////agilent.Reset();
            //var ch1 = agilent.GetAnalogInputChannel(ChannelEnum.AI_CH101);
            ////ch1.ChannelEnable = ChannelEnableEnum.Enabled;
           
            //ch1.SampleRate = 500000;
            //ch1.PointsPerShot = 50000;
            //ch1.DataSetReady += ch1_DataSetReady;
            //Console.WriteLine(agilent.Query(agilent.CommandSet.IDNQuery()));
            //ch1.StartAcquisition();
            //System.Threading.Thread.Sleep(5000);
            //ch1.StopAcquisition();
            
            //for (int i = 0; i < 0xFFFFFFFE; i++)
            //{
                
            //}
        }
        static long counter = 0;
        static void ch1_DataSetReady(object sender, EventArgs e)
        {
            var a = (AgilentU2442A.AnalogInputChannel)sender;
            double[] data;
            a.DequeueData(out data);
            counter++;
            Console.WriteLine("b{0},d{1};",counter,data.Length);
        }
    }
}