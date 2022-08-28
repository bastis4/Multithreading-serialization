﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.ComponentModel.DataAnnotations;

namespace CountingEntities.Models
{
    public partial class CountingData
    {
        [Key]
        public int Id { get; set; }
        public int ThreadCount { get; set; }
        public int LastCount { get; set; }
        public virtual ICollection<UserData> UserData { get; set; } = new HashSet<UserData>();

        public CountingData()
        {
            
        }
        public CountingData(int threadCount, int lastCount)
        {
            ThreadCount = threadCount;
            LastCount = lastCount;
        }
    }
}
