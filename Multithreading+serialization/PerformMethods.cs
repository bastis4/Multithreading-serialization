using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
using static Program;

namespace Multithreading_serialization
{
    public static class PerformMethods
    {
        static int _counter = 20;
        public static Semaphore sem = new Semaphore(1, 1);
        public static int lastCount = _counter;
        public static void CountFromMillionToOne(object para)
        {
            ThreadParams param = (ThreadParams)para;
            CancellationToken ct = (CancellationToken)param.a;
            IProgress<int> progress = param.b;
            while (_counter > 0 && !ct.IsCancellationRequested)
            {
                sem.WaitOne();
                lastCount = _counter;


                if (ct.IsCancellationRequested || _counter < 0)
                {
                    var pi = 77;
                    Console.WriteLine(pi);

                    progress.Report(lastCount);

                    return;
                }

                else
                {
                    lastCount = _counter;
                    Console.WriteLine($"Счет: {_counter} , ID#: {Thread.CurrentThread.ManagedThreadId}");
                    
                    progress.Report(lastCount);

                    _counter--;

                    Thread.Sleep(200);
                }
                sem.Release();
            }
        }

        public static void SaveAsBinaryFormat(object objGraph, string fileName)
        {
            BinaryFormatter binFormat = new BinaryFormatter();

            using (Stream fStream = new FileStream(fileName,
                  FileMode.Create, FileAccess.Write, FileShare.None))
            {
                binFormat.Serialize(fStream, objGraph);
            }
            Console.WriteLine("Данные подсчета сохранены");
        }

        public static void LoadFromBinaryFile(string fileName)
        {
            BinaryFormatter binFormat = new BinaryFormatter();

            using (Stream fStream = File.OpenRead(fileName))
            {
                CountingData countingData = (CountingData)binFormat.Deserialize(fStream);
                Console.WriteLine("Во сколько потоков считали в прошлый раз? : {0}", countingData.threadCount);
                Console.WriteLine("На какой цифре остановились? : {0}", countingData.lastCount);
            }
        }
    }
}
