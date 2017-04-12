using ExperimentViewer;
using ExperimentDataModel;
using NoiseMeasurementLegacy.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoiseMeasurementLegacy.Experiments
{
    public abstract class NoiseMeasurementBase<InfoT, DataT> : AbstractExperiment<InfoT, DataT>
        where InfoT : struct,IMeasurementInfo
        where DataT : struct
    {
        protected ExperimentMainViewModel _vm;
        protected string _workingDirectory;
        protected string _experimentName;
        protected string _measurementName;
        protected int _measurementCount;

        public NoiseMeasurementBase(ExperimentMainViewModel viewModel, string Name)
            : base(Name)
        {
            _vm = viewModel;
        }

        public override void InitializeExperiment()
        {
            base.InitializeExperiment();

            ///
            ///  INITIALIZE HERE PARAMETERS OF YOUR EXPERIMENT
            ///


            AssertParams();
            InitializeWriter(_workingDirectory, _experimentName);
        }

        public override void InitializeInstruments()
        {

            ///
            /// INITIALIZE HERE YOUR INSTRUMENTS
            ///

            throw new NotImplementedException();
        }

        public override void OwnInstruments()
        {

            ///
            /// OWN INSTRUMENTS BY EXPERIMENT
            ///
            /// e.g. DrainKeithlet.InstrumentOwner = this;
            ///


            throw new NotImplementedException();
        }

        protected override void AssertParams()
        {
            base.AssertParams();

            ///
            /// CHECK EXPERIMENT PARAMETERS 
            ///
            /// e.g. if(String.IsNullOrEmpty(_workingDirectory))
            ///         throw new ArgumentNullException("Working directory is not set");
            /// 
            ///

            throw new NotImplementedException();
        }

        public override void ReleaseInstruments()
        {
            ///
            /// OWN INSTRUMENTS BY EXPERIMENT
            ///
            /// e.g. DrainKeithlet.InstrumentOwner = null;
            ///

            throw new NotImplementedException();
        }

        protected override void HandleError(Exception e)
        {
            _vm.ErrorHandler(e);
        }

        protected override void HandleMessage(string Message)
        {
            _vm.MessageHandler(Message);
        }
    }
}
