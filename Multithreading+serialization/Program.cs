using System;
using Multithreading_serialization;
using System.Runtime.Serialization.Formatters.Binary;

class Program
{

    public static void Main(string[] args)
    {
        
        
        Console.WriteLine("Сколько потоков хочешь запустить?");

        int threadCountRequest = 0;

        while (!int.TryParse(Console.ReadLine(), out threadCountRequest) | threadCountRequest == 0)
            Console.WriteLine("Шалите, милок! Даю вам еще попытку");

        Thread[] threads = new Thread[threadCountRequest];

        Console.WriteLine("Как захочешь остановиться - нажми ESC");

        CancellationTokenSource cts = new CancellationTokenSource();

        for (int i = 0; i < threadCountRequest; i++)
        {
            var t = new Thread(new ParameterizedThreadStart(PerformMethods.CountFromMillionToOne));
            //var t = new Thread(() => { lastCount = PerformMethods.CountFromMillionToOne(cts.Token); });
            
            threads[i] = t;

            t.Start(cts.Token);
        }

        var threadWorkingStates = new[] { ThreadState.WaitSleepJoin, ThreadState.Running, ThreadState.Unstarted, ThreadState.Background };

        while(threads.Any(x => threadWorkingStates.Contains(x.ThreadState)) && !cts.IsCancellationRequested)
        {
            if (Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Escape)
            {

                cts.Cancel();
                break;
            }
        }

        cts.Dispose();

        var countingData = new CountingData();
        countingData.threadCount = threadCountRequest;
        countingData.lastCount = PerformMethods.lastCount;

        PerformMethods.SaveAsBinaryFormat(countingData, "CountingData.dat");
        
        PerformMethods.LoadFromBinaryFile("CountingData.dat");

        Console.ReadKey();
    }
}



