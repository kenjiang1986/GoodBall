using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public string Adress { get; set; }

        /// <summary>
        /// 积分
        /// </summary>
        public string Integral { get; set; }

        public string ContactPhone { get; set; }

        /// <summary>
        /// 比赛时间
        /// </summary>
        public DateTime MatchTime { get; set; }

        public string State { get; set; }

        public DateTime CreateTime { get; set; }

        public virtual Goods Goods { get; set; }
    }
}
