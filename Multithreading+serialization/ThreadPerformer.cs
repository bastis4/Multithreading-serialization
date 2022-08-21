using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multithreading_serialization
{
    public class ThreadPerformer
    {
        private static int _counter;
        private static int _nextCount;
        private static int _lastCount;
        private static Semaphore _sem = new Semaphore(1, 1);
        private static CancellationTokenSource _cts = new CancellationTokenSource();
        private static Progress<int> _progress = new Progress<int>();
        
        public int PerformThreads(int threadCountRequest, int counter) 
        {
            _counter = counter;
            
            _progress.ProgressChanged += (sender, nextCount) => _lastCount = nextCount;

            var threads = new Thread[threadCountRequest];

            Console.WriteLine("Как захочешь остановиться - нажми ESC");
            Console.WriteLine();

            for (int i = 0; i < threadCountRequest; i++)
            {
                var launchThreadParameters = new ThreadParameters
                {
                    CtsToken = _cts.Token,
                    Progress = _progress
                };

                var t = new Thread(new ParameterizedThreadStart(CountDown));
                //var t = new Thread(() => { lastCount = PerformMethods.CountFromMillionToOne(cts.Token); });

                threads[i] = t;

                t.Start(launchThreadParameters);
            }

            while (_lastCount >= 0 && !_cts.IsCancellationRequested)
            {
                if (Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Escape)
                {
                    _cts.Cancel();
                    break;
                }
            }

            _cts.Dispose();
            return _lastCount;
        }

        private void CountDown(object parameters)
        {
            var threadParams = (ThreadParameters)parameters;
            var ct = (CancellationToken)threadParams.CtsToken;
            IProgress<int> progress = threadParams.Progress;

            while (_counter >= 0 && !ct.IsCancellationRequested)
            {
                _sem.WaitOne();

                _nextCount = _counter;

                if (ct.IsCancellationRequested || _counter < 0)
                {
                    progress.Report(_nextCount);
                    return;
                }
                else
                {
                    _nextCount = _counter;
                    Console.WriteLine($"Счет: {_counter} , ID#: {Thread.CurrentThread.ManagedThreadId}");

                    progress.Report(_nextCount);

                    _counter--;

                    Thread.Sleep(200);
                }
                _sem.Release();
            }
        }
    }
}
