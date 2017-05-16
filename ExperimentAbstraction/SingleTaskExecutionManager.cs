using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ExperimentAbstraction
{

    //http://stackoverflow.com/questions/20422026/run-sequence-of-tasks-one-after-the-other
    // Pause pattern
    // https://blogs.msdn.microsoft.com/pfxteam/2013/01/13/cooperatively-pausing-async-methods/
    public class SingleTaskExecutionManager : IExecutionManager
    {
        public SingleTaskExecutionManager()
        {
            _cancellationSource = new CancellationTokenSource();
            _pauseTokenSource = new PauseTokenSource();
            _executionProgress = new Progress<ExecutionReport>();
            _executionProgress.ProgressChanged += OnExecutionProgressChanged;
            IsRunning = false;
        }

        private void ResetControlTokens()
        {
            _cancellationSource = new CancellationTokenSource();
        }

        protected virtual void OnExecutionProgressChanged(object sender, ExecutionReport e)
        {
            var handler = ExecutionProgressChanged;
            if (handler != null)
            {
                handler(sender, e);
            }
        }

        protected virtual void OnNewExecutionStarted(object sender, IExecutable e)
        {
            var handler = NewExecutableStarted;
            if (handler != null)
            {
                handler(sender, e);
            }
        }

        protected virtual void OnExecutionLoopStarted(object sender, EventArgs e)
        {
            var handler = ExecutionLoopStarted;
            if (handler != null)
            {
                handler(sender, e);
            }
        }

        protected virtual void OnExecutionLoopFinished(object sender, EventArgs e)
        {
            var handler = ExecutionLoopFinished;
            if (handler != null)
            {
                handler(sender, e);
            }
        }

        public event EventHandler<ExecutionReport> ExecutionProgressChanged;
        public event EventHandler<IExecutable> NewExecutableStarted;
        public event EventHandler ExecutionLoopStarted;
        public event EventHandler ExecutionLoopFinished;

        private IExecutable _executable;
        public IExecutable CurrentExecutable
        {
            get { return _executable; }
            set
            {
                _executable = value;
            }
        }

        private Progress<ExecutionReport> _executionProgress;
        private CancellationTokenSource _cancellationSource;
        private PauseTokenSource _pauseTokenSource;
        private Task _executionLoopTask;
        public bool IsRunning { get; protected set; }

        public virtual void Start()
        {
            var pauseToken = _pauseTokenSource.Token;
            var cancellationToken = _cancellationSource.Token;
            _executionLoopTask = Task.Factory.StartNew(() => ExecutionLoop(_executionProgress, cancellationToken, pauseToken), cancellationToken);
        }

        protected virtual void ExecutionLoop(IProgress<ExecutionReport> progress, CancellationToken cancellationToken, PauseToken pauseToken)
        {
            try
            {
                IsRunning = true;
                Task initialTask = null;
                var action = new Action<object, IExecutable>((o, e) =>
                {
                    OnNewExecutionStarted(o, e);
                    e.Execute(progress, cancellationToken, pauseToken);
                });
                pauseToken.WaitWhilePausedAsync().Wait();
                cancellationToken.ThrowIfCancellationRequested();

                initialTask = Task.Factory.StartNew(() => OnExecutionLoopStarted(this, EventArgs.Empty), cancellationToken);
                initialTask = initialTask.ContinueWith((t) => action(this, CurrentExecutable), cancellationToken);
                IsRunning = false;
                initialTask = initialTask.ContinueWith((t) => OnExecutionLoopFinished(this, EventArgs.Empty), cancellationToken);
            }
            catch (AggregateException e)
            {

            }
            finally
            {

            }
        }

        public virtual void Abort()
        {
            try
            {
                _pauseTokenSource.IsPaused = false;
                _cancellationSource.Cancel();
                _executionLoopTask.Wait();
                ResetControlTokens();
            }
            catch (AggregateException e)
            {

            }
        }

        public virtual void Pause()
        {
            _pauseTokenSource.IsPaused = true;
        }

        public virtual void Resume()
        {
            _pauseTokenSource.IsPaused = false;
        }




        public virtual void Wait()
        {
            if (_executionLoopTask != null)
                _executionLoopTask.Wait();
        }
    }
}

