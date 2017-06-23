using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Dto
{
    public class CustomerDto
    {
        public long Id { get; set; }

        /// <summary>
        /// 问题
        /// </summary>
        public string Question { get; set; }

        /// <summary>
        /// 回答
        /// </summary>
        public string Answer { get; set; }

        public string CreateTime { get; set; }

        public string AnswerTime { get; set; }

        public string OpenId { get; set; }
    }
}
