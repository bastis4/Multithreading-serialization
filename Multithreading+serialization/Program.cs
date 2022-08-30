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

        var threadPerformer = new DecrementingCounter();

        var countingData = new CountingData()
        {
            LastCount = threadPerformer.StartCountDown(ThreadCountRequest, Counter),
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
        var foundCountingData = new CountingData();

        using (var dataRepo = new CountDownRepository<CountingData>())
        using (var userRepo = new UserDataRepository())
        {
            foundCountingData = dataRepo.GetData(userRepo.GetDataByParameter(user));
        }

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

    private static void AddRecordToDB(CountingData countingData, UserData userData)
    {

        using (var dataRepo = new CountDownRepository<CountingData>())
        {
            using (var userRepo = new UserDataRepository())
            {
                dataRepo.AddDataRecords(countingData);
                dataRepo.Save();

                userRepo.AddDataRecords(userData);
                userRepo.Save();
            }
        }
        

            

        /* using (var dataRepo = new CountDownRepository<CountingData>())

         {
             dataRepo.AddDataRecords(countingData);
             using (var userRepo = new CountDownRepository<UserData>())
             {
                 userRepo.AddDataRecords(userData);
             }
         }*/

        /*        using (var ent = new CountingDataEntities())
                {
                    var dataRepo = new CountDownRepository<CountingData>();
                    var userRepo = new CountDownRepository<UserData>();
                    userRepo.AddDataRecords(userData);
                    ent.SaveChanges();
                    dataRepo.AddDataRecords(countingData);
                    ent.SaveChangesAsync().Wait();
                }*/

        /*        using (var userRepo = new CountDownRepository<UserData>())
                {
                    userRepo.AddDataRecords(userData);
                }

                using (var dataRepo = new CountDownRepository<CountingData>())
                {
                    dataRepo.AddDataRecords(countingData);
                }*/









        Console.WriteLine("Данные добавлены в таблицы");
    }
}



