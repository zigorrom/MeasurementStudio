using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;

namespace DataVisualization
{
    public interface IVisualizationViewModel:INotifyPropertyChanged
    {
        string HorizontalAxisLabel { get; set; }
        string VertivalAxisLabel { get; set; }

        string HeaderLabel { get; set; }
        double LineThickness { get; set; }
        Visibility LegendVisibility { get; set; }
    }
}
