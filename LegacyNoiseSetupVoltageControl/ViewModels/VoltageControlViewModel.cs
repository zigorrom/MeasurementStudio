using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LegacyNoiseSetupVoltageControl.ViewModels
{
    public class VoltageControlViewModel:INotifyPropertyChanged
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

        private double _sampleVoltage;
        public double SampleVoltage
        {
            get { return _sampleVoltage; }
            set { SetField(ref _sampleVoltage, value, "SampleVoltage"); }
        }

        private double _gateVoltage;

        public double GateVoltage
        {
            get { return _gateVoltage; }
            set { SetField(ref _gateVoltage, value, "GateVoltage"); }
        }

        private double _sampleVoltageToSet;

        public double SampleVoltageToSet
        {
            get { return _sampleVoltageToSet; }
            set { SetField(ref _sampleVoltageToSet, value, "SampleVoltageToSet"); }
        }

        private double _gateVoltageToSet;

        public double GateVoltageToSet
        {
            get { return _gateVoltageToSet; }
            set { SetField(ref _gateVoltageToSet, value, "GateVoltageToSet"); }
        }

        //private ICommand _selectWorkingDirectory;
        //public ICommand SelectWorkingDirectory
        //{
        //    get
        //    {
        //        return _selectWorkingDirectory ?? (_selectWorkingDirectory = new RelayCommand(() =>
        //        {
        //            //var fbd = new System.Windows.Forms.FolderBrowserDialog();

        //            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
        //            {
        //                WorkingDirectory = fbd.SelectedPath;
        //            }

        //        }));
        //    }
        //}


        public ICommand _setSampleVoltageCommand;
        public ICommand SetSampleVoltageCommand
        {
            get
            {
                return _setSampleVoltageCommand??(_setSampleVoltageCommand = new RelayCommand())
            }
        }

        public ICommand _setGateVoltageCommand;


    }
}
