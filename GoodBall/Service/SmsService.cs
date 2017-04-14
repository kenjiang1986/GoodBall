using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Net;
using System.IO;
using Service.API;
using Service.Cond;


namespace Service
{
    internal class SmsService : BaseClientService
    {
        private static string User = ConfigurationManager.AppSettings["SmsUser"];
        private static string Password = ConfigurationManager.AppSettings["SmsPassword"];

        public SmsService()
            : base(ConfigurationManager.AppSettings["SmsUrl"])
        {
        }


        public bool SendSms(SmsRequest request)
        {
            var result = Post<SmsRequest, ApiModelResponseResult>(request, "/apis/InworkReportFinish");
            return result.Success;
        }
    }
}
