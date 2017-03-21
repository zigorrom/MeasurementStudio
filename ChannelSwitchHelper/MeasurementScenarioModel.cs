using Microsoft.TeamFoundation.MVVM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace ChannelSwitchHelper
{
    public class MeasurementChannelController
    {
        public MeasurementChannelController(int channelNumber)
        {
            ChannelNumber = channelNumber;
        }

        private int _channelNumber;
        public int ChannelNumber
        {
            get { return _channelNumber; }
            private set { _channelNumber = value; }
        }



    }



    public class MeasurementScenarioModel:INotifyPropertyChanged
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

        public MeasurementScenarioModel()
        {
            SelectedMeasurementChannels = new ObservableCollection<MeasurementChannelController>();
            
        }

        public ObservableCollection<MeasurementChannelController> SelectedMeasurementChannels { get; set; }

      
        


        private ICommand _buttonPressed;
        public ICommand ButtonPressed
        {
            get
            {
                return _buttonPressed ?? (_buttonPressed = new RelayCommand((b) => 
                    {
                        var button = (Button)b;
                        var channelNumber = int.Parse(button.Content.ToString());
                        var task = new MeasurementChannelController(channelNumber);
                        this.SelectedMeasurementChannels.Add(task);
                        
                        //System.Windows.MessageBox.Show(String.Format("Button pressed {0}",button.Content));
                    }));//new RoutedUICommand("keyInput", "keyPressed", typeof(IMainViewModel)));
            }
        }

        private ICommand _removeItemPressed;
        public ICommand RemoveItemPressed
        {
            get
            {
                return _removeItemPressed ?? (_removeItemPressed = new RelayCommand((item) => {
                    var selectedItem = item as MeasurementChannelController;
                    //System.Windows.MessageBox.Show("removing");
                    if (selectedItem != null)
                    {
                        SelectedMeasurementChannels.Remove(selectedItem);
                    }


                }));
            }
        }

        private ICommand _addAllItemsPressed;
        public ICommand AddAllItemsPressed
        {
            get
            {
                return _addAllItemsPressed ?? (_addAllItemsPressed = new RelayCommand(() =>
                {
                    for (int i = 1; i <= 32; i++)
                    {
                        this.SelectedMeasurementChannels.Add(new MeasurementChannelController(i));
                    }

                    //var selectedItem = item as MeasurementChannelController;
                    ////System.Windows.MessageBox.Show("removing");
                    //if (selectedItem != null)
                    //{
                    //    SelectedMeasurementChannels.Remove(selectedItem);
                    //}


                }));
            }
        }

        private ICommand _removeAllItemsPressed;
        public ICommand RemoveAllItemsPressed
        {
            get
            {
                return _removeAllItemsPressed ?? (_removeAllItemsPressed = new RelayCommand(() =>
                {
                    this.SelectedMeasurementChannels.Clear();
                    //var selectedItem = item as MeasurementChannelController;
                    ////System.Windows.MessageBox.Show("removing");
                    //if (selectedItem != null)
                    //{
                    //    SelectedMeasurementChannels.Remove(selectedItem);
                    //}


                }));
            }
        }



    }
}
