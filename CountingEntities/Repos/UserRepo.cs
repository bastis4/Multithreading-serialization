using CountingEntities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CountingEntities.Repos
{
    public class UserRepo : IRepository<UserData>
    {
        private CountingDataEntities _db;

        public UserRepo(CountingDataEntities db)
        {
            _db = db;
        }

        public void AddDataRecords(UserData entity)
        {
            _db.UserData.Add(entity);
        }

        public UserData GetData(int parameter)
        {
            return _db.UserData.Find(parameter);
        }
        public int GetDataByParameter(string parameter)
        {
            return _db.UserData
                .Where(d => d.UserName == parameter)
                .OrderByDescending(u => u.RequestDate)
                .Select(u => u.RequestId)
                .FirstOrDefault();
        }
    }
}
