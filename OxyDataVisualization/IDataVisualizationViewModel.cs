using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataVisualization
{
    public enum GraphScaleType
    {
        None,
        Lin,
        SemiLog,
        Log
    }

    public interface IDataVisualizationViewModel:INotifyPropertyChanged
    {
        string ChartTitle { get; set; }
        string ChartSubtitle { get; set; }
        string HorizontalAxisTitle { get; set; }
        string VerticalAxisTitle { get; set; }
        GraphScaleType ScaleType { get; set; }
        void AddSeries(IEnumerable points);
        void ClearChart();
        void InvalidatePlot();


    }
}
