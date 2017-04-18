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
    #region OldVersion
    //public class SequentialTaskExecutionManager:ICollection<IExecutable>, IExecutionManager 
    //{
    //    public SequentialTaskExecutionManager()
    //    {
    //        _executionList = new List<IExecutable>();
    //        _cancellationSource = new CancellationTokenSource();
    //        _pauseTokenSource = new PauseTokenSource();
    //        _executionProgress = new Progress<ExecutionReport>();
    //        _executionProgress.ProgressChanged += OnExecutionProgressChanged;
    //        IsRunning = false;
    //    }

    //    private void ResetControlTokens()
    //    {
    //        _cancellationSource = new CancellationTokenSource();
    //    }

    //    void OnExecutionProgressChanged(object sender, ExecutionReport e)
    //    {
    //        var handler = ExecutionProgressChanged;
    //        if( handler != null)
    //        {
    //            handler(sender, e);
    //        }
    //    }

    //    void OnNewExecutionStarted(object sender, IExecutable e)
    //    {
    //        var handler = NewExecutableStarted;
    //        if(handler != null)
    //        {
    //            handler(sender, e);
    //        }
    //    }

    //    void OnExecutionLoopStarted(object sender, EventArgs e)
    //    {
    //        var handler = ExecutionLoopStarted;
    //        if(handler!=null)
    //        {
    //            handler(sender, e);
    //        }
    //    }

    //    void OnExecutionLoopFinished(object sender, EventArgs e)
    //    {
    //        var handler = ExecutionLoopFinished;
    //        if (handler != null)
    //        {
    //            handler(sender, e);
    //        }
    //    }




    //    public event EventHandler<ExecutionReport> ExecutionProgressChanged;
    //    public event EventHandler<IExecutable> NewExecutableStarted;
    //    public event EventHandler ExecutionLoopStarted;
    //    public event EventHandler ExecutionLoopFinished;




    //    private List<IExecutable> _executionList;
    //    private Progress<ExecutionReport> _executionProgress;
    //    private CancellationTokenSource _cancellationSource;
    //    private PauseTokenSource _pauseTokenSource;
    //    private Task _executionLoopTask;
    //    public bool IsRunning { get; private set; }


    //    public void Start()
    //    {
    //        //Task.Factory.StartNew(new Action<PauseToken>((pauseToken)=>ExecutionLoop(_cancellationSource.Token,_pauseTokenSource.Token),_pauseTokenSource.Token, _cancellationSource.Token)
    //        var pauseToken = _pauseTokenSource.Token;
    //        var cancellationToken = _cancellationSource.Token;
    //        _executionLoopTask = Task.Factory.StartNew(()=> ExecutionLoop(_executionProgress, cancellationToken,pauseToken),cancellationToken);
    //        //Task.Factory.StartNew(() => ExecutionLoop(cancellationToken, pauseToken), cancellationToken);
    //    }

    //    private void ExecutionLoop(IProgress<ExecutionReport> progress , CancellationToken cancellationToken, PauseToken pauseToken)
    //    {
    //        IsRunning = true;
    //        Task initialTask = null;
    //        var action = new Action<object, IExecutable>((o, e) =>
    //        {
    //            OnNewExecutionStarted(o, e);
    //            e.Execute(progress, cancellationToken, pauseToken);
    //        });
    //        pauseToken.WaitWhilePausedAsync().Wait();
    //        cancellationToken.ThrowIfCancellationRequested();

    //        #region another version - not working
    //        //var executionEnumerator = _executionList.GetEnumerator();
    //        //if (!executionEnumerator.MoveNext())
    //        //    return;

    //        //initialTask = Task.Factory.StartNew(() => executionEnumerator.Current.Execute(progress, cancellationToken, pauseToken));
    //        //while (executionEnumerator.MoveNext())
    //        //{
    //        //    initialTask = initialTask.ContinueWith((t) => executionEnumerator.Current.Execute(progress, cancellationToken, pauseToken));
    //        //}
    //        #endregion
    //        try
    //        {
    //            initialTask = Task.Factory.StartNew(() => OnExecutionLoopStarted(this, EventArgs.Empty), cancellationToken);
    //            //OnExecutionLoopStarted(this, EventArgs.Empty);

    //            foreach (var task in _executionList)
    //            {
    //                //try
    //                //{


    //                var localItem = task;
    //                initialTask = initialTask.ContinueWith((t) => action(this, localItem), cancellationToken);
    //                //if (initialTask == null)
    //                //    initialTask = Task.Factory.StartNew(() => action(this, localItem), cancellationToken);
    //                ////() =>
    //                ////{
    //                ////    OnNewExecutionStarted(this, localItem);
    //                ////    localItem.Execute(progress, cancellationToken, pauseToken);
    //                ////});
    //                //else
    //                //    initialTask = initialTask.ContinueWith((t) => action(this, localItem), cancellationToken);
    //                //    =>
    //                //{
    //                //    OnNewExecutionStarted(this, localItem);
    //                //    localItem.Execute(progress, cancellationToken, pauseToken);
    //                //});
    //                //}catch(OperationCanceledException e)
    //                //{

    //                //}
    //            }

    //            IsRunning = false;
    //            initialTask = initialTask.ContinueWith((t) => OnExecutionLoopFinished(this, EventArgs.Empty), cancellationToken);
    //            //OnExecutionLoopFinished(this, EventArgs.Empty);
    //        }catch (AggregateException e)
    //        {

    //        }
    //        finally
    //        {
                
    //        }
    //    }

    //    public void Abort()
    //    {
    //        try
    //        {
    //            _pauseTokenSource.IsPaused = false;
    //            _cancellationSource.Cancel();
    //            _executionLoopTask.Wait();
    //            ResetControlTokens();
    //        }catch(AggregateException e)
    //        {

    //        }
    //    }

    //    public void Pause()
    //    {
    //        _pauseTokenSource.IsPaused = true;
    //    }

    //    public void Resume()
    //    {
    //        _pauseTokenSource.IsPaused = false;
    //    }



    //    public void Add(IExecutable item)
    //    {
    //        _executionList.Add(item);
    //    }

    //    public void Clear()
    //    {
    //        _executionList.Clear();
    //    }

    //    public bool Contains(IExecutable item)
    //    {
    //        return _executionList.Contains(item);
    //    }

    //    public void CopyTo(IExecutable[] array, int arrayIndex)
    //    {
    //        _executionList.CopyTo(array, arrayIndex);
    //    }

    //    public int Count
    //    {
    //        get { return _executionList.Count; }
    //    }

    //    public bool IsReadOnly
    //    {
    //        get { return true; }
    //    }

    //    public bool Remove(IExecutable item)
    //    {
    //        return _executionList.Remove(item);
    //    }

    //    public IEnumerator<IExecutable> GetEnumerator()
    //    {
    //        return _executionList.GetEnumerator();
    //    }

    //    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
    //    {
    //        return GetEnumerator();
    //    }
    //}
    #endregion

    public class SequentialTaskExecutionManager: SingleTaskExecutionManager, ICollection<IExecutable>, IExecutionManager
    {
        public SequentialTaskExecutionManager():base()
        {
            _executionList = new List<IExecutable>();
        }
        private List<IExecutable> _executionList;

        protected override void ExecutionLoop(IProgress<ExecutionReport> progress, CancellationToken cancellationToken, PauseToken pauseToken)
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

                foreach (var task in _executionList)
                {
                    var localItem = task;
                    initialTask = initialTask.ContinueWith((t) => action(this, localItem), cancellationToken);
                }

                IsRunning = false;
                initialTask = initialTask.ContinueWith((t) => OnExecutionLoopFinished(this, EventArgs.Empty), cancellationToken);
                //OnExecutionLoopFinished(this, EventArgs.Empty);
            }
            catch (AggregateException e)
            {

            }
            finally
            {

            }
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
