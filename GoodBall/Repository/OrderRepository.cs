using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataCollection.Entity;

namespace Repository
{
    public sealed class OrderRepository : BaseRepository<Order, OrderRepository>
    {
        private OrderRepository() { }
    }
}
