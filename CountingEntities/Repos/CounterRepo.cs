using CountingEntities.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CountingEntities.Repos
{
    public class CounterRepo<T> : IDisposable, IRepo<T> where T : class
    {
        private DbSet<T> _table;
        private CounterSessionContext _db;

        protected CounterSessionContext Context => _db;

        public CounterRepo()
        {
            _db = new CounterSessionContext();
            _table = _db.Set<T>();
        }

        public void AddRecord(T entity)
        {
            _table.Add(entity);
            _db.SaveChanges();
        }

        public T GetData(int parameter)
        {
            return _table.Find(parameter);
        }

        public void Dispose()
        {
            _db?.Dispose();
        }
    }
}
