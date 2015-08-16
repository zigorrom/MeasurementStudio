﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeasurementStudio
{
    using ExperimentDataModel;
using OxyPlot;
using OxyPlot.Series;
using System.Collections.ObjectModel;
using System.Windows.Data;
    public class MainViewModel
    {
        public PlotModel MyModel { get; private set; }
        //public MeasurementData<int, int> a { get; private set; }
        int i;
        int curr_i;
        //Binding bnd;
        //LineSeries ls;
        public ObservableCollection<DataPoint> a;
        public MainViewModel()
        {
            a = new ObservableCollection<DataPoint>();//new MeasurementData<int, int>(123, new Func<int, DataPoint>((x) => new DataPoint(x, x * x)));
            for (i = 0, curr_i=0; i < 100; i++,curr_i++)
            {
                a.Add(new DataPoint(i,i*i));
            }
            MyModel = new PlotModel { Title = "123" };
            var ls = new LineSeries();
            ls.ItemsSource = a;
            MyModel.Series.Add(ls);
            
            

        }

        public void Add()
        {
            for (int i = 0; i < 16000; i++,curr_i++)
            {
                a.Add(new DataPoint(curr_i,curr_i));
                
            } //MyModel.InvalidatePlot(true);
            
        }

    }
}