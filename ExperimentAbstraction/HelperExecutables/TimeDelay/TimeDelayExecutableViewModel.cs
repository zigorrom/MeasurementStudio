using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExperimentViewer.HelperExecutables.TimeDelay
{
    public class TimeDelayExecutableViewModel:INotifyPropertyChanged
    {

        #region PropertyEvents

        public event PropertyChangedEventHandler PropertyChanged;
        protected bool SetField<ST>(ref ST field, ST value, string propertyName)
        {
            if (EqualityComparer<ST>.Default.Equals(field, value))
                return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
        private void OnPropertyChanged(string PropertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(PropertyName));
        }
        #endregion
        public TimeDelayExecutableViewModel()
        {
            DelayExecutable = new TimeDelayExecutable(this);
            DelayExecutable.TimeElapsed += DelayExecutable_TimeElapsed;
        }

        private void DelayExecutable_TimeElapsed(object sender, TimeSpan e)
        {
            CurrentTimeSpan = e;
        }

        public void Reset()
        {
            CurrentTimeSpan = TimeSpan.Zero;
        }


        private TimeSpan _delay;

        public TimeSpan Delay
        {
            get { return _delay; }
            set
            {
                
                if(SetField(ref _delay, value, "Delay"))
                {
                    DelayExecutable.TimeDelay = (int)Delay.TotalMilliseconds;
                }
            }
        }


        private TimeSpan _currentTimeSpan;
        public TimeSpan CurrentTimeSpan
        {
            get { return _currentTimeSpan; }
            set
            {
                SetField(ref _currentTimeSpan, value, "CurrentTimeSpan");
            }
        }

        public TimeDelayExecutable DelayExecutable
        {
            get;
            private set;
        }



        
    }
}
