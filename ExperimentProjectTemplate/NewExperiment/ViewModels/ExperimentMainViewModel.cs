using ExperimentAbstraction;
using Helper.NewExperimentWindow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewExperiment.ViewModels
{
    public abstract class ExperimentMainViewModel:AbstractExperimentViewModel
    {
        public ExperimentMainViewModel()
        {
            SettingsViewModel = new ExperimentSettingsViewModel();
        }

        public ExperimentSettingsViewModel SettingsViewModel { get; set; }

        protected override string GetExperimentName()
        {
            var d = new NewExperimentControl(ExperimentName);
            if (d.ShowDialog().Value)
                return d.ExperimentName;
            return String.Empty;
        }
    }
}
