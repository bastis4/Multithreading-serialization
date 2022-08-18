using System;
using Multithreading_serialization;


class Program
{
    public static int _counter = 1000;
    public static readonly string _path = "CountingData.dat";
    static void Main(string[] args)
    {
        var fileInfo = new FileInfo(_path);
        if (fileInfo.Exists)
        {
            var fileData = SerializationMethods.LoadFromBinaryFile(_path);
            if(fileData.lastCount > 0)
            {
                Console.WriteLine($"В прошлый раз ты остановился на счете {fileData.lastCount}, " +
                    $"потоков было {fileData.threadCount} - хочешь продолжить счет? \n" +
                    $"Если да - введи Y, если хочешь начать сначала, то введи N");

                var response = "";

                while((response = Console.ReadLine().ToLower()) != "y" && response != "n")
                  
                {
                    Console.WriteLine("Шалите, милок! Даю вам еще попытку");
                }
                    
                if(response == "y")
                {
                    _counter = fileData.lastCount;
                }
            }
        }

        Console.WriteLine("Во сколько потоков хочешь посчитать?");

        var threadCountRequest = 0;

        while (!int.TryParse(Console.ReadLine(), out threadCountRequest) | threadCountRequest == 0)
        {
            Console.WriteLine("Шалите, милок! Даю вам еще попытку");
        }

        var countingData = ThreadManager.ManageThreads(threadCountRequest, _counter);

        SerializationMethods.SaveAsBinaryFormat(countingData, _path);

        Console.ReadKey();
    }
}



