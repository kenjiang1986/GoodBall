﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Dto
{
    public class NewsDto
    {
        public long Id { get; set; }

        public string Title { get; set; }

        public string Source { get; set; }

        public string Content { get; set; }

        public string Operator { get; set; }

        public string CreateTime { get; set; }

        public string NewsType { get; set; }

        public string TitleImageUrl { get; set; }
    }
}
