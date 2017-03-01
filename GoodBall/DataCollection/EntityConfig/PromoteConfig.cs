using DataCollection.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCollection.EntityConfig
{
     internal class PromoteConfig : EntityConfig<Promote>
    {
        internal PromoteConfig()
        {
            base.ToTable("Promote");
            base.HasKey(x => x.Id);
        }
    }
}
