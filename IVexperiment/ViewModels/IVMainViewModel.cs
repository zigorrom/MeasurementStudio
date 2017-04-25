﻿using DataVisualization.D3DataVisualization;
using Helper.Ranges.DoubleRange;
using Helper.Ranges.SimpleRangeControl;
using System;
using System.ComponentModel;
using System.Windows;
using IVexperiment.ViewModels;
using Microsoft.Research.DynamicDataDisplay.DataSources;
using Helper.StartStopControl;
using Helper.NewExperimentWindow;
using ExperimentAbstraction.ViewModels;
using ExperimentAbstraction;
using Helper.ViewModelInterface;
using System.Xml.Serialization;


namespace IVexperiment
{
    [Serializable()]
    public abstract class IVMainViewModel<ExperimentType> : NewAbstractExperimentViewModel<ExperimentType>, ICurrentVoltageMeasurementViewModel
        where ExperimentType:INewExperiment
    {
        public IVMainViewModel()
        {
            SetRangeViewModels(out _drainSourceRangeViewModel, out _gateSourceRangeViewModel);
            SetVisualization(out m_Visualization);
            //load settings from file
            IVSettingsViewModel = new IVexpSettingsViewModel();
            
        }


        private D3VisualizationViewModel m_Visualization;
        [XmlIgnoreAttribute]
        public D3VisualizationViewModel Visualization
        {
            get { return m_Visualization; }
            private set
            {
                SetField(ref m_Visualization, value, "Visualization");
            }
        }

        public void AddSeries(IPointDataSource Points, string Description)
        {
            ExecuteInUIThread(() =>
            {
                if (Visualization != null)
                {
                    Visualization.AddLineGraph(Points, Description);
                }
            });
            
        }

        private VoltageRangeViewModel _drainSourceRangeViewModel;
        public VoltageRangeViewModel DrainSourceRangeViewModel
        {
            get { return _drainSourceRangeViewModel; }
            set { _drainSourceRangeViewModel = value; }
        }

        private VoltageRangeViewModel _gateSourceRangeViewModel;

        public VoltageRangeViewModel GateSourceRangeViewModel
        {
            get { return _gateSourceRangeViewModel; }
            set { _gateSourceRangeViewModel = value; }
        }

        public IVexpSettingsViewModel IVSettingsViewModel { get; set; }
        protected abstract void SetRangeViewModels(out VoltageRangeViewModel DrainSourceRangeViewModel, out VoltageRangeViewModel GateSourceRangeViewModel);
        protected abstract void SetVisualization(out D3VisualizationViewModel visualVM);
        
        
        protected override void ClearVisualization()
        {
            ExecuteInUIThread(() => Visualization.Clear());
        }
        
       
    }
}
