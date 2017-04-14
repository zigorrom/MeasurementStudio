using Microsoft.TeamFoundation.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ExperimentViewer.ViewModels
{
    public class ExperimentMenuItemViewModel
    {
        public ExperimentMenuItemViewModel(string ExperimentName, ExecutionViewModel parent)
        {
            _experimentName = ExperimentName;
            _parentViewModel = parent;
        }

        private string _experimentName;
        public string ExperimentName { get { return _experimentName; } }

        private ExecutionViewModel _parentViewModel;
        public ExecutionViewModel ParentExecutionViewModel { get { return _parentViewModel; } }

        private ICommand _openExperiment;
        public ICommand OpenExperiment
        {
            get
            {
                return _openExperiment ?? (_openExperiment = new RelayCommand(() => _parentViewModel.OpenExperiment(_experimentName)));
            }
        }




    }
}
