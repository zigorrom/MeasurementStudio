using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper.Ranges.DoubleRange
{
    public class IntPointsCount:INotifyPropertyChanged
    {
        public IntPointsCount()
        {
            PointsCount = 0;
        }

        public IntPointsCount(int count)
        {
            PointsCount = count;
        }
        private int m_PointsCount;
        public int PointsCount
        {
            get { return m_PointsCount; }
            set
            {
                if (m_PointsCount == value)
                    return;
                m_PointsCount = value;
                OnPropertyChanged("PointsCount");
            }
        }

        private void OnPropertyChanged(string p)
        {
            var handler = PropertyChanged;
            if (handler!= null)
                PropertyChanged(this, new PropertyChangedEventArgs(p));
        }
        
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
