using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Helper.Enum;

namespace DataCollection.Entity
{
    public class Order
    {
        public long Id { get; set; }

        /// <summary>
        /// 订单编号
        /// </summary>
        public string OrderNo { get; set; }

        /// <summary>
        /// 收件人
        /// </summary>
        public string Receiver { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 积分
        /// </summary>
        public int Integral { get; set; }

        public OrderStateEnum State { get; set; }

        public string ContactPhone { get; set; }

        public string PostCode { get; set; }


        public DateTime CreateTime { get; set; }

        public long GoodsId { get; set; }

        public virtual Goods Goods { get; set; }

        public long UserId { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public int Quantity { get; set; }
    }
}
