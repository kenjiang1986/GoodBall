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
    internal class SmsService
    {
        private static string PostUrl = ConfigurationManager.AppSettings["SmsUrl"];
        private static string User = ConfigurationManager.AppSettings["SmsUser"];
        private static string Password = ConfigurationManager.AppSettings["SmsPassword"];




        public static bool SendSms(string phone, string content)
        {
            bool result = false;
            //content = "您的验证码是：1234。请不要把验证码泄露给其他人。";

            string postStrTpl = "account={0}&password={1}&mobile={2}&content={3}";

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
            //String xml = new StreamReader(myResponse.GetResponseStream(), Encoding.UTF8).ReadToEnd();

            if (myResponse.StatusCode == HttpStatusCode.OK)
            {
                myResponse.Close();
                myRequest.Abort();
                result = true;
            }

            return result;
        }

        /// <summary>  
        /// 生成随机数字  
        /// </summary>  
        /// <param name="Length">生成长度</param>  
        /// <param name="Sleep">是否要在生成前将当前线程阻止以避免重复</param>  
        public static string GetPhoneNumber(int Length, bool Sleep)
        {
            if (Sleep) System.Threading.Thread.Sleep(3);
            string result = "";
            System.Random random = new Random();
            for (int i = 0; i < Length; i++)
            {
                result += random.Next(10).ToString();
            }
            return result;
        }  
    }
}
