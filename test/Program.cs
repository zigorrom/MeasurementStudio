
using NationalInstruments.VisaNS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.SelfHost;
using System.Web.Http;
using Hioki3432;
using Instruments;
using System.ServiceModel;
using MeasurementStudioWebApi;
using System.Threading;
using ExperimentViewer;
//using HelperExecutables.TimeDelay;


namespace test
{
    public sealed class Pool : IDisposable
    {
        public Pool(int size)
        {
            this._workers = new LinkedList<Thread>();
            for (var i = 0; i < size; ++i)
            {
                var worker = new Thread(this.Worker) { Name = string.Concat("Worker ", i) };
                worker.Start();
                this._workers.AddLast(worker);
            }
        }

        public void Dispose()
        {
            var waitForThreads = false;
            lock (this._tasks)
            {
                if (!this._disposed)
                {
                    GC.SuppressFinalize(this);

                    this._disallowAdd = true; // wait for all tasks to finish processing while not allowing any more new tasks
                    while (this._tasks.Count > 0)
                    {
                        Monitor.Wait(this._tasks);
                    }

                    this._disposed = true;
                    Monitor.PulseAll(this._tasks); // wake all workers (none of them will be active at this point; disposed flag will cause then to finish so that we can join them)
                    waitForThreads = true;
                }
            }
            if (waitForThreads)
            {
                foreach (var worker in this._workers)
                {
                    worker.Join();
                }
            }
        }

        public void QueueTask(Action task)
        {
            lock (this._tasks)
            {
                if (this._disallowAdd) { throw new InvalidOperationException("This Pool instance is in the process of being disposed, can't add anymore"); }
                if (this._disposed) { throw new ObjectDisposedException("This Pool instance has already been disposed"); }
                this._tasks.AddLast(task);
                Monitor.PulseAll(this._tasks); // pulse because tasks count changed
            }
        }

        private void Worker()
        {
            Action task = null;
            while (true) // loop until threadpool is disposed
            {
                lock (this._tasks) // finding a task needs to be atomic
                {
                    while (true) // wait for our turn in _workers queue and an available task
                    {
                        if (this._disposed)
                        {
                            return;
                        }
                        if (null != this._workers.First && object.ReferenceEquals(Thread.CurrentThread, this._workers.First.Value) && this._tasks.Count > 0) // we can only claim a task if its our turn (this worker thread is the first entry in _worker queue) and there is a task available
                        {
                            task = this._tasks.First.Value;
                            this._tasks.RemoveFirst();
                            this._workers.RemoveFirst();
                            Monitor.PulseAll(this._tasks); // pulse because current (First) worker changed (so that next available sleeping worker will pick up its task)
                            break; // we found a task to process, break out from the above 'while (true)' loop
                        }
                        Monitor.Wait(this._tasks); // go to sleep, either not our turn or no task to process
                    }
                }

                task(); // process the found task
                lock (this._tasks)
                {
                    this._workers.AddLast(Thread.CurrentThread);
                }
                task = null;
            }
        }

        private readonly LinkedList<Thread> _workers; // queue of worker threads ready to process actions
        private readonly LinkedList<Action> _tasks = new LinkedList<Action>(); // actions to be processed by worker threads
        private bool _disallowAdd; // set to true when disposing queue but there are still tasks pending
        private bool _disposed; // set to true when disposing queue and no more tasks are pending
    }


    //public static class Program
    //{
    //    static void Main()
    //    {
    //        using (var pool = new Pool(5))
    //        {
    //            var random = new Random();
    //            Action<int> randomizer = (index =>
    //            {
    //                Console.WriteLine("{0}: Working on index {1}", Thread.CurrentThread.Name, index);
    //                Thread.Sleep(random.Next(20, 400));
    //                Console.WriteLine("{0}: Ending {1}", Thread.CurrentThread.Name, index);
    //            });

    //            for (var i = 0; i < 40; ++i)
    //            {
    //                var i1 = i;
    //                pool.QueueTask(() => randomizer(i1));
    //            }
    //        }
    //    }
    //}

   

