using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataVisualization
{
    public abstract class AbstractDataVisualizationViewModel:IDataVisualizationViewModel
    {
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        protected bool SetField<T>(ref T field, T value, string PropertyName)
        {
            if (Object.Equals(field, value))
                return false;
            field = value;
            OnPropertyChanged(PropertyName);
            return true;
        }

        protected bool SetField<T>(ref T field, T value, string PropertyName, Action callback)
        {
            var res = SetField<T>(ref field, value, PropertyName);
            if (!res )
                return false;
            callback();
            return true;
        }
        protected void OnPropertyChanged(string PropertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new System.ComponentModel.PropertyChangedEventArgs(PropertyName));
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

        private string _chartSubtitle;
        public string ChartSubtitle
        {
            get
            {
                return _chartSubtitle;
            }
            set
            {
                SetField(ref _chartSubtitle, value, "ChartSubtitle");
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

        public abstract GraphScaleType ScaleType { get; set; }

        public abstract void AddSeries(System.Collections.IEnumerable points);

        public abstract void InvalidatePlot();
        




        
    }
}
