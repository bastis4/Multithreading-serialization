﻿using CountingEntities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CountingEntities.Repos
{
    public class UserDataRepository : CountDownRepository<UserData>
    {
        public int GetDataByParameter(string parameter)
        {
            var id = Context.UserData
                .Where(d => d.UserName == parameter)
                .OrderByDescending(u => u.RequestDate)
                .Select(u => u.RequestId)
                .FirstOrDefault();
            return id;
        }
    }
}