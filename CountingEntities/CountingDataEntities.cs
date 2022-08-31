using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using CountingEntities;
using CountingEntities.Models;

namespace CountingEntities
{
    public partial class CountingDataEntities : DbContext
    {
        public CountingDataEntities()
            : base("name=CountingDataEntities")
        {
        }

        public virtual DbSet<CounterItem> CountingData { get; set; }
        //public virtual ICollection<CountingData> Values { get; set; }
        public virtual DbSet<UserData> UserData { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            /* modelBuilder.Entity<CountingData>()
                 .HasMany(e => e.UserData)
                 .WithRequired(e => e.CountingData)
                 .HasForeignKey(e => e.RequestId)
                 .WillCascadeOnDelete(false);*/

            modelBuilder.Entity<CounterItem>()
                .HasKey(e => e.Id)
                .HasMany(e => e.UserData)
                .WithRequired(e => e.CountingData)
                .WillCascadeOnDelete(false);

        }
    }
}
