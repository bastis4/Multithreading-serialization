using CountingEntities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CountingEntities.Repos
{
    public class SessionRepo : CounterRepo<Session>
    {
        public int GetSessionByParameter(string parameter)
        {
            var id = Context.Session
                .Where(d => d.UserName == parameter)
                .OrderByDescending(u => u.RequestDate)
                .Select(u => u.RequestId)
                .FirstOrDefault();
            return id;
        }
    }
}
