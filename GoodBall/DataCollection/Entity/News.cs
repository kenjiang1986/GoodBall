﻿using Helper.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCollection.Entity
{
    public class News
    {
        public long Id { get; set; }

        public string Title { get; set; }

        public string Source { get; set; }

        public string Content { get; set; }

        public string Operator { get; set; }

        public DateTime CreateTime { get; set; }

        public NewsTypeEnum NewsType { get; set; }

        public string TitleImageUrl { get; set; }
    }
}
