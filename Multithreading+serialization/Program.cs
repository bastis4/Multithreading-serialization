using System;
using CountingEntities;
using CountingEntities.Models;
using CountingEntities.Repos;
using Multithreading_serialization;


class Program
{
    public static int Counter = 10;
    public static int ThreadCountRequest = 0;
    public static readonly string Path = "CountingData.dat";
    static void Main(string[] args)
    {
        var user = Environment.UserName;

        //var user = "jix";

        LoadLastData(user);

        if (ThreadCountRequest == 0)
        {
            Console.WriteLine("Во сколько потоков хочешь посчитать?");

            while (!int.TryParse(Console.ReadLine(), out ThreadCountRequest) || ThreadCountRequest == 0)
            {
                Console.WriteLine("Шалите, милок! Даю вам еще попытку");
            }
        }

        var threadPerformer = new DownCounter();

        var countingPoints = new CounterPoint()
        {
            LastCount = threadPerformer.StartCountDown(ThreadCountRequest, Counter),
            ThreadCount = ThreadCountRequest
        };

        var session = new Session()
        {
            UserName = user,
            RequestDate = DateTime.Now,
            CounterPoint = countingPoints
        };

        AddCounterSessionRecord(session);
        Console.ReadKey();
    }
    private static void LoadLastData(string user)
    {
        var foundCounterPoints = new CounterPoint();

        using (var dataRepo = new CounterRepo<CounterPoint>())
        using (var userRepo = new SessionRepo())
        {
            foundCounterPoints = dataRepo.GetData(userRepo.GetSessionByParameter(user));
        }

        if (foundCounterPoints != null && foundCounterPoints.LastCount > 0)
        {
            Console.WriteLine($"В прошлый раз ты остановился на счете {foundCounterPoints.LastCount}, " +
                              $"потоков было {foundCounterPoints.ThreadCount} - хочешь продолжить счет? \n" +
                              $"Если да - введи Y, если хочешь начать сначала, то введи N");

            var response = "";

            while ((response = Console.ReadLine().ToLower()) != "y" && response != "n")
            {
                Console.WriteLine("Шалите, милок! Даю вам еще попытку");
            }

            if (response == "y")
            {
                Counter = foundCounterPoints.LastCount;
                ThreadCountRequest = foundCounterPoints.ThreadCount;
            }
        }
    }

    private static void AddCounterSessionRecord(Session session)
    {
        using (var sessionRepo = new SessionRepo())
        {
            sessionRepo.AddRecord(session);
        }
        Console.WriteLine("Архиварус все помнит");
    }
}



