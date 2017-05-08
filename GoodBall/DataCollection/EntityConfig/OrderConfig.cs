using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataCollection.Entity;

namespace DataCollection.EntityConfig
{
    internal class OrderConfig : EntityConfig<Order>
    {
        internal OrderConfig()
        {
            base.ToTable("Order");
            base.HasKey(x => x.Id);
            base.HasRequired(x => x.Goods).WithMany().HasForeignKey(x => x.GoodsId);
        }
    }
}
