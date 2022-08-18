using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multithreading_serialization
{
    public class ThreadManager
    {
        static int lastCount;
        public static int _counter;
        static int nextCount = _counter;

        static Semaphore sem = new Semaphore(1, 1);
        public static CancellationTokenSource cts = new CancellationTokenSource();
        
        public static Progress<int> progress = new Progress<int>();

        public static ThreadState[] threadWorkingStates = new[] 
        { 
            ThreadState.WaitSleepJoin, 
            ThreadState.Running, 
            ThreadState.Unstarted, 
            ThreadState.Background 
        };

        public static CountingData ManageThreads(int threadCountRequest, int counter) 
        {
            _counter = counter;
            
            progress.ProgressChanged += ReportProgress;

            Thread[] threads = new Thread[threadCountRequest];
            Console.WriteLine("Как захочешь остановиться - нажми ESC");

            for (int i = 0; i < threadCountRequest; i++)
            {
                var launchThreadParameters = new ThreadParameters();
                launchThreadParameters.ctsToken = cts.Token;
                launchThreadParameters.progress = progress;

                var t = new Thread(new ParameterizedThreadStart(CountDown));
                //var t = new Thread(() => { lastCount = PerformMethods.CountFromMillionToOne(cts.Token); });

                threads[i] = t;

                t.Start(launchThreadParameters);
            }

            while (threads.Any(x => threadWorkingStates.Contains(x.ThreadState)) && !cts.IsCancellationRequested)
            {
                if (Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Escape)
                {
                    cts.Cancel();
                    break;
                }
            }

            cts.Dispose();
            return new CountingData(threadCountRequest, lastCount) ;
        }

        public static void CountDown(object parameters)
        {
            var threadParams = (ThreadParameters)parameters;
            var ct = (CancellationToken)threadParams.ctsToken;
            IProgress<int> progress = threadParams.progress;

            while (_counter > 0 && !ct.IsCancellationRequested)
            {
                sem.WaitOne();

                nextCount = _counter;

                if (ct.IsCancellationRequested || _counter < 0)
                {
                    progress.Report(nextCount);
                    return;
                }

                else
                {
                    nextCount = _counter;
                    Console.WriteLine($"Счет: {_counter} , ID#: {Thread.CurrentThread.ManagedThreadId}");

                    progress.Report(nextCount);

                    _counter--;

                    Thread.Sleep(200);
                }
                sem.Release();
            }
        }
        private static void ReportProgress(object? sender, int nextCount)
        {
            lastCount = nextCount;
        }
    }
}
