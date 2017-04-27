using ChannelSwitchExecutable;
using ExperimentAbstraction;
using ExperimentAbstraction.HelperExecutables.TimeDelay;
using IVexperiment.ViewModels;
using Microsoft.TeamFoundation.MVVM;
using Microsoft.Win32;
using ScenarioBuilder.Views;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ScenarioBuilder.ViewModel
{
    public interface IAvailableExperimentItem
    {
        string Name { get; }
        IExperimentItem GenerateExperimentItem();
    }

    public interface IExperimentItem
    {
        string Name { get; }
        object ViewModel { get; }
    }

    internal class ExperimentItem : IExperimentItem
    {
        public ExperimentItem()
        {

        }

        public string Name
        {
            get;
            internal set;
        }

        public object ViewModel
        {
            get;
            internal set;
        }
    }


    public abstract class AvailableExperimentItem:IAvailableExperimentItem
    {
      
        public AvailableExperimentItem(Type experimentType)
        {
            this.expType = experimentType;
            this.Name = expType.Name;
        }

        public AvailableExperimentItem(Type experimentType, string Name)
        {
            this.expType = experimentType;
            this.Name = Name;
        }


        Type expType;
        
        public string Name
        {
            get;
            protected set;
        }

        public virtual IExperimentItem GenerateExperimentItem()
        {
            return new ExperimentItem { Name = this.Name, ViewModel = Activator.CreateInstance(expType) };
            //return  Activator.CreateInstance(expType);
        }



    }
    public class AvailableExperimentItem<ExperimentType>:AvailableExperimentItem
        where ExperimentType:class, new()
    {
        public AvailableExperimentItem() : base(typeof(ExperimentType)) { }

        public AvailableExperimentItem(string Name) : base(typeof(ExperimentType), Name) { }
    }

    
    public class ScenarioBuilderViewModel:INotifyPropertyChanged
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

        public ScenarioBuilderViewModel()
        {
            AvailableExperiments = new ObservableCollection<AvailableExperimentItem>();
            ScenarioExperimentsList = new ObservableCollection<IExperimentItem>();
            ExperimentDataContext = new ScenarioExperimentDataContext();
            //EditItemWindow = new Window();
            InitializeAvailableExperiments();    
        }

        //private Window EditItemWindow { get; set; }

        private void InitializeAvailableExperiments()
        {
            AvailableExperiments.Add(new AvailableExperimentItem<OutputIVViewModel>("Output characteristic"));
            //ScenarioExperimentsList.Add(item.GenerateExperimentItem());
            AvailableExperiments.Add(new AvailableExperimentItem<TransfrerIVViewModel>("Transfer characteristic"));
            //ScenarioExperimentsList.Add(item2.GenerateExperimentItem());
            AvailableExperiments.Add(new AvailableExperimentItem<TimeDelayExecutableViewModel>("Time delay"));

            AvailableExperiments.Add(new AvailableExperimentItem<ChannelSwitchExecutableViewModel>("Channel switch"));

        }

        public ObservableCollection<AvailableExperimentItem> AvailableExperiments { get; private set; }
        public ObservableCollection<IExperimentItem> ScenarioExperimentsList { get; private set; }
        public ScenarioExperimentDataContext ExperimentDataContext { get; private set; }

        private ICommand _addExperimentToScenario;

        public ICommand AddExperimentToScenarioCommand
        {
            get { return _addExperimentToScenario??(_addExperimentToScenario = new RelayCommand((SelectedList)=>AddExperimentToScenario(SelectedList))); }
        }

        private void AddExperimentToScenario(object SelectedList)
        {
            System.Diagnostics.Debug.WriteLine(SelectedList.GetType());
            var list = ((IList)SelectedList).Cast<IAvailableExperimentItem>();
            foreach (var item in list)
            {
                var experimentItem = item.GenerateExperimentItem();
                var experimentVM = experimentItem as IExperimentDataContextAcceptor;
                if(null!=experimentVM)
                {
                    experimentVM.ExperimentDataContext = this.ExperimentDataContext;
                }
                ScenarioExperimentsList.Add(experimentItem);
            }

        }

        private ICommand _removeAllFromScenatioCommand;

        public ICommand RemoveAllFromScenarioCommand
        {
            get { return _removeAllFromScenatioCommand ?? (_removeAllFromScenatioCommand = new RelayCommand(() => ScenarioExperimentsList.Clear())); }
        }

        private ICommand _removeFromScenarioCommand;

        public ICommand RemoveFromScenarioCommand
        {
            get { return _removeFromScenarioCommand ?? (_removeFromScenarioCommand = new RelayCommand((SelectedList) => RemoveExperimentFromScenario(SelectedList))); }
        }

        private void RemoveExperimentFromScenario(object SelectedList)
        {
            System.Diagnostics.Debug.WriteLine(SelectedList.GetType());
            var list = ((IList)SelectedList).Cast<IExperimentItem>().ToList();
            foreach (var item in list)
            {
                ScenarioExperimentsList.Remove(item);
            }

        }

        private ICommand _removeItemFromScenarioCommand;

        public ICommand RemoveItemFromScenario
        {
            get { return _removeItemFromScenarioCommand ?? (_removeItemFromScenarioCommand = new RelayCommand((i) => RemoveItemFromScenarioList(i))); }
        }

        private void RemoveItemFromScenarioList(object SelectedItem)
        {
            var item = SelectedItem as IExperimentItem;
            if (item != null)
                ScenarioExperimentsList.Remove(item);

        }

        private ICommand _editScenarioItemCommand;

        public ICommand EditScenarioItemCommand
        {
            get { return _editScenarioItemCommand ?? (_editScenarioItemCommand = new RelayCommand((i) => EditScenarioItem(i))); }
        }

        private void EditScenarioItem(object item)
        {
            var vm = ((IExperimentItem)item).ViewModel;
            var wnd = new EditDialogWindow() { Content = vm};
            wnd.Show();
            //EditItemWindow.ShowDialog();
        }

        private ICommand _saveCommand;
        public ICommand SaveCommand
        {
            get { return _saveCommand??(_saveCommand = new RelayCommand(()=>SaveScenario()));}
        }

        private void SaveScenario()
        {
            var sfd = new SaveFileDialog();
            var result = sfd.ShowDialog();
            if (!result.HasValue)
                return;
            if ((bool)result)
            {
                MessageBox.Show("Saving");
            }


        }

    }
}
