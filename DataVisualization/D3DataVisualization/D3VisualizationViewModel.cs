using Microsoft.Research.DynamicDataDisplay.DataSources;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DataVisualization.D3DataVisualization
{
    public class D3VisualizationViewModel:IDataVisualizationViewModel
    {
        #region NotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string PropertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(PropertyName));
        }
        private bool SetField<T>(ref T field, T value, string PropertyName)
        {
            if (Object.Equals(field, value))
                return false;
            field = value;
            OnPropertyChanged(PropertyName);
            return true;
        }


        #endregion
        public D3VisualizationViewModel()
        {
            //Title = "adfasf";
            //HorizontalAxisTitle = "horiz";
            //VerticalAxisTitle = "vert";
            //StrokeThickness = 4;
        }


        public ID3View View { get; set; }
        
        private string _title;
        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                SetField(ref _title, value, "Title");
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

        private int _strokeThickness;
        public int StrokeThickness
        {
            get
            {
                return _strokeThickness;
            }
            set
            {
                SetField(ref _strokeThickness, value, "StrokeThickness");
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
                //View.SetScale(value);
                SetField(ref _scaleType, value, "ScaleType");
            }
        }




        public void AddLineGraph(IPointDataSource data)
        {
            View.AddSeries(data);
        }

        //public async Task ExecuteInUIThread(Action action)
        //{
        //    await Application.Current.Dispatcher.BeginInvoke(action, null);
        //}


        //public System.Windows.Controls.UserControl MainView
        //{
        //    get;
        //    set;
        //}

        public void Clear()
        {
            View.Clear();
        }
    }
}
