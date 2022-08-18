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

        public static CountingData LoadFromBinaryFile(string fileName)
        {
            BinaryFormatter binFormat = new BinaryFormatter();

            var countingData = new CountingData();

            using (Stream fStream = File.OpenRead(fileName))
            {
                countingData = (CountingData)binFormat.Deserialize(fStream);
                Console.WriteLine("Файл получен");
               
            }
            return countingData;
        }
    }
}
