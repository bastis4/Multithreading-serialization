using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.ComponentModel.DataAnnotations;

namespace CountingEntities.Models
{
    public partial class CounterPoint
    {
        [Key]
        public int Id { get; set; }
        public int ThreadCount { get; set; }
        public int LastCount { get; set; }
        public virtual ICollection<Session> Session { get; set; } = new HashSet<Session>();

        public CounterPoint()
        {
            
        }
        public CounterPoint(int threadCount, int lastCount)
        {
            ThreadCount = threadCount;
            LastCount = lastCount;
        }
    }
}
