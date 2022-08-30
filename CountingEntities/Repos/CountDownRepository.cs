using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CountingEntities.Repos
{
    public class CountDownRepository<T> : IDisposable, IRepository<T> where T : class
    {
        private CountingDataEntities _context;
        private DbSet<T> _dbSet;

        protected CountingDataEntities Context => _context;

        public CountDownRepository()
        {
            _context = new CountingDataEntities();
            _dbSet = _context.Set<T>();
        }
        public void AddDataRecords(T entity)
        {
            var e = _dbSet.Add(entity);
            //_context.SaveChanges();
        }

        public T GetData(int parameter)
        {
            return _dbSet.Find(parameter);
        }

        public void Dispose()
        {
            _context?.Dispose();
        }

        public void Save()
        {
            _context.SaveChanges();
        }

 /*       public int Save(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            return SaveChanges();
        }

        internal int SaveChanges()
        {
            try
            {
                return _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                //Thrown when there is a concurrency error
                //for now, just rethrow the exception
                throw;
            }
            catch (DbUpdateException ex)
            {
                //Thrown when database update fails
                //Examine the inner exception(s) for additional 
                //details and affected objects
                //for now, just rethrow the exception
                throw;
            }
            catch (CommitFailedException ex)
            {
                //handle transaction failures here
                //for now, just rethrow the exception
                throw;
            }
            catch (Exception ex)
            {
                //some other exception happened and should be handled
                throw;
            }
        }*/
    }
}
