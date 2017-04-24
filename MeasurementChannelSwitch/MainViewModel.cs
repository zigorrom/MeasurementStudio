using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeasurementChannelSwitch
{


    using System.ComponentModel;
    using System.Timers;
    using System.Windows.Controls;


    internal enum Layout
    {
        Layout1,
        Layout2
    }

    public class MainViewModel:INotifyPropertyChanged
    {

        Timer _t;
        public MainViewModel()
        {
            Initialize();
           
        }

        private void Initialize()
        {
            _t = new Timer(5000);
            _t.Elapsed += _t_Elapsed;
        }

        void _t_Elapsed(object sender, ElapsedEventArgs e)
        {
            Message = String.Empty;
            _t.Stop();
        }


        private string _message;
        public string Message
        {
            get
            { return _message; }
            set
            {
                _t.Stop();
                SetValue(ref _message, value, "Message");
                if (!String.IsNullOrEmpty(_message))
                    _t.Start();
            }
        }


        private void SetValue<T>(ref T value, T newValue, string PropertyName)
        {
            if (Object.Equals(value, newValue))
                return;
            value = newValue;
            OnPropertyChanged(PropertyName);
        }

        public void OnPropertyChanged(string PropertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                PropertyChanged(this, new PropertyChangedEventArgs(PropertyName));
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
