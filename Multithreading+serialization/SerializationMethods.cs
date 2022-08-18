using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


namespace Multithreading_serialization
{
    public static class SerializationMethods
    {
        static BinaryFormatter binFormat = new BinaryFormatter();
        public static void SaveAsBinaryFormat(object objGraph, string fileName)
        {
            using (var fStream = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                binFormat.Serialize(fStream, objGraph);
            }
            Console.WriteLine("Данные подсчета сохранены");
        }

        public static CountingData LoadFromBinaryFile(string fileName)
        {
            var countingData = new CountingData();

            using (var fStream = File.OpenRead(fileName))
            {
                countingData = (CountingData)binFormat.Deserialize(fStream);
                Console.WriteLine("Файл получен");
            }
            return countingData;
        }
    }
}
