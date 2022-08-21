﻿using System;
using Multithreading_serialization;

class Program
{
    public static int Counter = 20;
    public static int ThreadCountRequest = 0;
    public static readonly string Path = "CountingData.dat";
    static void Main(string[] args)
    {
        LoadLastData();

        if (ThreadCountRequest == 0)
        {
            Console.WriteLine("Во сколько потоков хочешь посчитать?");

            while (!int.TryParse(Console.ReadLine(), out ThreadCountRequest) || ThreadCountRequest == 0)
            {
                Console.WriteLine("Шалите, милок! Даю вам еще попытку");
            }
        }

        var threadPerformer = new ThreadPerformer();

        var countingData = new CountingData()
        {
            LastCount = threadPerformer.PerformThreads(ThreadCountRequest, Counter),
            ThreadCount = ThreadCountRequest
        };

        Serialization.SaveAsBinaryFormat(countingData, Path);

        Console.ReadKey();
    }
    private static void LoadLastData()
    {
        var fileInfo = new FileInfo(Path);
        if (fileInfo.Exists)
        {
            var fileData = Serialization.LoadFromBinaryFile(Path);
            if (fileData.LastCount > 0)
            {
                Console.WriteLine($"В прошлый раз ты остановился на счете {fileData.LastCount}, " +
                    $"потоков было {fileData.ThreadCount} - хочешь продолжить счет? \n" +
                    $"Если да - введи Y, если хочешь начать сначала, то введи N");

                var response = "";

                while ((response = Console.ReadLine().ToLower()) != "y" && response != "n")
                {
                    Console.WriteLine("Шалите, милок! Даю вам еще попытку");
                }

                if (response == "y")
                {
                    Counter = fileData.LastCount;
                    ThreadCountRequest = fileData.ThreadCount;
                }
            }
        }
    }
}



