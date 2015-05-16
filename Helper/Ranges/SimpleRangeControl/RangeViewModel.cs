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
        private DoubleUnitValue m_Start;
        private DoubleUnitValue m_End;
        private DoubleUnitValue m_Step;
        private IntPointsCount m_PointCount;
        private DoubleRangeBase m_doubleRange;
        
        
        public event PropertyChangedEventHandler PropertyChanged;



        public void OnPropertyChanged(string PropertyName)
        {
            var handler = PropertyChanged;
            if(handler!= null)
                handler(this, new PropertyChangedEventArgs(PropertyName));
        }
        

        public RangeViewModel(DoubleUnitValue start, DoubleUnitValue end, DoubleUnitValue step)
        {


            Start = start;
            //Start.NumericValueChanged += Start_NumericValueChanged;

            End = end;
            //End.NumericValueChanged += End_NumericValueChanged;

            Step = step;
            //Step.NumericValueChanged += Step_NumericValueChanged;

            PointsCount = new IntPointsCount(0);
            //PointsCount.CountChanged += PointsCount_CountChanged;

            m_doubleRange = new DoubleRangeBase(Start.NumericValue, End.NumericValue, Step.NumericValue);

            //var StartBind = new Binding("NumericValue");
            //StartBind.Source = Start;
            //StartBind.Mode = BindingMode.TwoWay;
            //StartBind.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            //BindingOperations.SetBinding(m_doubleRange, DoubleRangeBase.StartProperty, StartBind);

            //var EndBind = new Binding("NumericValue");
            //EndBind.Source = End;
            //EndBind.Mode = BindingMode.TwoWay;
            //EndBind.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            //BindingOperations.SetBinding(m_doubleRange, DoubleRangeBase.EndProperty, EndBind);

            //var StepBind = new Binding("NumericValue");
            //StepBind.Source = Step;
            //StepBind.Mode = BindingMode.TwoWay;
            //StepBind.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            //BindingOperations.SetBinding(m_doubleRange, DoubleRangeBase.StepProperty, StepBind);

            //var PointsCountBind = new Binding("PointsCount");
            //PointsCountBind.Source = PointsCount;
            //PointsCountBind.Mode = BindingMode.TwoWay;
            //PointsCountBind.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            //BindingOperations.SetBinding(m_doubleRange, DoubleRangeBase.PointsCountProperty, PointsCountBind);
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

        public DoubleUnitValue Start
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

        public DoubleUnitValue End
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

        public DoubleUnitValue Step
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
    }
}
