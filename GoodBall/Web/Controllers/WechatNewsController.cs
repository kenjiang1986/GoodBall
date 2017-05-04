using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Service;
using Service.Cond;

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

        public ActionResult NewsDetail()
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

        public JsonResult GetList(string newsType)
        {
            int total;
            var result = NewsService.Instance.GetNewsListByPage(new NewsCond() { NewsType = newsType}, 1000, 1, out total);

            return Json(new
            {
                data = result.Select(x => new
                {
                    x.Id,
                    x.Title,
                    x.Source,
                    x.Content,
                    x.Operator,
                    x.NewsType,
                    x.TitleImageUrl,
                    CreateTime = x.CreateTime.ToString(),
                }),
                status = 200
            }, JsonRequestBehavior.AllowGet);
        }

    }
}
