using ExperimentAbstraction;
using Microsoft.TeamFoundation.MVVM;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace ChannelSwitchExecutable
{
    public class ChannelSwitchExecutableViewModel: INotifyPropertyChanged, IExecutableViewModel
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

        public IExecutable Executable
        {
            get { throw new NotImplementedException(); }
        }

        private ICommand _buttonPressed;
        public ICommand ButtonPressed
        {
            get
            {
                return _buttonPressed ?? (_buttonPressed = new RelayCommand((b) =>
                {
                    var button = (Button)b;
                    var channelNumber = int.Parse(button.Content.ToString());

                    
                    //var task = new MeasurementChannelController(channelNumber);
                    //this.SelectedMeasurementChannels.Add(task);

                    //System.Windows.MessageBox.Show(String.Format("Button pressed {0}",button.Content));
                }));//new RoutedUICommand("keyInput", "keyPressed", typeof(IMainViewModel)));
            }
        } 

    }
}
