using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Helper;
using Senparc.Weixin.MP;
using Senparc.Weixin.MP.AdvancedAPIs;
using Service;
using Service.API;

namespace Web.Controllers
{
    public class WechatLoginController : Controller
    {
        public ActionResult WechatLoginIndex()
        {
            if (string.IsNullOrEmpty(CookieHelper.GetCookie("OpenId")))
            {
                string redirectUrl = string.Format("{0}/WechatLogin/GetUserToken", ConfigHelper.WebDomainName);
                string authUrl = OAuthApi.GetAuthorizeUrl(ConfigHelper.WeChatAppId, redirectUrl, "STATE", OAuthScope.snsapi_base, "code", false);
                return Redirect(authUrl);
            }
            return View();
        }

        /// <summary>
        /// 微信重定向
        /// </summary>
        /// <param name="state"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public ActionResult GetUserToken(string state, string code)
        {
            var token = OAuthApi.GetAccessToken(ConfigHelper.WeChatAppId, ConfigHelper.WeChatSecret, code);
            CookieHelper.WriteCookie("OpenId", token.openid);
            return RedirectToAction("WechatLoginIndex", "WechatLogin");
            

        }

        [HttpPost]
        public JsonResult Login(string userName, string password)
        {
            return ExceptionCatch.WechatInvoke(() =>
            {
                UserService.Instance.CustomerLogin(userName, password);
            });
        }

        [HttpPost]
        public JsonResult Logout(string userName)
        {
            UserService.Instance.CustomerLogout();
            return Json(new WechatResponse(), JsonRequestBehavior.AllowGet);
        }

    }
}
