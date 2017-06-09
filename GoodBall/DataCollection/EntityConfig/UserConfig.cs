using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataCollection.Entity;

namespace DataCollection.EntityConfig
{
    internal class UserConfig : EntityConfig<User>
    {
        internal UserConfig() : base(200)
        {
            base.ToTable("Users");
            base.HasKey(x => x.Id);
            base.HasMany(x => x.PromoteList).WithMany(x => x.UserList)
                .Map(x => { x.ToTable("UserPromote");
                    x.MapLeftKey("UserId");
                    x.MapRightKey("PromoteId");
                });
        }
    }
}
