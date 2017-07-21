using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Dto
{
    public class GoodsDto
    {
        public long Id { get; set; }

        /// <summary>
        /// 货物名称
        /// </summary>
        public string GoodsName { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public string Quantity { get; set; }

        /// <summary>
        /// 积分
        /// </summary>
        public string Integral { get; set; }

        public string  CreateTime { get; set; }

        /// <summary>
        /// 货物图片
        /// </summary>
        public string GoodsImage { get; set; }

        /// <summary>
        /// 尺寸
        /// </summary>
        public string Size { get; set; }

        /// <summary>
        /// 尺寸数据
        /// </summary>
        public List<string> SizeList { get; set; }
    }
}
