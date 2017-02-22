using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataCollection.Entity;

namespace DataCollection.EntityConfig
{
    internal class UserConfig : EntityConfig<User>
    {
        internal UserConfig()
        {
            base.ToTable("User");
            base.HasKey(x => x.Id);
        }
    }
}
