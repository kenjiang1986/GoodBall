using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCollection.Entity
{
    public class RechargeRecord
    {
        public long Id { get; set; }

        /// <summary>
        /// 充值金额
        /// </summary>
        public int Price { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        public string Operator { get; set; }

        public DateTime CreateTime { get; set; }

        public long UserId { get; set; }

        public virtual User RechargeUser { get; set; }
    }
}
