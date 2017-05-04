using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.API
{
    public class WechatResponse
    {
        public WechatResponse()
        {
            status = 200;
        }

        public int status { get; set; }

        public object data { get; set; }
    }
}
