using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataCollection.Entity;

namespace DataCollection.EntityConfig
{
    internal class ReturnRuleConfig : EntityConfig<ReturnRule>
    {
        internal ReturnRuleConfig()
        {
            base.ToTable("ReturnRule");
            base.HasKey(x => x.Id);
        }
    }
}
