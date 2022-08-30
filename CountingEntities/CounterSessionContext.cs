using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using CountingEntities;
using CountingEntities.Models;

namespace CountingEntities
{
    public partial class CounterSessionContext : DbContext
    {
        public CounterSessionContext()
            : base("name=CountDownConnect")
        {
        }

        public virtual DbSet<CounterPoint> CounterPoint { get; set; }
        public virtual DbSet<Session> Session { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CounterPoint>()
                .HasKey(e => e.Id)
                .HasMany(e => e.Session)
                .WithRequired(e => e.CounterPoint)
                .WillCascadeOnDelete(false);
        }
    }
}
