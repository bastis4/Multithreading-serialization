using CountingEntities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CountingEntities.Repos
{
    public class CounterRepo : IRepository<CountingData>
    {
        private CountingDataEntities _db;

        public CounterRepo(CountingDataEntities db)
        {
            _db = db;
        }

        public void AddDataRecords(CountingData entity)
        {
            _db.CountingData.Add(entity);
        }

        public CountingData GetData(int parameter)
        {
            return _db.CountingData.Find(parameter);
        }
    }
}
