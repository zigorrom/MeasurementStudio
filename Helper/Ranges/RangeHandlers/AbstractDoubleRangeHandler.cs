using Helper.Ranges.DoubleRange;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper.Ranges.RangeHandlers
{
    public abstract class AbstractDoubleRangeHandler:INotifyPropertyChanged, IEnumerable<double>
    {
        public const string NormalRangeHandler = "Normal";
        public  const string BackAndForthRangeHandler = "BackAndForth";
        public  const string ZeroCrossingRangeHandler = "ZeroStart";
        public  const string ZeroCrossingBackAndForthRangeHandler = "ZeroStartBackAndForth";


        private DoubleRangeBase m_range;
        public DoubleRangeBase Range { get { return m_range; }
            set
            {
                if (m_range == value)
                    return;
                m_range = value;
                OnPropertyChanged("Range");
            }
        }
        public AbstractDoubleRangeHandler(string HandlerName, bool BackAndForth, bool StartFromZero, DoubleRangeBase range)
        {
            m_range = range;
            //Initialize(HandlerName, BackAndForth, StartFromZero);
            Initialize(HandlerName, BackAndForth, StartFromZero);
            //m_BackAndForth = false;
            //m_StartFromZero = false;
            m_RepeatCount = 1;
        }
        public AbstractDoubleRangeHandler(string HandlerName, bool BackAndForth, bool StartFromZero)
        {
            m_range = null;
            Initialize(HandlerName, BackAndForth, StartFromZero);
            //m_BackAndForth = false;
            //m_StartFromZero = false;
            m_RepeatCount = 1;
        }

        protected virtual void Initialize(string handlerName,bool backAndForth, bool startFromZero)
        {
            HandlerName = handlerName;
            BackAndForth = backAndForth;
            StartFromZero = startFromZero;
        }

        public event ProgressChangedEventHandler ProgressChanged;

        public void OnProgressChanged(int ProcessPercentage, object State)
        {
            var handler = ProgressChanged;
            if (handler != null)
                handler(this, new ProgressChangedEventArgs(ProcessPercentage, State));
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string PropertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(PropertyName));
        }
        private bool m_BackAndForth;
        private bool m_StartFromZero;
        private int m_RepeatCount;
        private string m_HandlerName;
        public string HandlerName
        {
            get { return m_HandlerName; }
            protected set
            {
                if (m_HandlerName == value)
                    return;
                m_HandlerName = value;
            }
        }
            

        public bool BackAndForth { 
            get { return m_BackAndForth; }
            set
            {
                if (m_BackAndForth == value)
                    return;
                m_BackAndForth = value;
                OnPropertyChanged("BackAndForth");
            }
        }

        public bool StartFromZero
        {
            get { return m_StartFromZero; }
            set
            {
                if (m_StartFromZero == value) return;
                m_StartFromZero = value;
                OnPropertyChanged("StartFromZero");
            }
        }

        public int RepeatCounts
        {
            get { return m_RepeatCount; }
            set
            {
                if (m_RepeatCount == value) return;
                m_RepeatCount = value;
                OnPropertyChanged("RepeatCounts");
            }
        }


        protected bool AssertRangeNull()
        {
            if (m_range == null)
                return true;
            return false;
        }

        public abstract IEnumerator<double> GetEnumerator();
        

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
