
using DataVisualization.D3DataVisualization;
using ExperimentAbstraction;
using Helper.Ranges;
using Helper.Ranges.DoubleRange;
using Helper.Ranges.RangeHandlers;
using Helper.Ranges.SimpleRangeControl;
using Instruments;

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using IVCharacterization.ViewModels;
using Microsoft.Research.DynamicDataDisplay.DataSources;
using Helper.StartStopControl;


namespace IVCharacterization
{
  

    public abstract class IVMainViewModel : AbstractExperimentViewModel
    {
        private RangeHandlerViewModel m_DSRangeHandlerViewModel;
        public RangeHandlerViewModel DSRangeHandlerViewModel
        {
            get { return m_DSRangeHandlerViewModel; }
            private set
            {
                SetField(ref m_DSRangeHandlerViewModel, value, "DSRangeHandlerViewModel");
            }
        }

        private RangeHandlerViewModel m_GSRangeHandlerViewModel;
        public RangeHandlerViewModel GSRangeHandlerViewModel
        {
            get { return m_GSRangeHandlerViewModel; }
            private set
            {
                SetField(ref m_GSRangeHandlerViewModel, value, "GSRangeHandlerViewModel");
            }
        }

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
            if (Visualization != null)
            {
                Visualization.AddLineGraph(Points);
            }
            
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
        
        public IVMainViewModel()
        {
            DSRangeViewModel = new RangeViewModel(new Voltage(), new Voltage(), new Voltage());
            GSRangeViewModel = new RangeViewModel(new Voltage(), new Voltage(), new Voltage());

            DSRangeHandlerViewModel = new RangeHandlerViewModel();
            GSRangeHandlerViewModel = new RangeHandlerViewModel();

            Visualization = new D3VisualizationViewModel();
            ExperimentControlButtons = new ControlButtonsViewModel();
            GlobalIsEnabled = true;

            ExperimentControlButtons.PauseCommandRaised += ExperimentControlButtons_PauseCommandRaised;
            ExperimentControlButtons.StartCommandRaised += ExperimentControlButtons_StartCommandRaised;
            ExperimentControlButtons.StopCommandRaised += ExperimentControlButtons_StopCommandRaised;
        }

        void ExperimentControlButtons_StopCommandRaised(object sender, EventArgs e)
        {
            Experiment.Abort();
        }

        void ExperimentControlButtons_StartCommandRaised(object sender, EventArgs e)
        {
            Experiment.Start();
        }

        void ExperimentControlButtons_PauseCommandRaised(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Pause button press not handled");
            //throw new NotImplementedException();
        }

    }
}
