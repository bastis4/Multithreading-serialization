using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CountingEntities.Repos
{
    public interface IRepository<T>
    {
        T GetData(int parameter);
        void AddDataRecords(T data);
    }
}
