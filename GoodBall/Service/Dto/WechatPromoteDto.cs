using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Dto
{
    public class WechatPromoteDto
    {
        public string  OpenId { get; set; }

        public string Match { get; set; }

        public string Result { get; set; }


        public string MatchTime { get; set; }

        public string MatchResult { get; set; }
    }
}
