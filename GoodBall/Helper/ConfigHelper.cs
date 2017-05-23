using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper
{
    public class ConfigHelper
    {
        public static string GetAdminName()
        {
            return ConfigurationManager.AppSettings["AdminName"].ToString();
        }
    }
}
