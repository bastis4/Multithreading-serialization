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

        PerformMethods.sem.SafeWaitHandle.Equals(threadCountRequest);

        Thread[] threads = new Thread[threadCountRequest];

        Console.WriteLine("Как захочешь остановиться - нажми ESC");

        CancellationTokenSource cts = new CancellationTokenSource();

        do
        {
            for (int i = 0; i < threadCountRequest; i++)
            {
                if (Console.KeyAvailable)
                {
                    break;
                }
                else
                {
                    new Thread(new ParameterizedThreadStart(PerformMethods.CountFromMillionToOne)).Start(cts.Token);
                }
            }

        }
        while (Console.ReadKey(true).Key != ConsoleKey.Escape);
        cts.Cancel();


        var countingData = new CountingData();
        countingData.threadCount = threadCountRequest;

        PerformMethods.SaveAsBinaryFormat(countingData, "CountingData.dat");
        PerformMethods.LoadFromBinaryFile("CountingData.dat");

        Console.ReadKey();
    }
}



