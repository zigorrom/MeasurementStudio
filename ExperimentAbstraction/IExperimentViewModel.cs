using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ExperimentAbstraction
{
    public interface IExperimentViewModel
    {
        Task ExecuteInUIThread(Action action);
        UserControl MainView { get; set; }
    }
}
