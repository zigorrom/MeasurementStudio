using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExperimentAbstraction
{
    public abstract class AbstractExperimentViewModel: INotifyPropertyChanged
    {
        #region PropertyEvents

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


        private IExperiment _experiment;
        public virtual IExperiment Experiment
        {
            get
            {
                return _experiment;
            }
            protected set
            {
                SetField(ref _experiment, value, "IVExperiment");
            }
        }

        public AbstractExperimentViewModel()
        {
            InitExperiment();
            if (Experiment == null)
                throw new ArgumentNullException("Experiment not defined");

            Experiment.ExperimentStarted += ExperimentStartedHandler;
            Experiment.ExperimentPaused += ExperimentPausedHandler;
            Experiment.ExperimentStopped += ExperimentStoppedHandler;
            Experiment.ExperimentFinished += ExperimentFinishedHandler;
            Experiment.ExperimentProgressChanged += ExperimentProgressChangedHandler;
        }

        protected abstract void ExperimentProgressChangedHandler(object sender, EventArgs e);
        protected abstract void ExperimentFinishedHandler(object sender, EventArgs e);
        protected abstract void ExperimentStoppedHandler(object sender, EventArgs e);
        protected abstract void InitExperiment();
        protected abstract void ExperimentPausedHandler(object sender, EventArgs e);
        protected abstract void ExperimentStartedHandler(object sender, EventArgs e);
        

        


    }
}
