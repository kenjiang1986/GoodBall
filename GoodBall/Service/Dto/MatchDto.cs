using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Dto
{
    public class MatchDto
    {
        public long Id { get; set; }

        /// <summary>
        /// 主队
        /// </summary>
        public string TeamA { get; set; }

        /// <summary>
        /// 客队
        /// </summary>
        public string TeamB { get; set; }

        /// <summary>
        /// 盘口数据
        /// </summary>
        public string Dish { get; set; }

        /// <summary>
        /// 比赛地点
        /// </summary>
        public string Venue { get; set; }

        /// <summary>
        /// 比赛时间
        /// </summary>
        public string  MatchTime { get; set; }

        public string Operator { get; set; }

        public string CreateTime { get; set; }
    }
}
