using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataCollection.Entity;

namespace DataCollection.EntityConfig
{
    internal class PayAmountConfig : EntityConfig<PayAmount>
    {
        internal PayAmountConfig()
        {
            base.ToTable("PayAmount");
            base.HasKey(x => x.Id);
        }
    }
}
