using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeasurementStudio
{
    using ExperimentDataModel;
    using IVCharacterization.ViewModels;
    using Microsoft.TeamFoundation.MVVM;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Input;
    
    public class MainViewModel:INotifyPropertyChanged, IMainViewModel
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
        //private HomeViewModel _home;
        //public HomeViewModel Home
        //{
        //    get { return _home; }
        //    set
        //    {
        //        SetField(ref _home, value, "Home");
        //    }
        //}

        private PagesEnum _current;

        private ICommand _selectExperiment;
        public ICommand SelectExperiment
        {
            get
            {
                return _selectExperiment ?? (_selectExperiment = new RelayCommand(
                    (param) =>
                    {
                        PagesEnum exp;
                        if (param is PagesEnum)
                        {
                            exp = (PagesEnum)param;


                            //if (Enum.TryParse<PagesEnum>(param as string, out exp))
                            SwitchToExperiment(exp);
                        }
                    },
                    (param) =>
                    {
                        PagesEnum exp;
                        if (param is PagesEnum)
                        {
                            exp = (PagesEnum)param;
                            if (_current != exp)
                                return true;
                        }
                        return false;
                    }));
            }
        }


        private Dictionary<PagesEnum, UserControl> _controls = new Dictionary<PagesEnum, UserControl>();
        private void SwitchToExperiment(PagesEnum exp)
        {
            if (!_controls.ContainsKey(exp))
                return;
            _current = exp;
            var control = _controls[exp];
            View.ShowPage(control);
        }
        //public ObservableCollection<Lazy<UserControl>> _Controls

        public MainViewModel()
        {
            //Home = new HomeViewModel();
            _current = PagesEnum.Home;
            _controls.Add(PagesEnum.Home, new UserControl { Content = new HomeViewModel()});
            _controls.Add(PagesEnum.IVOutput, new UserControl { Content = new OutputIVViewModel() });
            _controls.Add(PagesEnum.IVTransfer, new UserControl { Content = new TransfrerIVViewModel() });
            //_controls.Add(ExperimentsEnum.IVTransfer, new UserControl{Content = new})
            //View.ShowPage(new System.Windows.Controls.UserControl { Content = Home });
        }

        public IPageTransitionView View
        {
            get;
            set;
        }


        public void DataContextIsSet()
        {
            if(!_controls.ContainsKey(_current))
                return;
            View.ShowPage(_controls[_current]);
        }
    }
}
