using ExperimentAbstraction;
using Helper.Ranges.RangeHandlers;
using InstrumentAbstraction.InstrumentInterfaces;
using Keithley24xxNamespace;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace IVCharacterization
{

    

    public class IVCharacterizationExperiment:AbstractExperiment
    {
        private const string ExperimentName = "IV characterization";
        private IVCharacteristicTypeEnum m_currentCharacteristic;
        private IVMainView m_control;
        private IVMainViewModel m_transferVM;
        private IVMainViewModel m_outputVM;
        private ISourceMeasurementUnit m_gateSMU;
        private ISourceMeasurementUnit m_drainSMU;

        

        public IVCharacterizationExperiment():base(ExperimentName)
        {
            m_control = new IVMainView();
            m_control.DataContextChangeDemand += a_DataContextChangeDemand;
            m_transferVM = new IVMainViewModel(IVCharacteristicTypeEnum.Transfer);
            m_outputVM = new IVMainViewModel(IVCharacteristicTypeEnum.Output);
            SetContext(IVCharacteristicTypeEnum.Output);
            m_control.ControlButtons.StartButton.Click += StartButton_Click;
        }

        void StartButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Start();
        }

        void SetContext( IVCharacteristicTypeEnum e)
        {
            m_currentCharacteristic = e;
            switch (e)
            {
                case IVCharacteristicTypeEnum.Output:
                    m_control.DataContext = m_outputVM;
                    break;
                case IVCharacteristicTypeEnum.Transfer:
                    m_control.DataContext = m_transferVM;
                    break;
                default:
                    break;
            }
        }

        void a_DataContextChangeDemand(object sender, IVCharacteristicTypeEnum e)
        {
            SetContext(e);
        }

        public override void InitializeExperiment()
        {
            m_drainSMU = new Keithley24xx("drain", "sda", "");
            m_gateSMU = new Keithley24xx("gate", "dsa", "");

            ///
            /// Add here routines with InstrumentHandler;
            ///


            //throw new NotImplementedException();
        }

        public override void InitializeInstruments()
        {
            throw new NotImplementedException();
        }

        public override void ReleaseInstruments()
        {
            throw new NotImplementedException();
        }

        public override void Start()
        {
            AbstractDoubleRangeHandler outer;
            AbstractDoubleRangeHandler inner;
            switch (m_currentCharacteristic)
            {
                case IVCharacteristicTypeEnum.Output:
                    {

                        inner = privateViewModel.DSRangeHandlerViewModel.RangeHandler;
                        inner.RepeatCounts = privateViewModel.DSRangeHandlerViewModel.RepeatCounts;
                        inner.Range = privateViewModel.DSRangeViewModel.Range;

                        outer = privateViewModel.GSRangeHandlerViewModel.RangeHandler;
                        outer.RepeatCounts = privateViewModel.GSRangeHandlerViewModel.RepeatCounts;
                        outer.Range = privateViewModel.GSRangeViewModel.Range;
                    }break;
                case IVCharacteristicTypeEnum.Transfer:
                    {
                        inner = privateViewModel.GSRangeHandlerViewModel.RangeHandler;
                        inner.RepeatCounts = privateViewModel.GSRangeHandlerViewModel.RepeatCounts;
                        inner.Range = privateViewModel.GSRangeViewModel.Range;

                        outer = privateViewModel.DSRangeHandlerViewModel.RangeHandler;
                        outer.RepeatCounts = privateViewModel.DSRangeHandlerViewModel.RepeatCounts;
                        outer.Range = privateViewModel.DSRangeViewModel.Range;
                    } break;
                default:
                    return;
            }
            //privateViewModel.DSRangeHandlerViewModel.RangeHandler

            foreach (var outer_val in outer)
            {
                foreach (var inner_val in inner)
                {
                    Debug.WriteLine("{0},{1}", outer_val, inner_val);
                }
            }

            Debug.WriteLine("");

            //throw new NotImplementedException();
        }

        public override int ReportProgress()
        {
            throw new NotImplementedException();
        }

        public override void Abort()
        {
            throw new NotImplementedException();
        }

        public override void OwnInstruments()
        {
            throw new NotImplementedException();
        }

        public override object ViewModel
        {
            get { return m_control.DataContext; }
        }
        private IVMainViewModel privateViewModel
        {
            get { return (IVMainViewModel)m_control.DataContext; }
        }


        public override UserControl Control
        {
            get { return m_control; }
        }
    }
}
