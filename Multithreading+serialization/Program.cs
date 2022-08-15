using System;
using Multithreading_serialization;
using System.Runtime.Serialization.Formatters.Binary;

class Program
{
    public static int lastCount;
    public static CancellationTokenSource cts = new CancellationTokenSource();
    public static Progress<int> progress = new Progress<int>();
  

   static void Main(string[] args)
    {


        Console.WriteLine("Сколько потоков хочешь запустить?");

        var threadCountRequest = 0;

        //var progress = new Progress<int>();
        progress.ProgressChanged += ReportPrpgress;


        while (!int.TryParse(Console.ReadLine(), out threadCountRequest) | threadCountRequest == 0)
            Console.WriteLine("Шалите, милок! Даю вам еще попытку");

        Thread[] threads = new Thread[threadCountRequest];

        Console.WriteLine("Как захочешь остановиться - нажми ESC");

        CancellationTokenSource cts = new CancellationTokenSource();


        for (int i = 0; i < threadCountRequest; i++)
        {
            var para = new ThreadParams();
            para.a = cts.Token;
            para.b = progress;

            var t = new Thread(new ParameterizedThreadStart(PerformMethods.CountFromMillionToOne));
            //var t = new Thread(() => { lastCount = PerformMethods.CountFromMillionToOne(cts.Token); });

            threads[i] = t;

            t.Start(para);
        }

        var threadWorkingStates = new[] { ThreadState.WaitSleepJoin, ThreadState.Running, ThreadState.Unstarted, ThreadState.Background };

        while (threads.Any(x => threadWorkingStates.Contains(x.ThreadState)) && !cts.IsCancellationRequested)
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
        countingData.lastCount = lastCount;

        PerformMethods.SaveAsBinaryFormat(countingData, "CountingData.dat");

        PerformMethods.LoadFromBinaryFile("CountingData.dat");

        Console.ReadKey();
    }

    private static void ReportPrpgress(object? sender, int e)
    {
        lastCount = e;
        Console.WriteLine($"Here are result {e}");
    }


}



