using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Dto
{
    public class OrderDto
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

       
        public string CreateTime { get; set; }

        /// <summary>
        /// 货物名称
        /// </summary>
        public string GoodsName { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public string Quantity { get; set; }

        /// <summary>
        /// 货物图片
        /// </summary>
        public string GoodsImage { get; set; }
    }
}
