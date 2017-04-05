using DataCollection.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCollection.EntityConfig
{
    internal class NewsConfig : EntityConfig<News>
    {
        internal NewsConfig()
        {
            base.ToTable("News");
            base.Property(x => x.Content).IsMaxLength();
            base.HasKey(x => x.Id);
        }
    }
}
