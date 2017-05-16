using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Dto
{
    public class PayAmountDto
    {
        public long Id { get; set; }

        /// <summary>
        /// 基本金额
        /// </summary>
        public int BaseAmount { get; set; }

        /// <summary>
        /// 赠送金额
        /// </summary>
        public int GiveAmount { get; set; }

        /// <summary>
        /// 赠送类型
        /// </summary>
        public string CalType { get; set; }


        public string  CreateTime { get; set; }
    }
}
