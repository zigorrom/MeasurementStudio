using OxyPlot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataVisualization.OxyPlotVisualization
{
    using OxyPlot.Axes;
    using OxyPlot.Series;
    using System.Collections;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Windows.Data;
    

    public class OxyMainViewModel:INotifyPropertyChanged//:AbstractDataVisualizationViewModel//:INotifyPropertyChanged
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

        private PlotModel _plotModel;
        public PlotModel CurrentPlotModel
        {
            get{return _plotModel;}
            set{
                SetField(ref _plotModel, value, "CurrentPlotModel");
            }
        }


        public string Title
        {
            get { return _plotModel.Title; }
            set
            {
                _plotModel.Title = value;
            }
        }

        public string Subtitle
        {
            get { return _plotModel.Subtitle; }
            set
            {
                _plotModel.Subtitle = value;
            }
        }

        private string _horizontalAxisTitle;
        public string HorizontalAxisTitle
        {
            get { return _horizontalAxisTitle; }
            set
            {
                _horizontalAxisTitle = value;
                if (_bottomAxis != null)
                    _bottomAxis.Title = value;
            }
        }

        private string _verticalAxisTitle;
        public string VerticalAxisTitle
        {
            get { return _verticalAxisTitle; }
            set
            {
                _verticalAxisTitle = value;
                if (_leftAxis != null)
                    _leftAxis.Title = value;
            }
        }
        
        //private string _expressionToDisplay;        
        //public string ExpressionToDisplay { get; set; }

        //create generic types for this function at class definition


        private Axis _bottomAxis;
        private Axis _leftAxis;

        private GraphScaleType _scale;
        public GraphScaleType ScaleType
        {
            get { return _scale; }
            set
            {
                SetField(ref _scale, value, "Scale",
                    new Action(() =>
                    {
                        switch (_scale)
                        {

                            case GraphScaleType.SemiLog:
                                {
                                    _bottomAxis = new LinearAxis { Position = AxisPosition.Bottom };
                                    _leftAxis = new LogarithmicAxis { Position = AxisPosition.Left };
                                }
                                break;
                            case GraphScaleType.Log:
                                {
                                    _bottomAxis = new LogarithmicAxis { Position = AxisPosition.Bottom };
                                    _leftAxis = new LogarithmicAxis { Position = AxisPosition.Left };
                                }
                                break;
                            default:
                            case GraphScaleType.Lin:
                                {
                                    _bottomAxis = new LinearAxis { Position = AxisPosition.Bottom };
                                    _leftAxis = new LinearAxis { Position = AxisPosition.Left };
                                }
                                break;
                        }
                        _bottomAxis.Title = HorizontalAxisTitle;
                        _leftAxis.Title = VerticalAxisTitle;

                        _plotModel.Axes.Clear();
                        _plotModel.Axes.Add(_bottomAxis);
                        _plotModel.Axes.Add(_leftAxis);
                        _bottomAxis.MajorGridlineStyle = LineStyle.Solid;
                        _leftAxis.MajorGridlineStyle = LineStyle.Solid;

                        _plotModel.InvalidatePlot(true);
                    }));
            }
        }

        
    

        public OxyMainViewModel()
        {
            _plotModel = new PlotModel();
            ScaleType = GraphScaleType.Lin;
        }


        public void AddSeries(IEnumerable points)
        {
            if (_plotModel != null)
                _plotModel.Series.Add(new LineSeries { ItemsSource = points });

        }

        public void InvalidatePlot()
        {
            if (_plotModel != null)
                _plotModel.InvalidatePlot(true);
        }



        
    }
}
