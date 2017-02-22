﻿using DataCollection.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public sealed class UserRepository : Repository<User, UserRepository>
    {
        private UserRepository()
        {

        }
    }
}
