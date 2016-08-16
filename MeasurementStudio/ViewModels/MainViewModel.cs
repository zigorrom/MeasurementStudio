using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeasurementStudio
{
    using CurrentTimetrace.ViewModels;
    using CVCharacterization.ViewModels;
    using ExperimentDataModel;
    using Helper.ViewModelInterface;
    using IVCharacterization.ViewModels;
    using MeasurementStudioWebApi;
    using Microsoft.TeamFoundation.MVVM;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Net.Http;
    using System.ServiceModel;
    using System.Threading;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Input;
    
    public class MainViewModel:INotifyPropertyChanged, IMainViewModel,IMeasurementWebApi
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


        private ServiceHost _host;
        
        private PagesEnum _current;

        private ICommand _keyPressed;
        public ICommand KeyPressed
        {
            get
            {
                return _keyPressed ?? (_keyPressed = new RelayCommand(() => System.Windows.MessageBox.Show("Key pressed")));//new RoutedUICommand("keyInput", "keyPressed", typeof(IMainViewModel)));
            }
        }

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
            {
                switch (exp)
                {
                    case PagesEnum.Home:
                        _controls.Add(PagesEnum.Home, new UserControl { Content = new HomeViewModel() });
                        break;
                    case PagesEnum.IVOutput:
                        _controls.Add(PagesEnum.IVOutput, new UserControl { Content = new OutputIVViewModel() });
                        break;
                    case PagesEnum.IVTransfer:
                        _controls.Add(PagesEnum.IVTransfer, new UserControl { Content = new TransfrerIVViewModel() });
                        break;
                    case PagesEnum.CVCharacteristics:
                        _controls.Add(PagesEnum.CVCharacteristics, new UserControl { Content = new CVViewModel() });
                        break;
                    case PagesEnum.Timetrace:
                        _controls.Add(PagesEnum.Timetrace, new UserControl { Content = new TimetraceMainViewModel() });
                        break;
                    default:
                        return;
                }
            }
            _current = exp;
            var control = _controls[exp];
            View.ShowPage(control);
        }
        //public ObservableCollection<Lazy<UserControl>> _Controls

        public MainViewModel()
        {
            
            _current = PagesEnum.Home;


            StartWebApiHost();


            //_controls.Add(PagesEnum.Home, new UserControl { Content = new HomeViewModel() });
            //_controls.Add(PagesEnum.IVOutput, new UserControl { Content = new OutputIVViewModel() });
            //_controls.Add(PagesEnum.IVTransfer, new UserControl { Content = new TransfrerIVViewModel() });
            //_controls.Add(PagesEnum.CVCharacteristics, new UserControl { Content = new CVViewModel() });
            //_controls.Add(PagesEnum.Timetrace, new UserControl { Content = new TimetraceMainViewModel() });
            //var baseAddr = "http://localhost:9000/";
            //using(Microsoft.Owin.Hosting.WebApp.Start<Startup>(url: baseAddr))
            //{
            //    HttpClient client = new HttpClient();
            //    var response = client.GetAsync(baseAddr + "api/values").Result;
            //}
            //Initialize();
        }

        private void StartWebApiHost()
        {
            try
            {
                _host = new MeasurementServiceHost(this, typeof(MeasurementWebApiService));
                _host.Open();
            }catch(Exception e)
            {
                ShowMessage("Failed to start api host");
            }
        }



        //private readonly string[] pages = { "Home", "IVoutput", "IVtransfer", "CVcharacteristic" };
        public string[] GetAvailablePages()
        {
            return Enum.GetNames(typeof(PagesEnum));
        }

        public bool SwitchToPage(string PageName)
        {
            var names = Enum.GetNames(typeof(PagesEnum));
            PagesEnum enumVal;
            if (Enum.TryParse(PageName, true, out enumVal))
            {
                if (_current != enumVal)
                    SwitchToExperiment(enumVal);
                return true;
            }
            return false;
           
        }

        public void ShowMessage(string message)
        {
            MessageBox.Show(message,"MainWindow");
        }


        public System.Threading.SynchronizationContext CurrentSynchronizationContext
        {
            get { return SynchronizationContext.Current; }
        }


        //private async void Initialize()
        //{
        //    await Task.Factory.StartNew(() =>
        //    {
                
        //    });
        //    //await InitializeViews();
        //}

        //private async Task InitializeViews()
        //{
        //    var t1 = Task.Factory.StartNew(() => { _controls.Add(PagesEnum.IVOutput, new UserControl { Content = new OutputIVViewModel() }); });
        //    var t2 = Task.Factory.StartNew(() => { _controls.Add(PagesEnum.IVTransfer, new UserControl { Content = new TransfrerIVViewModel() }); });
        //    var t3 = Task.Factory.StartNew(() => { _controls.Add(PagesEnum.CVCharacteristics, new UserControl { Content = new CVViewModel() }); });
        //    await t1;
        //    await t2;
        //    await t3;
        //}


        public IMeasurementView View
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
