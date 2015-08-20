using Microsoft.Research.DynamicDataDisplay;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public string ChartTitle
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public string ChartSubtitle
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public string HorizontalAxisTitle
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public string VerticalAxisTitle
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public GraphScaleType ScaleType
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public void AddSeries(System.Collections.IEnumerable points)
        {
            throw new NotImplementedException();
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
