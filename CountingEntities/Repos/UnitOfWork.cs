using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CountingEntities.Repos
{
    public class UnitOfWork : IDisposable
    {
        private CountingDataEntities _db = new CountingDataEntities();
        private UserRepo userRepo;
        private CounterRepo counterRepo;
        public UserRepo Users
        {
            get
            {
                if (userRepo == null)
                    userRepo = new UserRepo(_db);
                return userRepo;
            }
        }
        public CounterRepo Counters
        {
            get
            {
                if (counterRepo == null)
                    counterRepo = new CounterRepo(_db);
                return counterRepo;
            }
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}

