using ExperimentAbstraction.HelperExecutables.TimeDelay;
using IVexperiment.ViewModels;
using Microsoft.TeamFoundation.MVVM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public AvailableExperimentItem():base(typeof(ExperimentType))
        {
            
        }

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
            InitializeAvailableExperiments();    
        }

        private void InitializeAvailableExperiments()
        {
            var item = new AvailableExperimentItem<OutputIVViewModel>();
            AvailableExperiments.Add(item);
            ScenarioExperimentsList.Add(item.GenerateExperimentItem());
            var item2 = new AvailableExperimentItem<TransfrerIVViewModel>();
            AvailableExperiments.Add(item2);
            ScenarioExperimentsList.Add(item2.GenerateExperimentItem());
            AvailableExperiments.Add(new AvailableExperimentItem<TimeDelayExecutableViewModel>());

        }

        public ObservableCollection<AvailableExperimentItem> AvailableExperiments { get; private set; }
        public ObservableCollection<IExperimentItem> ScenarioExperimentsList { get; private set; }


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
                ScenarioExperimentsList.Add(item.GenerateExperimentItem());
            }

        }

        private ICommand _removeAllFromScenatioCommand;

        public ICommand RemoveAllFromScenarioCommand
        {
            get { return _removeAllFromScenatioCommand ?? (_removeAllFromScenatioCommand = new RelayCommand(() => ScenarioExperimentsList.Clear())); }
        }




    }
}
