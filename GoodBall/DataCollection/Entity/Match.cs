﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Helper.Enum;

namespace DataCollection.Entity
{
    public class Match
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
        /// 比赛地点;
        /// </summary>
        public string  Venue { get; set; }

        public string MatchResult { get; set; }

        /// <summary>
        /// 比赛时间
        /// </summary>
        public DateTime MatchTime { get; set; }

        public string Operator { get; set; }

        public DateTime CreateTime { get; set; }

        public MatchStateEnum MatchState { get; set; }
    }
}
