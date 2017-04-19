using ExperimentViewer.HelperExecutables.TimeDelay;
using IVexperiment.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScenarioBuilder.ViewModel
{
    public interface IExperimentItem
    {
        string Name { get; }
        object GenerateViewModel();
        object Data { get; }
    }

    public abstract class ExperimentItem:IExperimentItem
    {
        public ExperimentItem(Type experimentType)
        {
            expType = experimentType;
            Name = expType.Name;
            expData = GenerateViewModel();
        }
        Type expType;
        object expData;

        public string Name
        {
            get;
            protected set;
        }

        public object Data
        {
            get { return expData; }
        }

        public virtual object GenerateViewModel()
        {
            return Activator.CreateInstance(expType);
        }
    }
    public class ExperimentItem<ExperimentType>:ExperimentItem
        where ExperimentType:class, new()
    {
        public ExperimentItem():base(typeof(ExperimentType))
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
            AvailableExperiments = new List<IExperimentItem>();
            ScenarioExperimentsList = new List<IExperimentItem>();
            InitializeAvailableExperiments();    
        }

        private void InitializeAvailableExperiments()
        {
            AvailableExperiments.Add(new ExperimentItem<OutputIVViewModel>());
            AvailableExperiments.Add(new ExperimentItem<TransfrerIVViewModel>());
            AvailableExperiments.Add(new ExperimentItem<TimeDelayExecutableViewModel>());

            ScenarioExperimentsList.Add(new ExperimentItem<OutputIVViewModel>());
            ScenarioExperimentsList.Add(new ExperimentItem<TransfrerIVViewModel>());
            ScenarioExperimentsList.Add(new ExperimentItem<TimeDelayExecutableViewModel>());

        }

        public List<IExperimentItem> AvailableExperiments { get; private set; }
        public List<IExperimentItem> ScenarioExperimentsList { get; private set; }

    }
}
