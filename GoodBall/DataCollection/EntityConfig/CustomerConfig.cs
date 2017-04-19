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
        }
    }
}
