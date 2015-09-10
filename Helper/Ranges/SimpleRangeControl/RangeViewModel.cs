using Helper.Ranges.DoubleRange;
using Helper.Ranges.RangeHandlers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;


namespace Helper.Ranges.SimpleRangeControl
{
    public class RangeViewModel:INotifyPropertyChanged
    {
        private DoubleUnitValueDependencyObject m_Start;
        private DoubleUnitValueDependencyObject m_End;
        private DoubleUnitValueDependencyObject m_Step;
        private IntPointsCount m_PointCount;
        private DoubleRangeBase m_doubleRange;
        private string m_RangeName;


        private const string StartName = "Start";
        private const string EndName = "End";
        private const string StepName = "Step";
        private const string PointsCountName = "PointsCount";
        

        #region NotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string PropertyName)
        {
            var handler = PropertyChanged;
            if(handler!= null)
                handler(this, new PropertyChangedEventArgs(PropertyName));
        }

        //private 
        private bool SetField<T>(ref T field, T value, string FieldName )
        {
            if (Object.Equals(field, value))
                return false;
            field = value;
            OnPropertyChanged(FieldName);
            return true;
        }
        #endregion

        public RangeViewModel(string RangeName,DoubleUnitValueDependencyObject start, DoubleUnitValueDependencyObject end, DoubleUnitValueDependencyObject step):this(start, end, step)
        {
            this.RangeName = RangeName;
        }

        public RangeViewModel(DoubleUnitValueDependencyObject start, DoubleUnitValueDependencyObject end, DoubleUnitValueDependencyObject step)
        {
            Start = start;
            End = end;
            Step = step;
            PointsCount = new IntPointsCount(0);
            RepeatCounts = 1;
            RangeHandler = new NormalDoubleRangeHandler();
            m_doubleRange = new DoubleRangeBase(Start.NumericValue, End.NumericValue, Step.NumericValue);

            RangeHandler.Range = Range;

            var StartBind = new Binding(StartName);
            StartBind.Source = m_doubleRange;//.Start;
            StartBind.Mode = BindingMode.TwoWay;
            StartBind.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            BindingOperations.SetBinding(Start, DoubleUnitValueDependencyObject.NumericValueProperty, StartBind);

            var EndBind = new Binding(EndName);
            EndBind.Source = m_doubleRange;//.End;
            EndBind.Mode = BindingMode.TwoWay;
            EndBind.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            BindingOperations.SetBinding(End, DoubleUnitValueDependencyObject.NumericValueProperty, EndBind);

            var StepBind = new Binding(StepName);
            StepBind.Source = m_doubleRange;//.Step;
            StepBind.Mode = BindingMode.TwoWay;
            StepBind.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            BindingOperations.SetBinding(Step, DoubleUnitValueDependencyObject.NumericValueProperty, StepBind);

            var PointsCountBind = new Binding(PointsCountName);
            PointsCountBind.Source = m_doubleRange;//.PointsCount;
            PointsCountBind.Mode = BindingMode.TwoWay;
            PointsCountBind.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            BindingOperations.SetBinding(PointsCount,IntPointsCount.PointsCountProperty, PointsCountBind);
        }

        private int m_repeatCounts;
        public int RepeatCounts
        {
            get { return m_repeatCounts; }
            set
            {
                if (value < 1)
                    value = 1;
                if (SetField(ref m_repeatCounts, value, "RepeatCounts"))
                {
                    if (m_rangeHandler != null)
                    {
                        RangeHandler.RepeatCounts = m_repeatCounts;
                    }
                }
            }
        }

        private AbstractDoubleRangeHandler m_rangeHandler;
        public AbstractDoubleRangeHandler RangeHandler
        {
            get { return m_rangeHandler; }
            set
            {
                if (SetField(ref m_rangeHandler, value, "RangeHandler"))
                {
                    RangeHandler.RepeatCounts = RepeatCounts;
                    RangeHandler.Range = Range;
                    //RepeatCounts = m_rangeHandler.RepeatCounts;
                }
            }
        }

        public IntPointsCount PointsCount
        {
            get { return m_PointCount; }
            set
            {
                SetField(ref m_PointCount, value, PointsCountName);
                //if (m_PointCount == value) return;
                //m_PointCount = value;
                //OnPropertyChanged("PointsCount");
            }
        }

        public DoubleUnitValueDependencyObject Start
        {
            get { return m_Start; }
            private set
            {
                SetField(ref m_Start, value, StartName);
                //if (m_Start == value)
                //    return;
                //m_Start = value;
                //OnPropertyChanged("Start");
            }
        }

        public DoubleUnitValueDependencyObject End
        {
            get { return m_End; }
            private set
            {
                SetField(ref m_End, value, EndName);
                //if (m_End == value)
                //    return;
                //m_End = value;
                //OnPropertyChanged("End");
            }
        }

        public DoubleUnitValueDependencyObject Step
        {
            get { return m_Step; }
            private set
            {
                //if (m_Step== value)
                //    return;
                //m_Step= value;
                //OnPropertyChanged("Step");
                SetField(ref m_Step, value, StepName);
            }
        }

        public DoubleRangeBase Range
        {
            get { return m_doubleRange; }
        }

        public string RangeName
        {
            get { return m_RangeName; }
            set { SetField(ref m_RangeName, value, "RangeName"); }
        }
    }
}
