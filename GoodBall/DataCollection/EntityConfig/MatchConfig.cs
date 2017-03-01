using DataCollection.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCollection.EntityConfig
{
    internal class MatchConfig : EntityConfig<Match>
    {
        internal MatchConfig()
        {
            base.ToTable("Match");
            base.HasKey(x => x.Id);
        }
    }
}
