using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Net;
using System.IO;


namespace Service
{
    internal class SmsService
    {
        private static string PostUrl = ConfigurationManager.AppSettings["SmsUrl"];
        private static string User = ConfigurationManager.AppSettings["SmsUser"];
        private static string Password = ConfigurationManager.AppSettings["SmsPassword"];


        public static bool SendSms(string phone, string content)
        {
            bool result = false;

            string postStrTpl = "un={0}&pw={1}&phone={2}&msg={3}&rd=1";

            UTF8Encoding encoding = new UTF8Encoding();
            byte[] postData = encoding.GetBytes(string.Format(postStrTpl, User, Password, phone, content));
            System.GC.Collect();
            HttpWebRequest myRequest = (HttpWebRequest)HttpWebRequest.Create(PostUrl);
            myRequest.KeepAlive = false;
            myRequest.Method = "POST";
            myRequest.ContentType = "application/x-www-form-urlencoded";
            myRequest.ContentLength = postData.Length;
            myRequest.Timeout = 5000;
            Stream newStream = myRequest.GetRequestStream();
            //发送短信
            newStream.Write(postData, 0, postData.Length);
            newStream.Flush();
            newStream.Close();

            HttpWebResponse myResponse = (HttpWebResponse)myRequest.GetResponse();

            if (myResponse.StatusCode == HttpStatusCode.OK)
            {
                myResponse.Close();
                myRequest.Abort();
                result = true;
            }

            return result;
        }
    }
}
