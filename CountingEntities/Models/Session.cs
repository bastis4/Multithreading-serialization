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
    public partial class Session
    {
        [Key]
        public int Id { get; set; }

        [StringLength(50)]
        public string UserName { get; set; }
        public DateTime RequestDate { get; set; }
        public int RequestId { get; set; }

        [ForeignKey(nameof(RequestId))]
        public virtual CounterItem CounterItem { get; set; }

    }
}
