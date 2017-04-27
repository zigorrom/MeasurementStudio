using ExperimentAbstraction;
using Helper.AbstractPropertyChangedClass;
using Helper.NewExperimentWindow;
using Microsoft.TeamFoundation.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace ScenarioBuilder.ViewModel
{
    public class ScenarioExperimentDataContext : AbstractNotifyPropertyChangedClass, IExperimentDataContext
    {
        public ScenarioExperimentDataContext()
        {

        }

        private string _experimentName;
        public string ExperimentName
        {
            get
            {
                return _experimentName;
            }
            set
            {
                SetField(ref _experimentName, value, "ExperimentName");
            }
        }

        private int _measurementCount;
        public int MeasurementCount
        {
            get
            {
                return _measurementCount;
            }
            set
            {
                SetField(ref _measurementCount, value, "MeasurementCount");
            }
        }

        private string _measurementName;
        public string MeasurementName
        {
            get
            {
                return _measurementName;
            }
            set
            {
                SetField(ref _measurementName, value, "MeasurementName");
            }
        }

        private string _workingDirectory;
        public string WorkingDirectory
        {
            get
            {
                return _workingDirectory;
            }
            set
            {
                SetField(ref _workingDirectory, value, "WorkingDirectory");
            }
        }


        #region Commands
        private ICommand _createNewExperiment;

        public ICommand CreateNewExperiment
        {
            get
            {
                return _createNewExperiment ?? (_createNewExperiment = new RelayCommand(() =>
                {
                    ExperimentName = GetExperimentName();
                    
                }));
            }
        }
        private string GetExperimentName()
        {
            var d = new NewExperimentControl(ExperimentName);
            if (d.ShowDialog().Value)
                return d.ExperimentName;
            return String.Empty;
        }

        private System.Windows.Forms.FolderBrowserDialog fbd = new System.Windows.Forms.FolderBrowserDialog();
        private ICommand _selectWorkingDirectory;
        public ICommand SelectWorkingDirectory
        {
            get
            {
                return _selectWorkingDirectory ?? (_selectWorkingDirectory = new RelayCommand(() =>
                {
                    //var fbd = new System.Windows.Forms.FolderBrowserDialog();

                    if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        WorkingDirectory = fbd.SelectedPath;
                    }

                }));
            }
        }

        private ICommand _openWorkingDirectory;
        public ICommand OpenWorkingDirectory
        {
            get
            {
                return _openWorkingDirectory ?? (_openWorkingDirectory = new RelayCommand((o) =>
                {
                    try
                    {
                        System.Diagnostics.Process.Start(WorkingDirectory);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        //ErrorHandler(ex);
                    }

                }, (o) =>
                {
                    return !String.IsNullOrEmpty(WorkingDirectory);
                }));
            }
        }

        #endregion

    }
}
