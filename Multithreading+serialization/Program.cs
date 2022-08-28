﻿using System;
using CountingEntities;
using CountingEntities.Models;
using Multithreading_serialization;


class Program
{
    public static int Counter = 20;
    public static int ThreadCountRequest = 0;
    public static readonly string Path = "CountingData.dat";
    static void Main(string[] args)
    {
        //var user = Environment.UserName;

        var user = "jix";

        LoadLastData(user);

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

        var userData = new UserData()
        {
            UserName = user,
            RequestDate = DateTime.Now
        };

        AddRecordToDB(countingData, userData);

        Console.ReadKey();
    }
    private static void LoadLastData(string user)
    {
        using (var context = new CountingDataEntities())
        {
            var foundCountingData = context.CountingData.Find(context.UserData
                .Where(d => d.UserName == user)
                .OrderByDescending(u => u.RequestDate)
                .Select(u => u.RequestId).FirstOrDefault());
            if (foundCountingData != null && foundCountingData.LastCount > 0)
            {
                Console.WriteLine($"В прошлый раз ты остановился на счете {foundCountingData.LastCount}, " +
                                  $"потоков было {foundCountingData.ThreadCount} - хочешь продолжить счет? \n" +
                                  $"Если да - введи Y, если хочешь начать сначала, то введи N");

                var response = "";

                while ((response = Console.ReadLine().ToLower()) != "y" && response != "n")
                {
                    Console.WriteLine("Шалите, милок! Даю вам еще попытку");
                }

                if (response == "y")
                {
                    Counter = foundCountingData.LastCount;
                    ThreadCountRequest = foundCountingData.ThreadCount;
                }
            }
        }
    }

    private static void AddRecordToDB(CountingData countingData, UserData userData)
    {
        using (var context = new CountingDataEntities())
        {
            context.CountingData.Add(countingData);
            context.UserData.Add(userData);
            context.SaveChanges();
        }
        Console.WriteLine("Данные добавлены в таблицы");
    }

}



