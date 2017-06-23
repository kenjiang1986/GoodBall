using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataCollection.Entity;

namespace DataCollection.EntityConfig
{
    internal class CustomerConfig : EntityConfig<Customer>
    {
        internal CustomerConfig()
        {
            base.ToTable("Customer");
            base.HasKey(x => x.Id);
            base.Property(x => x.Question).HasMaxLength(2000);
            base.Property(x => x.Question).HasMaxLength(2000);
        }
    }
}
