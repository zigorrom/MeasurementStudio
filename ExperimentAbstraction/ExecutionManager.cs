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
    public class ExecutionManager 
    {
        public ExecutionManager()
        {
            _executionList = new List<IExecutable>();
            _executionProgress = new Progress<ExecutionReport>();
            _cancellationSource = new CancellationTokenSource();
            _pauseTokenSource = new PauseTokenSource();
        }

        private List<IExecutable> _executionList;
        private IProgress<ExecutionReport> _executionProgress;
        private CancellationTokenSource _cancellationSource;
        private PauseTokenSource _pauseTokenSource;
        private IExecutable _currentExecutable;
        public void Start()
        {
            //Task.Factory.StartNew(new Action<PauseToken>((pauseToken)=>ExecutionLoop(_cancellationSource.Token,_pauseTokenSource.Token),_pauseTokenSource.Token, _cancellationSource.Token)
            var pauseToken = _pauseTokenSource.Token;
            var cancellationToken = _cancellationSource.Token;
            //Task.Factory.StartNew(() => ExecutionLoop(cancellationToken, pauseToken), cancellationToken);
        }

        private void ExecutionLoop(IProgress<ExecutionReport> progress , CancellationToken cancellationToken, PauseToken pauseToken)
        {
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

        }

        public void Pause()
        {

        }

        public void Resume()
        {

        }


        




    }
}
