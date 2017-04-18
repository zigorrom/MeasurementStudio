using Helper.ViewModelInterface;
using Microsoft.TeamFoundation.MVVM;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Helper.StartStopControl
{
    public class ControlButtonsViewModel:INotifyPropertyChanged,IUIThreadExecutableViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string PropertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(PropertyName));
        }
        private bool SetField<T>(ref T field, T value, string PropertyName)
        {
            if (Object.Equals(field, value))
                return false;
            field = value;
            OnPropertyChanged(PropertyName);
            return true;
            
        }


        public event EventHandler StartCommandRaised;
        public event EventHandler PauseCommandRaised;
        public event EventHandler StopCommandRaised;


        private bool _canStartCommandExecute;
        public bool CanStartCommandExecute
        {
            get { return _canStartCommandExecute; }
            set
            {
                SetField(ref _canStartCommandExecute, value, "CanStartCommandExecute");
            }
        }

        private bool _canStopCommandExecute;
        public bool CanStopCommandExecute
        {
            get { return _canStopCommandExecute; }
            set
            {
                SetField(ref _canStopCommandExecute, value, "CanStopCommandExecute");
            }
        }

        private bool _canPauseCommandExecute;
        public bool CanPauseCommandExecute
        {
            get { return _canPauseCommandExecute; }
            set
            {
                SetField(ref _canPauseCommandExecute, value, "CanPauseCommandExecute");
            }
        }

        public ControlButtonsViewModel()
        {
            CanStartCommandExecute = true;
            CanPauseCommandExecute = false;
            CanStopCommandExecute = false;
        }

        public void Reset()
        {
            CanStartCommandExecute = true;
            CanPauseCommandExecute = false;
            CanStopCommandExecute = false;
        }

        private ICommand _startClickCommand;
        public ICommand StartClickCommand
        {
            get
            {
                return _startClickCommand ?? (_startClickCommand = new RelayCommand((o) => StartCommandExecute(), (o) => CanStartCommandExecute));
            }
        }

        private void StartCommandExecute()
        {
            SetCanExecute(false, true, true);
            var handler = StartCommandRaised;
            if(handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        private ICommand _pauseClickCommand;
        public ICommand PauseClickCommand
        {
            get
            {
                return _pauseClickCommand ?? (_pauseClickCommand = new RelayCommand((o) => PauseCommandExecute(), (o) => CanPauseCommandExecute));
            }
        }

        private void PauseCommandExecute()
        {
            SetCanExecute(false, true, true);
            var handler = PauseCommandRaised;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        

        private ICommand _stopClickCommand;
        public ICommand StopClickCommand
        {
            get
            {
                return _stopClickCommand ?? (_stopClickCommand = new RelayCommand((o) => StopCommandExecute(), (o) => CanStopCommandExecute));
            }
        }

        private void StopCommandExecute()
        {
            SetCanExecute(true, false, false);
            var handler = StopCommandRaised;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        private void SetCanExecute(bool CanStartExec, bool CanStopExec, bool CanPauseExec)
        {
            ExecuteInUIThread(()=>{
                CanStartCommandExecute = CanStartExec;
                CanPauseCommandExecute = CanPauseExec;
                CanStopCommandExecute = CanStopExec;
            });
            
        }



        public void ExecuteInUIThread(Action action)
        {
            Application.Current.Dispatcher.Invoke(action);
        }

        public Task ExecuteInUIThreadAsync(Action action)
        {
            throw new NotImplementedException();
        }

        
    }
}
