using DataCollection.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCollection.EntityConfig
{
    internal class RechargeRecordConfig : EntityConfig<RechargeRecord>
    {
        internal RechargeRecordConfig()
        {
            base.ToTable("RechargeRecord");
            base.HasKey(x => x.Id);
            //base.HasRequired(x => x.RechargeUser).WithMany().HasForeignKey(x => x.UserId);
        }
    }
}
