using DataVisualization.D3DataVisualization;
using ExperimentAbstraction;
using Helper.Ranges.DoubleRange;
using Helper.Ranges.SimpleRangeControl;
using System;
using System.ComponentModel;
using System.Windows;
using IVCharacterization.ViewModels;
using Microsoft.Research.DynamicDataDisplay.DataSources;
using Helper.StartStopControl;


namespace IVCharacterization
{
  

    public abstract class IVMainViewModel : AbstractExperimentViewModel
    {
       

        private D3VisualizationViewModel m_Visualization;
        public D3VisualizationViewModel Visualization
        {
            get { return m_Visualization; }
            private set
            {
                SetField(ref m_Visualization, value, "Visualization");
            }
        }

        public void AddSeries(IPointDataSource Points)
        {
            ExecuteInUIThread(() =>
            {
                if (Visualization != null)
                {
                    Visualization.AddLineGraph(Points);
                }
            });
            
        }

        private bool m_globalIsEnabled;
        public bool GlobalIsEnabled { get { return m_globalIsEnabled; }
            set
            {
                SetField(ref m_globalIsEnabled, value, "GlobalIsEnabled");
            }
        }

        public RangeViewModel DSRangeViewModel { get; set; }
        public RangeViewModel GSRangeViewModel { get; set; }
        public ControlButtonsViewModel ExperimentControlButtons {get;set;}

        public IVexpSettingsViewModel IVSettingsViewModel { get; set; }

        public IVMainViewModel()
        {
            DSRangeViewModel = new RangeViewModel(new Voltage(), new Voltage(), new Voltage());
            GSRangeViewModel = new RangeViewModel(new Voltage(), new Voltage(), new Voltage());



            Visualization = new D3VisualizationViewModel();
            ExperimentControlButtons = new ControlButtonsViewModel();
            GlobalIsEnabled = true;

            IVSettingsViewModel = new IVexpSettingsViewModel();

            ExperimentControlButtons.PauseCommandRaised += ExperimentControlButtons_PauseCommandRaised;
            ExperimentControlButtons.StartCommandRaised += ExperimentControlButtons_StartCommandRaised;
            ExperimentControlButtons.StopCommandRaised += ExperimentControlButtons_StopCommandRaised;

        }

       

        void ExperimentControlButtons_StopCommandRaised(object sender, EventArgs e)
        {
            ExecuteInUIThread(() => GlobalIsEnabled = true);
            Experiment.Abort();
        }

        void ExperimentControlButtons_StartCommandRaised(object sender, EventArgs e)
        {
            ExecuteInUIThread(() => GlobalIsEnabled = false);
            string Message = String.Empty;
            if (CheckParametersBeforeStart(out Message))
            {
                ExperimentIsRunning = true;
                Experiment.Start();
            }
            else
            {
                ExperimentControlButtons.Reset();
                MessageBox.Show(Message);
            }
        }

        void ExperimentControlButtons_PauseCommandRaised(object sender, EventArgs e)
        {
            ExecuteInUIThread(() => GlobalIsEnabled = true);
            Experiment.Pause();
            //System.Diagnostics.Debug.WriteLine("Pause button press not handled");
            //throw new NotImplementedException();
        }


        

        protected override bool CheckParametersBeforeStart(out string Message)
        {
            Message = String.Empty;
            //var res = true;
            if (Experiment.IsRunning)
            {
                Message = "Experiment is running";
                return false;
            }
            if (String.IsNullOrEmpty(ExperimentName))
            {
                Message = "Fill in the experiment name";
                return false;
            }
            if (String.IsNullOrEmpty(MeasurementName))
            {
                Message = "Fill in the measurement name";
                return false;
            }
            return true;
        }

        protected override void ExperimentProgressChangedHandler(object sender, ProgressChangedEventArgs e)
        {
            CurrentProgress = e.ProgressPercentage;
        }

        protected override void ExperimentStoppedHandler(object sender, EventArgs e)
        {
            CurrentProgress = 0;
            ExperimentIsRunning = false;
        }

        protected override void ExperimentPausedHandler(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        protected override void ExperimentStartedHandler(object sender, EventArgs e)
        {
            CurrentProgress = 0;
            ExperimentIsRunning = true;
            
        }

        protected override void ExperimentFinishedHandler(object sender, EventArgs e)
        {
            ExperimentIsRunning = false;
            ExecuteInUIThread(() => GlobalIsEnabled = true);
            ExecuteInUIThread(() => ExperimentControlButtons.Reset());
            
            CurrentProgress = 0;
            
        }

        protected override void ClearVisualization()
        {
            ExecuteInUIThread(() => Visualization.Clear());
        }
        protected override string GetExperimentName()
        {
            var d = new IVCharacterization.Views.NewExperiment(ExperimentName);
            if (d.ShowDialog().Value)
                return d.ExperimentName;
            return String.Empty;
        }
       
    }
}
