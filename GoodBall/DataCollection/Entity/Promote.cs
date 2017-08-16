using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Helper.Enum;

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
        public RaceTypeEnum RaceType { get; set; }

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
        /// 状态（1、未开始 2、中 3、不中）
        /// </summary>
        public PromoteStateEnum State { get; set; }

        /// <summary>
        /// 推介类型（1、推介 2、人人竞彩）
        /// </summary>
        public int PromoteType { get; set; }

        public virtual User Operator { get; set; }

        public long OperatorId { get; set; }

        public DateTime CreateTime { get; set; }

        public virtual Match Match { get; set; }

        public long MatchId { get; set; }


        /// <summary>
        /// 是否退费 
        /// </summary>
        public bool IsReturn { get; set; }

        /// <summary>
        /// 是否VIP推介
        /// </summary>
        public bool IsVip { get; set; }

        public string Result { get; set; }

        public virtual ICollection<User> UserList { get; set; }
    }
}
