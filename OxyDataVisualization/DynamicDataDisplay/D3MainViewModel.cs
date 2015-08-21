
using Microsoft.Research.DynamicDataDisplay;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;


namespace DataVisualization.DynamicDataDisplayChart
{
    public class D3MainViewModel:IDataVisualizationViewModel
    {
        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        public bool SetField<T>(ref T Property, T ValueToSet, string PropertyName)
        {
            if (Object.Equals(Property, ValueToSet))
                return false;
            Property = ValueToSet;
            OnPropertyChanged(PropertyName);
            return true;
        }
        public bool SetField<T>(ref T Property, T ValueToSet, string PropertyName, Action callback)
        {
            var res = SetField<T>(ref Property, ValueToSet, PropertyName);
            if (res == false)
                return false;
            callback();
            return true;
        }

        private void OnPropertyChanged(string PropertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(PropertyName));
        }
        #endregion

        private ChartPlotter _plotter;
        public D3MainViewModel(ChartPlotter Plotter)
        {
            _plotter = Plotter;
            AddSeries(enumer());
        }

        private string _chartTitle;
        public string ChartTitle
        {
            get
            {
                return _chartTitle;
            }
            set
            {
                SetField(ref _chartTitle, value, "ChartTitle");
            }
        }


        private string _horizontalAxisTitle;
        public string HorizontalAxisTitle
        {
            get
            {
                return _horizontalAxisTitle;
            }
            set
            {
                SetField(ref _horizontalAxisTitle, value, "HorizontalAxisTitle");
            }
        }

        private string _verticalAxisTitle;
        public string VerticalAxisTitle
        {
            get
            {
                return _verticalAxisTitle;
            }
            set
            {
                SetField(ref _verticalAxisTitle, value, "VerticalAxisTitle");
            }
        }

        private GraphScaleType _scaleType;
        public GraphScaleType ScaleType
        {
            get
            {
                return _scaleType;
            }
            set
            {
                SetField(ref _scaleType, value, "ScaleType");
            }
        }

        public void AddSeries(System.Collections.IEnumerable points)
        {
            
            //_plotter.AddLineChart(new DynamicDataDisplay.Markers.DataSources.EnumerableDataSource(points));

        }
        private IEnumerable<Point> enumer()
        {
            for (int i = 0; i < 1100; i++)
			{
                yield return new Point(i, i);
			}
        }

        public void ClearChart()
        {
            throw new NotImplementedException();
        }

        public void InvalidatePlot()
        {
            throw new NotImplementedException();
        }
    }

    


}
