using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCollection.Entity
{
    public class Goods
    {
        public long Id { get; set; }

        /// <summary>
        /// 货物名称
        /// </summary>
        public string GoodsName { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public string quantity { get; set; }

        /// <summary>
        /// 积分
        /// </summary>
        public string Integral { get; set; }

        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 货物图片
        /// </summary>
        public string GoodsImage { get; set; }



    }
}
