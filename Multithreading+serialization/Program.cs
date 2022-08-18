using System;
using Multithreading_serialization;


class Program
{
    public static int _counter = 100;
    public static readonly string _path = "CountingData.dat";
    static void Main(string[] args)
    {
        var fileInfo = new FileInfo(_path);
        if (fileInfo.Exists)
        {
            var fileData = SerializationMethods.LoadFromBinaryFile(_path);
            if(fileData.lastCount > 0)
            {
                Console.WriteLine($"В прошлый раз вы остановились на счете {fileData.lastCount}, потоков было {fileData.threadCount} - хотите продолжить?" +
                    $"\n" +
                    $"Если да - введите Y, если хотите начать сначала, то введите N");

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

        Console.WriteLine("Сколько потоков хочешь запустить?");

        var threadCountRequest = 0;

        while (!int.TryParse(Console.ReadLine(), out threadCountRequest) | threadCountRequest == 0)
            Console.WriteLine("Шалите, милок! Даю вам еще попытку");

        var countingData = ThreadManager.ManageThreads(threadCountRequest, _counter);

        SerializationMethods.SaveAsBinaryFormat(countingData, _path);

        Console.ReadKey();
    }
}



