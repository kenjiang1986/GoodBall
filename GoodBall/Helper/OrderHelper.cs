using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper
{
    public static class OrderHelper
    {
        public static string GetOrderNo()
        {
            byte[] buffer = Guid.NewGuid().ToByteArray();
            var instanceNo = BitConverter.ToInt64(buffer, 0).ToString().Substring(0, 12);
            return instanceNo;
        }
    }
}
