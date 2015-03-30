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

        public IEnumerator<double> GetEnumerator()
        {
            EnumerationInProgress = true;
            int counter = 0;
            double value = RangeStartValue;
            int CurrentCountDirection = CountDirection;
            int mode = (int)CountingMode * 3; // to make possible distinguish between modes + count direction

            var IncrementFuncArray = new Action[6]{
                new Action(()=>{    // Repeat and -1   -- variant 0
                    if (value + CurrentCountDirection * RangeStepValue < RangeEndValue)
                    {
                        value = RangeStartValue;
                    }
                    else
                        value+=CurrentCountDirection*RangeStepValue;
                }), 
                new Action(()=>{     // Repeat and 0   -- variant 1
                    
                }),
                new Action(()=>{     // Repeat and 1   -- variant 2
                    if (value+CurrentCountDirection * RangeStepValue > RangeEndValue)
                    {
                        value = RangeStartValue;
                    }
                    else
                        value += CurrentCountDirection * RangeStepValue;
                }),
                new Action(()=>{     // Cont and -1   -- variant 3
                    if (value+CurrentCountDirection * RangeStepValue < RangeEndValue)
                    {
                        CurrentCountDirection = -CurrentCountDirection;
                    }
                    value += CurrentCountDirection * RangeStepValue;
                }),
                new Action(()=>{     // Cont and 0   -- variant 4

                }),
                new Action(()=>{      // Cont and 1   -- variant 5
                    if (value+CurrentCountDirection * RangeStepValue > RangeEndValue)
                    {
                        CurrentCountDirection = -CurrentCountDirection;
                    }
                    value += CurrentCountDirection * RangeStepValue;
                })
            };

            for (; counter < TotalPointsCount && EnumerationInProgress; IncrementFuncArray[CurrentCountDirection + mode + 1]())
            {
                yield return value;
            }
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

    }
}
