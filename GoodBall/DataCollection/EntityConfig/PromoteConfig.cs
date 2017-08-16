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
            base.Property(x => x.Content).IsMaxLength();
            base.HasRequired(x => x.Match).WithMany().HasForeignKey(x => x.MatchId);
            base.HasRequired(x => x.Operator).WithMany().HasForeignKey(x => x.OperatorId);
        }
    }
}
