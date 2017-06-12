using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Cond
{
    public class MatchCond
    {
        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public string TeamA { get; set; }

        public string TeamB { get; set; }

        public string MatchState { get; set; }
    }
}
