using Instruments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ExperimentAbstraction
{
    public interface IExperiment:IInstrumentOwner
    {
        void InitializeExperiment();
        void InitializeInstruments();
        void OwnInstruments();
        void ReleaseInstruments();
        void Start();
        int ReportProgress();
        void Abort();
        object ViewModel { get; }
        UserControl Control { get; }

        event EventHandler ExperimentStarted;
        event EventHandler ExperimentStopped;
        event EventHandler ExperimentPaused;
        event EventHandler ExperimentProgressChanged;
        event EventHandler ExperimentFinished;
    }
}
