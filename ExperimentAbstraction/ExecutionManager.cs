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
    public class ExecutionManager:ICollection<IExecutable> 
    {
        public ExecutionManager()
        {
            _executionList = new List<IExecutable>();
            _cancellationSource = new CancellationTokenSource();
            _pauseTokenSource = new PauseTokenSource();
            _executionProgress = new Progress<ExecutionReport>();
            _executionProgress.ProgressChanged += OnExecutionProgressChanged;
        }

        void OnExecutionProgressChanged(object sender, ExecutionReport e)
        {
            var handler = executionProgressChanged;
            if( handler != null)
            {
                handler(sender, e);
            }
        }

        

        public event EventHandler<ExecutionReport> executionProgressChanged;

        private List<IExecutable> _executionList;
        private Progress<ExecutionReport> _executionProgress;
        private CancellationTokenSource _cancellationSource;
        private PauseTokenSource _pauseTokenSource;
        private IExecutable _currentExecutable;
        private Task _executionLoopTask;

        public void Start()
        {
            //Task.Factory.StartNew(new Action<PauseToken>((pauseToken)=>ExecutionLoop(_cancellationSource.Token,_pauseTokenSource.Token),_pauseTokenSource.Token, _cancellationSource.Token)
            var pauseToken = _pauseTokenSource.Token;
            var cancellationToken = _cancellationSource.Token;
            _executionLoopTask = Task.Factory.StartNew(()=> ExecutionLoop(_executionProgress, cancellationToken,pauseToken),cancellationToken);
            //Task.Factory.StartNew(() => ExecutionLoop(cancellationToken, pauseToken), cancellationToken);
        }

        private async void ExecutionLoop(IProgress<ExecutionReport> progress , CancellationToken cancellationToken, PauseToken pauseToken)
        {
            var initialTask = Task.FromResult(ExecutionReport.Empty);
                       
            await pauseToken.WaitWhilePausedAsync();
            cancellationToken.ThrowIfCancellationRequested();
            
            foreach (var task in _executionList)
            {
                var localItem = task;
                initialTask = initialTask.ContinueWith(prevRes => localItem.Execute(progress, cancellationToken, pauseToken));
            }


            //var tcs = new TaskCompletionSource<bool>();
            //tcs.SetResult(true);
            //Task<bool> ret = tcs.Task;

            //foreach (var executable in _executionList)
            //{
            //    var localItem = executable;
            //    ret = ret.ContinueWith(t => executable.Execute( ).Unwrap();
            //}
        }

        public void Abort()
        {
            _pauseTokenSource.IsPaused = false;
            _cancellationSource.Cancel();
            _executionLoopTask.Wait();
        }

        public void Pause()
        {
            _pauseTokenSource.IsPaused = true;
        }

        public void Resume()
        {
            _pauseTokenSource.IsPaused = false;
        }



        public void Add(IExecutable item)
        {
            _executionList.Add(item);
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(IExecutable item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(IExecutable[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public int Count
        {
            get { throw new NotImplementedException(); }
        }

        public bool IsReadOnly
        {
            get { throw new NotImplementedException(); }
        }

        public bool Remove(IExecutable item)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<IExecutable> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
