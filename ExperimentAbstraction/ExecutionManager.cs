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
            var handler = ExecutionProgressChanged;
            if( handler != null)
            {
                handler(sender, e);
            }
        }

        public event EventHandler<ExecutionReport> ExecutionProgressChanged;

        private List<IExecutable> _executionList;
        private Progress<ExecutionReport> _executionProgress;
        private CancellationTokenSource _cancellationSource;
        private PauseTokenSource _pauseTokenSource;
        
        private Task _executionLoopTask;

        public void Start()
        {
            //Task.Factory.StartNew(new Action<PauseToken>((pauseToken)=>ExecutionLoop(_cancellationSource.Token,_pauseTokenSource.Token),_pauseTokenSource.Token, _cancellationSource.Token)
            var pauseToken = _pauseTokenSource.Token;
            var cancellationToken = _cancellationSource.Token;
            _executionLoopTask = Task.Factory.StartNew(()=> ExecutionLoop(_executionProgress, cancellationToken,pauseToken),cancellationToken);
            //Task.Factory.StartNew(() => ExecutionLoop(cancellationToken, pauseToken), cancellationToken);
        }

        private void ExecutionLoop(IProgress<ExecutionReport> progress , CancellationToken cancellationToken, PauseToken pauseToken)
        {
            Task initialTask = null;
            pauseToken.WaitWhilePausedAsync().Wait();
            cancellationToken.ThrowIfCancellationRequested();

            #region another version - not working
            //var executionEnumerator = _executionList.GetEnumerator();
            //if (!executionEnumerator.MoveNext())
            //    return;

            //initialTask = Task.Factory.StartNew(() => executionEnumerator.Current.Execute(progress, cancellationToken, pauseToken));
            //while (executionEnumerator.MoveNext())
            //{
            //    initialTask = initialTask.ContinueWith((t) => executionEnumerator.Current.Execute(progress, cancellationToken, pauseToken));
            //}
            #endregion

            foreach (var task in _executionList)
            {
                var localItem = task;
                if (initialTask == null)
                    initialTask = Task.Factory.StartNew(() => localItem.Execute(progress, cancellationToken, pauseToken));
                else
                    initialTask = initialTask.ContinueWith((t) => localItem.Execute(progress, cancellationToken, pauseToken));
            }
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
            _executionList.Clear();
        }

        public bool Contains(IExecutable item)
        {
            return _executionList.Contains(item);
        }

        public void CopyTo(IExecutable[] array, int arrayIndex)
        {
            _executionList.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return _executionList.Count; }
        }

        public bool IsReadOnly
        {
            get { return true; }
        }

        public bool Remove(IExecutable item)
        {
            return _executionList.Remove(item);
        }

        public IEnumerator<IExecutable> GetEnumerator()
        {
            return _executionList.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
