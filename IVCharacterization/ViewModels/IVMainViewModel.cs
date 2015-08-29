
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
    public struct IVCharacteristicNames
    {
        public const string OutputCharacteristic = "Output";
        public const string TransferCharacteristic = "Transfer";
    }

    //public enum IVCharacteristicTypeEnum
    //{
    //    Output,
    //    Transfer
    //}

    public abstract class IVMainViewModel : INotifyPropertyChanged
    {
        //private IVCharacteristicTypeEnum m_IVCharacteristicType;
        //public IVCharacteristicTypeEnum IVCharacteristicType
        //{
        //    get { return m_IVCharacteristicType; }
        //    set
        //    {
        //        SetField(ref m_IVCharacteristicType, value, "IVCharacteristicType");
        //    }
        //}

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

        public  IExperiment Experiment { get; protected set; }

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

        public IVMainViewModel()//IVCharacteristicTypeEnum characteristicType = IVCharacteristicTypeEnum.Output)
        {
            //m_IVCharacteristicType = characteristicType;
            DSRangeViewModel = new RangeViewModel(new Voltage(), new Voltage(), new Voltage());
            GSRangeViewModel = new RangeViewModel(new Voltage(), new Voltage(), new Voltage());

            DSRangeHandlerViewModel = new RangeHandlerViewModel();
            GSRangeHandlerViewModel = new RangeHandlerViewModel();

            Visualization = new D3VisualizationViewModel();

            ExperimentControlButtons = new ControlButtonsViewModel();
            GlobalIsEnabled = true;

            
        }

       

        private IExperiment _ivExperiment;
        public IExperiment IVExperiment
        {
            get
            {
                return _ivExperiment;
            }
            protected set
            {
                SetField(ref _ivExperiment, value, "IVExperiment");
            }
        }

        
        #region PropertyEvents
        //public event EventHandler<IVCharacteristicTypeEnum> ChangeIVCharacterizationViewModel;
        //private void OnChangeIVCharacterizationViewModel(IVCharacteristicTypeEnum CharacteristicType)
        //{
        //    var handler = ChangeIVCharacterizationViewModel;
        //    if (handler != null)
        //        handler(this, CharacteristicType);
        //}

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
    }
}
