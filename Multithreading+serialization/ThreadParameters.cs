using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multithreading_serialization
{
    public class ThreadParameters
    {
       //1
        public CancellationToken CtsToken { get; set; }
        public Progress<int> Progress { get; set; }
    }
}
