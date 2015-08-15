using OxyPlot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OxyDataVisualization
{
    using OxyPlot.Axes;
    using OxyPlot.Series;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Windows.Data;
    public enum GraphScaleType
    {
        None,
        Lin,
        SemiLog,
        Log
    }

    public class OxyMainViewModel:INotifyPropertyChanged
    {
        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        public bool SetValue<T>(ref T Property, T ValueToSet, string PropertyName)
        {
            if (Object.Equals(Property, ValueToSet))
                return false;
            Property = ValueToSet;
            OnPropertyChanged(PropertyName);
            return true;
        }
        public bool SetValue<T>(ref T Property, T ValueToSet, string PropertyName, Action callback)
        {
            var res = SetValue<T>(ref Property, ValueToSet, PropertyName);
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
                SetValue(ref _plotModel, value, "CurrentPlotModel");
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

        private Axis _bottomAxis;
        private Axis _leftAxis;

        private string _horizontalAxisTitle;
        public string HorizontalAxisTitle
        {
            get { return _horizontalAxisTitle; }
            set
            {
                _horizontalAxisTitle = value;
                if(_bottomAxis!=null)
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
        

        private GraphScaleType _scale;
        public GraphScaleType Scale
        {
            get { return _scale; }
            set
            {
                SetValue(ref _scale, value, "Scale",
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
                                    _bottomAxis = new LogarithmicAxis { Position = AxisPosition.Bottom};
                                    _leftAxis = new LogarithmicAxis { Position = AxisPosition.Left};
                                }
                                break;
                            default:
                            case GraphScaleType.Lin:
                                {
                                    _bottomAxis = new LinearAxis { Position = AxisPosition.Bottom};
                                    _leftAxis = new LinearAxis { Position = AxisPosition.Left};
                                }
                                break;
                        }
                       _bottomAxis.Title = _horizontalAxisTitle;
                       _leftAxis.Title = _verticalAxisTitle;

                        _plotModel.Axes.Clear();
                        _plotModel.Axes.Add(_bottomAxis);
                        _plotModel.Axes.Add(_leftAxis);
                        _bottomAxis.MajorGridlineStyle = LineStyle.Solid;
                        _leftAxis.MajorGridlineStyle = LineStyle.Solid;
                        
                        _plotModel.InvalidatePlot(true);
                    }));
            }
        }

        
        public void AddSeries(IEnumerable<DataPoint> Points)
        {
            //var obs = new ObservableCollection<DataPoint>(Points);
            //_plotModel.Series.Add(new LineSeries { ItemsSource = obs, StrokeThickness = 2 });
            //var ls = new LineSeries();
            //var bnd = new Binding();
            //bnd.Source = Points;
            //bnd.Mode = BindingMode.Default;
            //bnd.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            //BindingOperations.SetBinding(OxyPlot.Wpf.LineSeries.ItemsSourceProperty, Points, bnd);


            //    var StartBind = new Binding("Start");
            //StartBind.Source = m_doubleRange;//.Start;
            //StartBind.Mode = BindingMode.TwoWay;
            //StartBind.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            //BindingOperations.SetBinding(Start, DoubleUnitValueDependencyObject.NumericValueProperty, StartBind);
            _plotModel.Series.Add(new LineSeries { ItemsSource = Points, StrokeThickness = 2 });
            //_plotModel.InvalidatePlot(true);
        }
        
        public void InvalidatePlot(bool UpdateData)
        {
            if (_plotModel != null)
                _plotModel.InvalidatePlot(UpdateData);
        }

        public OxyMainViewModel()
        {
            _plotModel = new PlotModel();
            Scale = GraphScaleType.Lin;
           
            
            
        }

        
    }
}
