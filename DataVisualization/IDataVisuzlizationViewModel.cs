using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DataVisualization
{

    public enum GraphScaleType
    {
        LinLin,
        LinLog,
        LogLog
    }
    public interface IDataVisualizationViewModel:INotifyPropertyChanged
    {
        string Title { get; set; }
        string HorizontalAxisTitle { get; set; }
        string VerticalAxisTitle { get; set; }
        int StrokeThickness { get; set; }
        GraphScaleType ScaleType { get; set; }

        void AddLineGraph(IEnumerable<Point> data);
    }
}
