using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Service;
using Service.API;

namespace Web.Controllers
{
    public class WechatPayController : Controller
    {
        //
        // GET: /WechatPay/

        public ActionResult PayCenter()
        {
            return View();
        }

        public JsonResult GetPayAmounts()
        {
            int total;
            var result = PayAmountService.Instance.GetPayAmountListByPage(100, 1, out total);

            return Json(new WechatResponse()
            {
                data = result.Select(x => new
                {
                    x.Id,
                    PayAmount = x.BaseAmount + x.GiveAmount,
                    PayAmountText = "充值" + x.BaseAmount + x.CalType + x.GiveAmount,
                })
            }, JsonRequestBehavior.AllowGet);
        }
    }
}
