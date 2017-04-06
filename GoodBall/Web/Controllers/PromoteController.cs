using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Helper;
using Service;
using Service.Cond;
using Service.Dto;

namespace Web.Controllers
{
    public class PromoteController : Controller
    {
        //
        // GET: /Promote/

        public ActionResult Index()
        {
            return View();
        }

          public JsonResult UpSetPromote(PromoteDto Promote)
        {
            return ExceptionCatch.Invoke(() =>
            {
                if (Promote.Id > 0)
                {
                    PromoteService.Instance.UpdatePromote(Promote);
                }
                else
                {
                    PromoteService.Instance.AddPromote(Promote);
                }
            });
        }

        public JsonResult GetPromote(long? id)
        {
            var result = PromoteService.Instance.GetPromote(id.Value);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeletePromote(long? id)
        {
            return ExceptionCatch.Invoke(() =>
            {
                PromoteService.Instance.DeletePromote(id.Value);
            });
        }

        public JsonResult GetList(string startDate, string endDate, int page, int rows)
        {
            int total;
            var cond = new PromoteCond();
            if (!string.IsNullOrEmpty(startDate))
            {
                cond.StartDate = Convert.ToDateTime(startDate);
            }
            if (!string.IsNullOrEmpty(endDate))
            {
                cond.EndDate = Convert.ToDateTime(endDate);
            }
            var result = PromoteService.Instance.GetPromoteListByPage(cond, rows, page, out total);

            return Json(new
            {
                rows = result.Select(x => new
                {
                    x.Id,
                    x.Content,
                    x.Integral,
                    x.Price,
                    x.Operator,
                    x.RaceType,
                    x.SendType,
                    x.level,
                    CreateTime = x.CreateTime.ToString(),
                }),
                total
            }, JsonRequestBehavior.AllowGet);
        }
    }
}
