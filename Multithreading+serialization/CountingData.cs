using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


namespace Multithreading_serialization
{
    [Serializable]
    public class CountingData
    {
        public int threadCount;
        public int lastCount;
    }
}
