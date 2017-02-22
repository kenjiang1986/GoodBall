using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Helper
{
    public static class CookieHelper
    {
        /// <summary>
        /// 用户状态保存标识Key
        /// </summary>
        public static readonly string UserStateKey = "GB-User";
        /// <summary>
        /// Cookie时效
        /// </summary>
        public static readonly int CookieExpiresMinute = int.Parse(ConfigurationManager.AppSettings["CookieExpiresMinute"]);

        /// <summary>
        /// 写cookie值(多个键值的 Cookie 对象)
        /// </summary>
        /// <param name="strName">名称</param>
        /// <param name="key">Key</param>
        /// <param name="strValue">值</param>
        public static void WriteCookie(string strName, string key, string strValue)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[strName];
            if (cookie == null)
            {
                cookie = new HttpCookie(strName);
                //cookie.Domain = "yunfangdata.com";
            }
            cookie[key] = strValue;
            HttpContext.Current.Response.AppendCookie(cookie);
        }
        /// <summary>
        /// 写cookie值
        /// </summary>
        /// <param name="strName">名称</param>
        /// <param name="key">Key</param>
        /// <param name="strValue">值</param>
        public static void WriteCookie(string strName, string strValue)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[strName];
            if (cookie == null)
            {
                cookie = new HttpCookie(strName);
                //cookie.Domain = "yunfangdata.com";                
            }
            //FormsAuthentication.HashPasswordForStoringInConfigFile(strValue, "md5");
            cookie.Value = strValue;
            HttpContext.Current.Response.AppendCookie(cookie);

        }

        /// <summary>
        /// 写加密cookie值
        /// </summary>
        /// <param name="strName">名称</param>
        /// <param name="strValue">值</param>
        /// <param name="nowTime">当前时间</param>
        public static void WriteEncryptCookie(string strName, string strValue, DateTime nowTime)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[strName];
            if (cookie == null)
            {
                cookie = new HttpCookie(strName);
                //cookie.Domain = "yunfangdata.com";                
            }
            //FormsAuthentication.HashPasswordForStoringInConfigFile(strValue, "md5");
            cookie.Value = DESEncrypt.Encrypt(strValue);
            cookie.Expires = nowTime.AddMinutes(CookieExpiresMinute);
            HttpContext.Current.Response.AppendCookie(cookie);

        }
        /// <summary>
        /// 设置cookie过期时间
        /// </summary>
        /// <param name="strName">名称</param>
        /// <param name="expires">过期时间(天数)</param>
        public static void SetCookieExpires(string strName, int day)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[strName];
            if (cookie == null)
            {
                cookie = new HttpCookie(strName);
            }
            cookie.Expires = DateTime.Now.AddDays(day);
            HttpContext.Current.Response.AppendCookie(cookie);
        }

        /// <summary>
        /// 读cookie值
        /// </summary>
        /// <param name="strName">名称</param>
        /// <returns>cookie值</returns>
        public static string GetCookie(string strName)
        {
            if (HttpContext.Current.Request.Cookies != null && HttpContext.Current.Request.Cookies[strName] != null)
            {
                return HttpContext.Current.Request.Cookies[strName].Value.ToString();
            }
            return "";
        }

        /// <summary>
        /// 读解密cookie值并返回超时时间
        /// </summary>
        /// <param name="strName">名称</param>
        /// <param name="nowTime">当前时间</param>
        /// <returns>cookie值</returns>
        public static string GetDecryptCookie(string strName, out DateTime nowTime)
        {
            if (HttpContext.Current.Request.Cookies[strName] != null)
            {
                nowTime = DateTime.Now.AddMinutes(-CookieExpiresMinute);
                return DESEncrypt.Decrypt(HttpContext.Current.Request.Cookies[strName].Value);
            }
            nowTime = DateTime.Now;
            return "";
        }

        /// <summary>
        /// 读cookie值(多个键值的 Cookie 对象)
        /// </summary>
        /// <param name="strName">名称</param>
        /// <param name="key">Key</param>
        /// <returns>cookie值</returns>
        public static string GetCookie(string strName, string key)
        {
            if (HttpContext.Current.Request.Cookies != null
                && HttpContext.Current.Request.Cookies[strName] != null
                && HttpContext.Current.Request.Cookies[strName][key] != null)
            {
                return HttpContext.Current.Request.Cookies[strName][key].ToString();
            }
            return "";
        }

        /// <summary>
        /// 移除客户端 Cookie
        /// </summary>
        /// <param name="strName"></param>
        public static void RemoveCookie(string strName)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[strName];
            if (cookie != null)
            {
                cookie.Value = "";
                HttpContext.Current.Request.Cookies.Remove(strName);
                //清除 
                TimeSpan ts = new TimeSpan(0, 0, 0, 0);//时间跨度 
                cookie.Expires = DateTime.Now.Add(ts);//立即过期 
                HttpContext.Current.Response.Cookies.Add(cookie);//写入立即过期的*/
                HttpContext.Current.Response.Cookies[strName].Expires = DateTime.Now.AddDays(-1);
            }
        }


        /// <summary>
        /// 移除客户端 Cookie(多值 Cookie 中的某个值)
        /// </summary>
        /// <param name="strName"></param>
        public static void RemoveCookie(string strName, string key)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[strName];
            if (cookie != null)
            {
                cookie.Values.Remove(key);
                HttpContext.Current.Response.AppendCookie(cookie);
            }
        }


       
    }
}
