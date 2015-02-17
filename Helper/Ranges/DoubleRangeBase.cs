using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper.Ranges
{
    public class DoubleRangeBase : IEnumerable<double>, INotifyPropertyChanged
    {
        private double m_StartValue;
        private double m_EndValue;
        private double m_StepValue;
        private double m_RangeWidth;
        private int m_PointsCount;
        private int m_Sign;

        public DoubleRangeBase(double start, double end, double step)
        {
            m_StartValue = start;
            m_EndValue = end;
            m_Sign = (end > start) ? 1 : -1;
            m_RangeWidth = Math.Abs(end - start);
            m_StepValue = step;
            if (m_StepValue == 0)
                m_PointsCount = 1;
            else
                m_PointsCount = (int)(m_RangeWidth / m_StepValue) + 1;
        }

        public DoubleRangeBase(double start, double end, int pointsCount)
        {
            m_StartValue = start;
            m_EndValue = end;
            m_Sign = (end > start) ? 1 : -1;
            m_RangeWidth = Math.Abs(end - start);
            m_PointsCount = pointsCount;
            if (m_PointsCount <= 1)
                m_StepValue = 0;
            else
                m_StepValue = m_RangeWidth / (m_PointsCount - 1);
        }

        public double Start
        {
            get { return m_StartValue; }
            set
            {
                if (SetField<double>(ref m_StartValue, value, "Start"))
                {
                    m_Sign = (End > Start) ? 1 : -1;
                    m_RangeWidth = Math.Abs(End - Start);
                    if (Step != 0)
                        PointsCount = (int)(RangeWidth / Step) + 1;
                    else
                        PointsCount = 1;
                }
            }
        }

        public double End
        {
            get { return m_EndValue; }
            set
            {
                if (SetField<double>(ref m_EndValue, value, "End"))
                {
                    m_Sign = (End > Start) ? 1 : -1;
                    m_RangeWidth = Math.Abs(End - Start);
                    if (Step != 0)
                        PointsCount = (int)(RangeWidth / Step) + 1;
                    else
                        PointsCount = 1;
                }

            }
        }

        public double Step
        {
            get { return m_StepValue; }
            set
            {
                if (SetField<double>(ref m_StepValue, value, "Step"))
                {
                    if (Step != 0)
                        PointsCount = (int)(RangeWidth / Step) + 1;
                    else
                        PointsCount = 1;
                }
            }
        }

        public double RangeWidth
        {
            get { return m_RangeWidth; }
        }

        public int PointsCount
        {
            get { return m_PointsCount; }
            set
            {
                if (SetField<int>(ref m_PointsCount, value, "PointsCount"))
                {
                    if (PointsCount > 1)
                        Step = RangeWidth / (PointsCount - 1);
                    else
                        Step = 0;
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetField<ST>(ref ST field, ST value, string propertyName)
        {
            if (EqualityComparer<ST>.Default.Equals(field, value))
                return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        public void Foreach(Action<double> action)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<double> GetEnumerator()
        {
            int counter = 0;

            for (double val = Start; (m_Sign >= 0 ? (val <= End) : (val >= End)) && (counter < PointsCount); val += m_Sign * Step, counter++)
            {
                yield return val;
            }
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
