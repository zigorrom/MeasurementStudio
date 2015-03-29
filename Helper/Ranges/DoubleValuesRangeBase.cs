using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper.Ranges
{
    public class DoubleValuesRangeBase:INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public virtual void OnPropertyChanged(string PropertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(PropertyName));
        }

        public bool SetField<T> (ref T VarToSet, T Value, string PropertyName)
        {
            if (!EqualityComparer<T>.Default.Equals(VarToSet, Value))
                return false;
            VarToSet = Value;
            OnPropertyChanged(PropertyName);
            return true;
        }

        public DoubleValuesRangeBase()
        {
            m_RangeStartValue = 0;
            m_RangeEndValue = 0;
            m_StepValue = 0;
            m_RangeWidth = 0;
            m_PointsCount = 0;

            m_CountDirection = 1;
            m_CountingMode = CountingModeEnum.Repetitive;
            m_CyclesNumber = 0;
        }

        private void RefreshValues()
        {

        }

        //create value checker which will firstly obtain value check its valid and if it is - send to property
        // if not - make textbox red and write error...

        private double m_RangeStartValue;

        public double RangeStartValue
        {
            get { return m_RangeStartValue; }
            set {
               if(SetField(ref m_RangeStartValue,value,"RangeStart"))
               {

               }
            }
        }

        private double m_RangeEndValue;

        public double RangeEndValue
        {
            get { return m_RangeEndValue; }
            set {
                if (SetField(ref m_RangeEndValue, value, "RangeEndValue"))
                {

                }
            }
        }

        private double m_StepValue;

        public double StepValue
        {
            get { return m_StepValue; }
            set {
                if(SetField(ref m_StepValue,value,"StepValue"))
                {

                }
            }
        }

        private double m_RangeWidth;

        public double RangeWidth
        {
            get { return m_RangeWidth; }
            private set {
                if(SetField(ref m_RangeWidth, value, "RangeWidth"))
                {

                }
            }
        }

        private int m_PointsCount;

        public int PointsCount
        {
            get { return m_PointsCount; }
            set {
                if (SetField(ref m_PointsCount, value, "PointsCount"))
                {

                }
            }
        }

        private int m_CountDirection;

        public int CountDirection
        {
            get { return m_CountDirection; }
            private set {
                if(SetField(ref m_CountDirection, value,"CountDirection"))
                {

                }
            }
        }

        private int m_CyclesNumber;

        public int CyclesNumber
        {
            get { return m_CyclesNumber; }
            set {
                if(SetField(ref m_CyclesNumber, value, "CyclesNumber"))
                {

                }
            }
        }

        private CountingModeEnum m_CountingMode;

        public CountingModeEnum CountingMode
        {
            get { return m_CountingMode; }
            set {
                if(SetField(ref m_CountingMode, value, "CountingMode"))
                {

                }
            }
        }

       
    }
}
