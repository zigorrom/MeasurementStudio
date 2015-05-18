using Helper.Ranges.DoubleRange;
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
        
        
        public event PropertyChangedEventHandler PropertyChanged;



        public void OnPropertyChanged(string PropertyName)
        {
            var handler = PropertyChanged;
            if(handler!= null)
                handler(this, new PropertyChangedEventArgs(PropertyName));
        }


        public RangeViewModel(DoubleUnitValueDependencyObject start, DoubleUnitValueDependencyObject end, DoubleUnitValueDependencyObject step)
        {
            Start = start;

            End = end;

            Step = step;

            PointsCount = new IntPointsCount(0);

            m_doubleRange = new DoubleRangeBase(Start.NumericValue, End.NumericValue, Step.NumericValue);

            var StartBind = new Binding("Start");
            StartBind.Source = m_doubleRange;//.Start;
            StartBind.Mode = BindingMode.TwoWay;
            StartBind.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            BindingOperations.SetBinding(Start, DoubleUnitValueDependencyObject.NumericValueProperty, StartBind);

            var EndBind = new Binding("End");
            EndBind.Source = m_doubleRange;//.End;
            EndBind.Mode = BindingMode.TwoWay;
            EndBind.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            BindingOperations.SetBinding(End, DoubleUnitValueDependencyObject.NumericValueProperty, EndBind);

            var StepBind = new Binding("Step");
            StepBind.Source = m_doubleRange;//.Step;
            StepBind.Mode = BindingMode.TwoWay;
            StepBind.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            BindingOperations.SetBinding(Step, DoubleUnitValueDependencyObject.NumericValueProperty, StepBind);

            var PointsCountBind = new Binding("PointsCount");
            PointsCountBind.Source = m_doubleRange;//.PointsCount;
            PointsCountBind.Mode = BindingMode.TwoWay;
            PointsCountBind.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            BindingOperations.SetBinding(PointsCount,IntPointsCount.PointsCountProperty, PointsCountBind);
        }


       

        public IntPointsCount PointsCount
        {
            get { return m_PointCount; }
            set
            {
                if (m_PointCount == value) return;
                m_PointCount = value;
                OnPropertyChanged("PointsCount");
            }
        }

        public DoubleUnitValueDependencyObject Start
        {
            get { return m_Start; }
            private set
            {
                if (m_Start == value)
                    return;
                m_Start = value;
                OnPropertyChanged("Start");
            }
        }

        public DoubleUnitValueDependencyObject End
        {
            get { return m_End; }
            private set
            {
                if (m_End == value)
                    return;
                m_End = value;
                OnPropertyChanged("End");
            }
        }

        public DoubleUnitValueDependencyObject Step
        {
            get { return m_Step; }
            private set
            {
                if (m_Step== value)
                    return;
                m_Step= value;
                OnPropertyChanged("Step");
            }
        }

        public DoubleRangeBase Range
        {
            get { return m_doubleRange; }
        }
    }
}
