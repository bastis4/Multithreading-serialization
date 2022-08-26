using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CountingEntities.Models
{
    public partial class UserData
    {
        public int Id { get; set; }
        [StringLength(50)]
        public string UserName { get; set; }
        public int UserId { get; set; }
        public byte[] Timestamp { get; set; }
        //public DateTime ModifiedDate { get; set; }

        [ForeignKey(nameof(Id))]
        public int QueryId { get; set; }
        public virtual CountingData CountingData { get; set; }
    }
}
