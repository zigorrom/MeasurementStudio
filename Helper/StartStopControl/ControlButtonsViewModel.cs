using Microsoft.TeamFoundation.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Helper.StartStopControl
{
    public class ControlButtonsViewModel
    {
        public event EventHandler StartCommandRaised;
        public event EventHandler PauseCommandRaised;
        public event EventHandler StopCommandRaised;

        public bool CanStartCommandExecute { get; set; }
        private bool CanStopCommandExecute { get; set; }
        private bool CanPauseCommandExecute { get; set; }

        public ControlButtonsViewModel()
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
            SetCanExecute(true, true, false);
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
            CanStartCommandExecute = CanStartExec;
            CanPauseCommandExecute = CanPauseExec;
            CanStopCommandExecute = CanStopExec;
        }
    }
}
