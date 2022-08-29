using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CountingEntities.Repos
{
    public class CountDownRepository<T> : IDisposable, IRepository<T> where T : class
    {
        private readonly CountingDataEntities _context;
        private readonly DbSet<T> _dbSet;

        protected CountingDataEntities Context => _context;

        public CountDownRepository()
        {
            _context = new CountingDataEntities();
            _dbSet = _context.Set<T>();
        }
        public void AddDataRecords(T data)
        {
            _dbSet.Add(data);
            _context.SaveChanges();
        }

        public T GetData(int parameter)
        {
            return _dbSet.Find(parameter);
        }

        public virtual int GetDataByParameter(string parameter)
        {
            return default;
        }

        public void Dispose()
        {
            _context?.Dispose();
        }

    }
}
