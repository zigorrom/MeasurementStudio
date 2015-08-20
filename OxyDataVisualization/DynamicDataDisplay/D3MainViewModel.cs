using Microsoft.Research.DynamicDataDisplay;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Research.DynamicDataDisplay.DataSources;

namespace DataVisualization.DynamicDataDisplay
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
            _plotter.AddLineChart()
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
