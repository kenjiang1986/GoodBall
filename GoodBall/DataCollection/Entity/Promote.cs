using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCollection.Entity
{
    /// <summary>
    /// 推介
    /// </summary>
    public class Promote
    {
        public long Id { get; set; }

        /// <summary>
        /// 竞彩类型
        /// </summary>
        public int RaceType { get; set; }

        /// <summary>
        /// 星数
        /// </summary>
        public int level { get; set; }

        /// <summary>
        /// 推介内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 价格
        /// </summary>
        public int Price { get; set; }

        /// <summary>
        /// 积分
        /// </summary>
        public int Integral { get; set; }

        /// <summary>
        /// 状态（1、中 2、不中）
        /// </summary>
        public int State { get; set; }

        /// <summary>
        /// 推送类型(1、让球 2、竞彩 3、足彩310)
        /// </summary>
        public int SendType { get; set; }

        public string Operator { get; set; }

        public DateTime CreateTime { get; set; }
    }
}
