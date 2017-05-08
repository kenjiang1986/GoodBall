using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataCollection.Entity;

namespace DataCollection.EntityConfig
{
    internal class GoodsConfig : EntityConfig<Goods>
    {
        internal GoodsConfig()
        {
            base.ToTable("Goods");
            base.Property(x => x.GoodsImage).HasMaxLength(200);
            base.HasKey(x => x.Id);
        }
    }
}
