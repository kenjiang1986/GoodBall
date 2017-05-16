using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EnumUtils;
using Helper;
using Helper.Enum;
using Management.Controllers;
using Service;
using Service.Dto;

namespace Web.Controllers
{
    public class BaseDataController : BaseController
    {
        //
        // GET: /BaseData/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult UpSet()
        {
            ViewBag.CalTypeList = EnumHelper.GetValues<CalTypeEnum>();
            return View();
        }

        public JsonResult UpSetPayAmount(PayAmountDto PayAmount)
        {
            return ExceptionCatch.Invoke(() =>
            {
                if (PayAmount.Id > 0)
                {
                    PayAmountService.Instance.UpdatePayAmount(PayAmount);
                }
                else
                {
                    PayAmountService.Instance.AddPayAmount(PayAmount);
                }
            });
        }

        public JsonResult DeletePayAmount(long? id)
        {
            return ExceptionCatch.Invoke(() =>
            {
                PayAmountService.Instance.DeletePayAmount(id.Value);
            });
        }

        public JsonResult GetPayAmount(long? id)
        {
            var result = PayAmountService.Instance.GetPayAmount(id.Value);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetList(int page, int rows)
        {
            int total;
            var result = PayAmountService.Instance.GetPayAmountListByPage(rows, page, out total);

            return Json(new
            {
                rows = result.Select(x => new
                {
                    x.Id,
                    x.BaseAmount,
                    x.GiveAmount,
                    x.CalType,
                    CreateTime = x.CreateTime.ToString(),
                }),
                total
            }, JsonRequestBehavior.AllowGet);
        }
    }
}