    class Program
    {
        class testAction: IExecutable
        {
            public testAction(string name)
            {
                this.name = name;
            }
            private string name;

            public void Execute()
            {
                throw new NotImplementedException();
            }

            public void Execute(object ExperimentStartObject, System.ComponentModel.DoWorkEventArgs e)
            {
                throw new NotImplementedException();
            }

            public void Execute(IProgress<ExecutionReport> progress, CancellationToken cancellationToken, PauseToken pauseToken)
            {
                ExecutionReport report = ExecutionReport.Empty;
                for (int i = 0; i < 100; i++)
                {
                    pauseToken.WaitWhilePausedAsync().Wait();
                    cancellationToken.ThrowIfCancellationRequested();
                    report = new ExecutionReport { ExperimentExecutionStatus = ExecutionStatus.Running, ExperimentProgress = i, ExperimentProgressMessage = String.Format("{0} running normally...", name) };
                    progress.Report(report);
                    Thread.Sleep(10);
                }
                //}catch(OperationCanceledException e)
                //{
                //    progress.Report(new ExecutionReport { ExperimentExecutionStatus = ExecutionStatus.Aborted, ExperimentProgress = 0, ExperimentProgressMessage = "aborted" });
                //}
                //return report;
            }

            public void Abort()
            {
                throw new NotImplementedException();
            }

            public void Pause()
            {
                throw new NotImplementedException();
            }

            public void Resume()
            {
                throw new NotImplementedException();
            }

            public bool IsRunning
            {
                get { throw new NotImplementedException(); }
            }

            public ExecutionStatus Status
            {
                get { throw new NotImplementedException(); }
            }

            public event EventHandler<ExecutionStatus> StatusChanged;

            public event EventHandler ExecutionStarted;

            public event EventHandler ExecutionAborted;

            public event System.ComponentModel.ProgressChangedEventHandler ProgressChanged;

            public event EventHandler ExecutionFinished;
        }

        //public static async Task SomeMethodAsync(PauseToken pause)
        //{
        //    for (int i = 0; i < 100; i++)
        //    {
        //        Console.WriteLine(i);
        //        await Task.Delay(100);
        //        await pause.WaitWhilePausedAsync();
        //    }
        //} 

        static void em_executionProgressChanged(object sender, ExecutionReport e)
        {
            Console.WriteLine("******************\r\n{0}\r\n{1}\r\n{2}\r\n******************", e.ExperimentProgress, e.ExperimentExecutionStatus, e.ExperimentProgressMessage);
            
            //Console.WriteLine("******************");
            //Console.WriteLine(e.ExperimentProgress);
            //Console.WriteLine(e.ExperimentExecutionStatus);
            //Console.WriteLine(e.ExperimentProgressMessage);
            //Console.WriteLine("******************");
        }
        static void em_NewExecutionStarted(object sender, IExecutable e)
        {
            Console.WriteLine(e.GetType());

        }
       
        static void Main(string[] args)
        {
            var em = new ExecutionManager();
            em.ExecutionProgressChanged += em_executionProgressChanged;
            
            //em.NewExecutableStarted += em_NewExecutionStarted;
            var task = new testAction("test1");
            //var waitTask = new TimeDelayExecutable() { TimeDelay = 10000 };

            var task2 = new testAction("test2");



            bool paused = false;
            em.Add(task);
            //em.Add(waitTask);
            em.Add(task2);
            em.Start();
         
            for (int i = 0; i < 100000000; i++)
            {
                if (i % 10 == 1)
                {
                    if (paused)
                    {
                        Console.WriteLine("Resume");
                        paused = false;
                        em.Resume();
                    }
                    else
                    {
                        Console.WriteLine("Pause");
                        paused = true;
                        em.Pause();
                    }
                }
                if (paused)
                    Thread.Sleep(20);
                Thread.Sleep(10);
            } 

            em.Abort();
            Console.WriteLine("Aborted");

            Console.ReadLine();

        }

       

        

        

    }
}
 