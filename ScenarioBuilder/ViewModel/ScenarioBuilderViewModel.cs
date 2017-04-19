using ExperimentViewer.HelperExecutables.TimeDelay;
using IVexperiment.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    public abstract class AvailableExperimentItem:IAvailableExperimentItem
    {
        internal class ExperimentItem:IExperimentItem
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
            AvailableExperiments = new ObservableCollection<IAvailableExperimentItem>();
            ScenarioExperimentsList = new ObservableCollection<IExperimentItem>();
            InitializeAvailableExperiments();    
        }

        private void InitializeAvailableExperiments()
        {
            var item = new AvailableExperimentItem<OutputIVViewModel>();
            AvailableExperiments.Add(item);
            ScenarioExperimentsList.Add(item.GenerateExperimentItem());
            AvailableExperiments.Add(new AvailableExperimentItem<TransfrerIVViewModel>());
            AvailableExperiments.Add(new AvailableExperimentItem<TimeDelayExecutableViewModel>());

        }

        public ObservableCollection<IAvailableExperimentItem> AvailableExperiments { get; private set; }
        public ObservableCollection<IExperimentItem> ScenarioExperimentsList { get; private set; }

    }
}
