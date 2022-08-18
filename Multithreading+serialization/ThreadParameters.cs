using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multithreading_serialization
{
    public class ThreadParameters
    {
        public object ctsToken;
        public Progress<int> progress;
    }
}
