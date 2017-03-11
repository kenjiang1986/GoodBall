using Helper.Enum;
using Service;
using Service.Cond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Management.Controllers
{
    public class NewsController : Controller
    {
        //
        // GET: /News/

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetList(string title, string startDate, string endDate,string newsType, int page, int rows)
        {
            int total;
            var newsCond = new NewsCond()
            {
                Title = title,
                StartDate = Convert.ToDateTime(startDate),
                EndDate = Convert.ToDateTime(endDate),
                NewsType = newsType
            };
            var result = NewsService.Instance.GetNewsListByPage(newsCond, rows, page, out total);

            return Json(new
            {
                rows = result.Select(x => new
                {
                    x.Id,
                    x.Title,
                    x.Soruce,
                    x.Content,
                    x.Operator,
                    CreateTime = x.CreateTime.ToString(),
                }),
                total
            }, JsonRequestBehavior.AllowGet);
        }
    }
}
