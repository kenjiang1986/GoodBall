using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Cond
{
    public class SmsRequest
    {
        public string Account { get; set; }

        public string Password { get; set; }

        public string Mobile { get; set; }

        public string Content { get; set; }

        public string Format { get; set; }
    }
}
