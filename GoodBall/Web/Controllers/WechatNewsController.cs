using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class WechatNewsController : Controller
    {
        //
        // GET: /WechatNews/

        /// <summary>
        /// 新闻 
        /// </summary>
        /// <returns></returns>
        public ActionResult News()
        {
            return View();
        }


        /// <summary>
        /// 好波分享 
        /// </summary>
        /// <returns></returns>
        public ActionResult Share()
        {
            return View();
        }


        /// <summary>
        /// 博彩趣谈
        /// </summary>
        /// <returns></returns>
        public ActionResult Gambling()
        {
            return View();
        }

    }
}
