﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCollection.Entity
{
    public class ReturnRule
    {
        public long Id { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        ///  数值
        /// </summary>
        public double Numerical { get; set; }
    }
}
