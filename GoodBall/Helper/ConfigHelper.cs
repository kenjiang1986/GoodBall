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
        public static string WeChatAppId = ConfigurationManager.AppSettings["WeChatAppId"];

        public static string WeChatSecret = ConfigurationManager.AppSettings["WeChatSecret"];

        public static string WebDomainName = ConfigurationManager.AppSettings["WebDomainName"];

        public static string BindingEnabled = ConfigurationManager.AppSettings["BindingEnabled"];

        public static string WeChatToken = ConfigurationManager.AppSettings["WeChatToken"];

        public static string WeChatEncodingAESKey = ConfigurationManager.AppSettings["WeChatEncodingAESKey"];

        public static string AdminName = ConfigurationManager.AppSettings["AdminName"];

        public static string CustomerPhone = ConfigurationManager.AppSettings["CustomerPhone"];

        public static string FocusContent = ConfigurationManager.AppSettings["FocusContent"];

        public static string CustomerContent = ConfigurationManager.AppSettings["CustomerContent"];
        
        //public static string GetAdminName()
        //{
        //    return ConfigurationManager.AppSettings["AdminName"].ToString();
        //}
    }
}
