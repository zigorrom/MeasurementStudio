using IVexperiment.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScenarioBuilder.ViewModel
{
    public interface IExperimentItem
    {
        string Name { get; }
        object GenerateViewModel();
    }

    public class ExperimentItem:IExperimentItem
    {
        public ExperimentItem()
        {
            expType = null;
        }
        Type expType;

        public void SetExperiment<T>()
        {
            expType = typeof(T);
            Name = expType.Name;
        }

        public string Name
        {
            get;
            private set;
        }
        public object GenerateViewModel()
        {
            return Activator.CreateInstance(expType);
        }
    }
    public class ExperimentItem<ExperimentType>:IExperimentItem
        where ExperimentType:class, new()
    {
        public ExperimentItem()
        {
            var t = typeof(ExperimentType);
            Name = "afasgfashf"+t.Name;

        }

        public string Name
        {
            get;
            private set;
        }
        public object GenerateViewModel()
        {
            return new ExperimentType();
        }

        public override string ToString()
        {
            return Name;
        }
    }

    public class ScenarioBuilderViewModel
    {
        public ScenarioBuilderViewModel()
        {
            AvailableExperiments = new List<IExperimentItem>();
            InitializeAvailableExperiments();    
        }

        private void InitializeAvailableExperiments()
        {
            var item = new ExperimentItem();
            item.SetExperiment<OutputIVViewModel>();
            AvailableExperiments.Add(item);
            //AvailableExperiments.Add(new ExperimentItem<OutputIVViewModel>());
            //AvailableExperiments.Add(new ExperimentItem<TransfrerIVViewModel>());
        }

        public List<IExperimentItem> AvailableExperiments { get; private set; }
        

    }
}
