using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multithreading_serialization
{
    public class ThreadPerformer
    {
        private int _counter;
        private int _lastCount;
        private Semaphore _sem = new Semaphore(1, 1);
        private CancellationTokenSource _cts = new CancellationTokenSource();
        private Progress<int> _progress = new Progress<int>();
        
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

            while (_counter >= 0 && !_cts.IsCancellationRequested)
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
            var ct = threadParams.CtsToken;
            IProgress<int> progress = threadParams.Progress;
            
            while (_counter >= 0 && !ct.IsCancellationRequested)
            {
                _sem.WaitOne();

                progress.Report(_counter);

                if (ct.IsCancellationRequested || _counter < 0)
                {
                    return;
                }
                else
                {
                    Console.WriteLine($"Счет: {_counter} , ID#: {Thread.CurrentThread.ManagedThreadId}");
                    _counter--;
                    Thread.Sleep(170);
                }

                _sem.Release();
            }
        }
    }
}
