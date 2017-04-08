using EnumUtils;
using Helper;
using Helper.Enum;
using Service;
using Service.Cond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Service.Dto;

namespace Management.Controllers
{
    public class NewsController : BaseController
    {
        //
        // GET: /News/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult UpSet()
        {
            ViewBag.NewsType = EnumHelper.GetValues<NewsTypeEnum>();
            return View();
        }

        [ValidateInput(false)]
        public JsonResult UpSetUser(NewsDto news)
        {
            return ExceptionCatch.Invoke(() =>
            {
                if (news.Id > 0)
                {
                    NewsService.Instance.UpdateNews(news);
                }
                else
                {
                    NewsService.Instance.AddNews(news);
                }
            });
        }

        public JsonResult GetNews(long? id)
        {
            var result = NewsService.Instance.GetNews(id.Value);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteNews(long? id)
        {
            return ExceptionCatch.Invoke(() =>
            {
                NewsService.Instance.DeleteNews(id.Value);
            });
        }

        public JsonResult GetList(string title, string startDate, string endDate,string newsType, int page, int rows)
        {
            int total;
            var newsCond = new NewsCond()
            {
                Title = title,
                NewsType = newsType
            };

            if(!string.IsNullOrEmpty(startDate))
            {
                newsCond.StartDate = Convert.ToDateTime(startDate);
            }
            if (!string.IsNullOrEmpty(endDate))
            {
                newsCond.EndDate = Convert.ToDateTime(endDate);
            }
            var result = NewsService.Instance.GetNewsListByPage(newsCond, rows, page, out total);

            return Json(new
            {
                rows = result.Select(x => new
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
                total
            }, JsonRequestBehavior.AllowGet);
        }
    }
}
