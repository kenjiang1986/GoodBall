using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Dto
{
    public class RechargeRecordDto
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

        public string CreateTime { get; set; }

        public string RechargeUser { get; set; }

        public long RechargeUserId { get; set; }
    }
}
