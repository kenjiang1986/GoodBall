using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Helper;
using Service;
using Service.API;
using Service.Dto;

namespace Web.Controllers
{
    public class WechatPayController : WechatBaseController
    {
        //
        // GET: /WechatPay/

        public ActionResult PayCenter()
        {
           return View();
        }

        public ActionResult PaySuccess(long amountId)
        {
            WechatPayService.Pay(amountId);
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

        public JsonResult Pay(long payAmountId)
        {
            //var result = WechatPayService.PayInfo("", "test", "oBbN2wV9VZ8D_wIqWpzlxJ6IpbtE", " WechatPayService.GetOrderNumber());
            var amount = PayAmountService.Instance.GetPayAmount(payAmountId);
            var result = WechatPayService.PayInfo("", "V币充值",UserService.GetCurrentUser().OpenId , ((amount.BaseAmount + amount.GiveAmount) * 100).ToString(), WechatPayService.GetOrderNumber());
            var shareInfo = WechatPayService.GetPayInfo(result.prepay_id);
            return Json(new WechatResponse()
            {
                data = new
                {
                    appId = shareInfo.CorpId,
                    timeStamp = shareInfo.Timestamp,
                    nonceStr = shareInfo.Noncestr,
                    package = shareInfo.Package,
                    signType = "MD5",
                    paySign= shareInfo.PaySign
                }
            }, JsonRequestBehavior.AllowGet);
        }
    }
}  
