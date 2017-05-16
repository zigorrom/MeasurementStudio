using ChannelSwitchLibrary;
//using ChannelSwitchLibrary;
using ExperimentAbstraction;
using Instruments;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ChannelSwitchExecutable
{
    public class ChannelSwitchExecutable:INewExperiment
    {
        public ChannelSwitchExecutable(ChannelSwitchExecutableViewModel viewModel)
        {
            ChannelSwitchViewModel= viewModel;
            Name = "Channel Switch executable";
        }

        private ChannelSwitchExecutableViewModel ChannelSwitchViewModel { get; set; }

        public void Execute(IProgress<ExecutionReport> progress, CancellationToken cancellationToken, PauseToken pauseToken)
        {
            //ExecutionReport report = ExecutionReport.Empty;
            InitializeExperiment();
            
            OnExecutionStarted(this, new EventArgs());
            IsRunning = true;
            Status = ExecutionStatus.Running;
            OnStatusChanged(this, Status);
            try
            {
                var chSwitch = new ArduinoChannelSwitch("ChannelSwitch", "ACS", ChannelSwitchViewModel.InstrumentResource.Resource);
                chSwitch.SwitchChannel((short)ChannelSwitchViewModel.SelectedChannel, ChannelSwitchViewModel.SelectedChannelState);
                chSwitch.Dispose();
                //HandleMessage(String.Format("Changing transistor to {0}", ChannelSwitchViewModel.SelectedChannel));
                //ChannelSwitchViewModel.SwitchToChannel(ChannelSwitchViewModel.SelectedChannel);
                //var channelSwitch = new SyncronousChannelSwitch();
                //channelSwitch.Setup();//ChannelSwitchViewModel.InstrumentResource.Resource);

                //for (short i = 0; i < 32; i++)
                //{
                //    while (channelSwitch.RunLoop) channelSwitch.SwitchToChannel(i, false);
                //}

                //while (channelSwitch.RunLoop) channelSwitch.SwitchToChannel((short)ChannelSwitchViewModel.SelectedChannel, true);
                //channelSwitch.Exit();
                //ChannelSwitchLibrary.ChannelSwitch sw = new ChannelSwitchLibrary.ChannelSwitch();
                //sw.Initialize(ChannelSwitchViewModel.InstrumentResource.Resource);
                //if(sw.Initialized)
                //{
                //    //need to implement the memory of state
                //    for (short i = 0; i < 32; i++)
                //    {
                //        sw.Switch(i, false);
                //    }

                //    sw.Switch((short)ChannelSwitchViewModel.SelectedChannel, true);
                //}
                Thread.Sleep(200);
                //perform here hardware channel switch
            }
            catch (OperationCanceledException e)
            {
                Status = ExecutionStatus.Aborted;
                HandleError(e);
                OnExecutionAborted(this, new EventArgs());
            }
            catch (Exception e)
            {
                Status = ExecutionStatus.Failed;
                HandleError(e);
            }
            finally
            {
                IsRunning = false;
                OnStatusChanged(this, Status);
                OnExecutionFinished(this, new EventArgs());
            }
        }

        protected void HandleError(Exception e)
        {
            ChannelSwitchViewModel.ErrorHandler(e);
        }
        protected void HandleMessage(string Message)
        {
            ChannelSwitchViewModel.MessageHandler(Message);
        }

       public bool IsRunning
        {
            get;
            private set;
        }

        public ExecutionStatus Status
        {
            get;
            private set;
        }

        #region events
        public event EventHandler<ExecutionStatus> StatusChanged;
        private void OnStatusChanged(object sender, ExecutionStatus status)
        {
            var handler = StatusChanged;
            if (handler != null)
            {
                handler(sender, status);
            }
        }

        public event EventHandler ExecutionStarted;

        protected virtual void OnExecutionStarted(object sender, EventArgs e)
        {
            var handler = ExecutionStarted;
            if (handler != null)
                handler(sender, e);
        }

        public event EventHandler ExecutionAborted;
        protected virtual void OnExecutionAborted(object sender, EventArgs e)
        {
            var handler = ExecutionAborted;
            if (handler != null)
                handler(sender, e);
        }


        public event ProgressChangedEventHandler ProgressChanged;
        protected virtual void OnProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            var handler = ProgressChanged;
            if (handler != null)
                handler(sender, e);
        }

        public event EventHandler ExecutionFinished;
        protected virtual void OnExecutionFinished(object sender, EventArgs e)
        {
            var handler = ExecutionFinished;
            if (handler != null)
                handler(sender, e);
        }
        #endregion

        public void InitializeExperiment()
        {
            //throw new NotImplementedException();
        }

        public void InitializeInstruments()
        {
            //throw new NotImplementedException();
        }

        public void OwnInstruments()
        {
            //throw new NotImplementedException();
        }

        public void ReleaseInstruments()
        {
            //throw new NotImplementedException();
        }

        public void FinalizeExperiment()
        {
            //throw new NotImplementedException();
        }

        public bool SimulateExperiment
        {
            get;
            set;
        }

        public object ViewModel
        {
            get { return ChannelSwitchViewModel; }
        }

        public string Name
        {
            get;
            private set;
        }
        public bool Equals(IInstrumentOwner other)
        {
            if (other.Name == Name)
                if (Object.ReferenceEquals(this, other))
                    return true;
            return false;
        }
        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }

       
    }
}
