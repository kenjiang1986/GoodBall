﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Dto
{
    public class ReturnRuleDto
    {
        public long Id { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        ///  数值
        /// </summary>
        public int Numerical { get; set; }


        /// <summary>
        ///  第一次折扣价格
        /// </summary>
        public int FirstPrice { get; set; }
    }
}
