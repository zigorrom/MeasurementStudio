using Helper.Ranges.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper.Ranges
{
    public class DoublePropertyValueRange:IEnumerable<double>,INotifyPropertyChanged
    {
        #region Routines
        public event PropertyChangedEventHandler PropertyChanged;
        public virtual void OnPropertyChanged(string PropertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(PropertyName));
        }

        public virtual void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, e);
        }
        
        #endregion

        public DoublePropertyValueRange()
        {
            EnumerationInProgress = false;

            m_RangeStartValue = new DoubleProperty("RangeStartValue");
            m_RangeStartValue.SubscribeOnPropertyChanged(OnPropertyChanged);

            m_RangeEndValue = new DoubleProperty("RangeEndValue");
            m_RangeEndValue.SubscribeOnPropertyChanged(OnPropertyChanged);

            m_RangeStepValue = new DoubleProperty("RangeStepValue");
            m_RangeStepValue.SubscribeOnPropertyChanged(OnPropertyChanged);

            m_RangeWidth = new DoubleProperty("RangeWidth");
            m_RangeWidth.SubscribeOnPropertyChanged(OnPropertyChanged);

            m_RangePointsCount = new IntProperty("RangePointsCount");
            m_RangePointsCount.SubscribeOnPropertyChanged(OnPropertyChanged);

            m_TotalPointsCount = new IntProperty("TotalPointsCount");
            m_TotalPointsCount.SubscribeOnPropertyChanged(OnPropertyChanged);

            m_CountDirection = new IntProperty("CountDirection");
            m_CountDirection.SubscribeOnPropertyChanged(OnPropertyChanged);

            m_CyclesNumber = new IntProperty("CyclesNumber");
            m_CyclesNumber.SubscribeOnPropertyChanged(OnPropertyChanged);
            m_CyclesNumber.SetValue(1);

            m_CountingMode = new CountingModeProperty("CountingMode");
            m_CountingMode.SubscribeOnPropertyChanged(OnPropertyChanged);
        }

        public void AbortEnumeration()
        {
            EnumerationInProgress = false;
        }

        private bool EnumerationInProgress;

        private DoubleProperty m_RangeStartValue;
        public double RangeStartValue
        {
            get { return m_RangeStartValue; }
            set
            {
                m_RangeStartValue.SetValue(value, () =>
                {
                    CountDirection = Math.Sign(RangeEndValue - RangeStartValue);
                    RangeWidth = Math.Abs(RangeEndValue - RangeStartValue);
                    if (RangeStepValue != 0)
                        RangePointsCount = (int)(RangeWidth / RangeStepValue) + 1;
                    else
                        RangePointsCount = 1;
                });
            }
        }

        private DoubleProperty m_RangeEndValue;
        public double RangeEndValue
        {
            get { return m_RangeEndValue; }
            set
            {
                m_RangeEndValue.SetValue(value, () =>
                {
                    CountDirection = Math.Sign(RangeEndValue - RangeStartValue);
                    RangeWidth = Math.Abs(RangeEndValue - RangeStartValue);
                    if (RangeStepValue != 0)
                        RangePointsCount = (int)(RangeWidth / RangeStepValue) + 1;
                    else
                        RangePointsCount = 1;
                });
            }
        }



        private DoubleProperty m_RangeStepValue;
        public double RangeStepValue
        {
            get { return m_RangeStepValue; }
            set
            {
                m_RangeStepValue.SetValue(value, () =>
                {
                    if (RangeStepValue != 0)
                        RangePointsCount = (int)(RangeWidth / RangeStepValue) + 1;
                    else
                        RangePointsCount = 1;
                });
            }
        }

        private DoubleProperty m_RangeWidth;
        public double RangeWidth
        {
            get { return m_RangeWidth; }
            private set
            {
                m_RangeWidth.SetValue(value);
                
            }
        }

        private IntProperty m_RangePointsCount;
        public int RangePointsCount
        {
            get { return m_RangePointsCount; }
            set
            {
                m_RangePointsCount.SetValue(value, () =>
                {
                    if (RangePointsCount > 1)
                        RangeStepValue = RangeWidth / (RangePointsCount - 1);
                    else
                        RangeStepValue = 0;
                    TotalPointsCount = RangePointsCount * CyclesNumber;
                });
            }
        }

        private IntProperty m_TotalPointsCount;
        public int TotalPointsCount
        {
            get { return m_TotalPointsCount; }
            set
            {
                m_TotalPointsCount.SetValue(value);
                
            }
        }

        private IntProperty m_CountDirection;
        public int CountDirection
        {
            get { return m_CountDirection; }
            set
            {
                m_CountDirection.SetValue(value);
            }
        }


        private IntProperty m_CyclesNumber;
        public int CyclesNumber
        {
            get { return m_CyclesNumber; }
            set
            {
                m_CyclesNumber.SetValue(value, () =>
                {
                    TotalPointsCount = RangePointsCount * CyclesNumber;
                });
            }
        }

        private CountingModeProperty m_CountingMode;

        public CountingModeEnum CountingMode
        {
            get { return m_CountingMode; }
            set
            {
                m_CountingMode.SetValue(value);
            }
        }
        private IEnumerator<double> GetRepetativeEnumerator()
        {
            throw new NotImplementedException();
        }

        private IEnumerator<double> GetBackAndForthEnumerator()
        {
            var val = RangeStartValue;
            var dir = CountDirection;
            for (int count = 0; count < TotalPointsCount; count++)
            {
                if()
            }

            throw new NotImplementedException();
        }

        public IEnumerator<double> GetEnumerator()
        {
            switch (CountingMode)
            {
                case CountingModeEnum.Repetitive:
                    return GetRepetativeEnumerator();
                case CountingModeEnum.BackAndForth:
                    return GetBackAndForthEnumerator();
                default:
                    throw new ArgumentException("Wrong value");
            }
            
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

    }
}
