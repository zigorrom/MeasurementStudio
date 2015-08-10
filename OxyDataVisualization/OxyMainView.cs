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
    public enum GraphScaleType
    {
        None,
        Lin,
        SemiLog,
        Log
    }

    public class OxyMainView:INotifyPropertyChanged
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
                        Axis _bottomAxis;
                        Axis _leftAxis;
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
                        _plotModel.Axes.Clear();
                        _plotModel.Axes.Add(_bottomAxis);
                        _plotModel.Axes.Add(_leftAxis);
                        _bottomAxis.MajorGridlineStyle = LineStyle.Solid;
                        _leftAxis.MajorGridlineStyle = LineStyle.Solid;
                        
                        _plotModel.InvalidatePlot(true);
                    }));
            }
        }

        
        public void AddPoints()
        {
            var rnd = new Random();
            
            for (int i = 0; i < 100; i++)
            {
                var m = rnd.NextDouble() * 10;
                var n = rnd.NextDouble() * 100000;
                //l.Add(new DataPoint(n,  m*i));
                l.Add(new System.Windows.Point(n, m * i));
            }
            //System.Threading.Thread.Sleep(400);
            //.RemoveRange(0, 100);
            _plotModel.InvalidatePlot(true);
        }
        private ObservableCollection<System.Windows.Point> l;
        public OxyMainView()
        {
            _plotModel = new PlotModel();
            
            Scale = GraphScaleType.Lin;
            l = new ObservableCollection<System.Windows.Point>();
            //l = new ObservableCollection<DataPoint>();
            var ls = new LineSeries { ItemsSource = l };
            _plotModel.Series.Add(ls);
            
            
        }

        
    }
}
