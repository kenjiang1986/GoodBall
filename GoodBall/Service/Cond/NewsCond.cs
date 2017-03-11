using Helper.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Cond
{
    public class NewsCond
    {
        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public string Title { get; set; }

        public string  NewsType { get; set; }
    }
}
