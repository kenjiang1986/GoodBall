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
    public class WechatShopController : WechatBaseController
    {
        //
        // GET: /WechatShop/

        public ActionResult Index()
        {
            ViewBag.UserIntegral = UserService.GetCurrentUser() == null ? 0 : UserService.GetCurrentUser().Integral;
            return View();
        }

        public ActionResult Cash()
        {
            return View();
        }

        public ActionResult UserOrders()
        {
            return View();
        }

        public JsonResult GetUserOrders(long userId)
        {
            var result = OrderService.Instance.GetUserOrderList(userId);
            return Json(new WechatResponse()
            {
                data = result
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetProductList()
        {
            int total;
            var result = GoodsService.Instance.GetGoodsListByPage(string.Empty, 1000, 1, out total);

            return Json(new WechatResponse()
            {
                data = result.Select(x => new
                {
                    x.Id,
                    x.GoodsName,
                    x.GoodsImage,
                    x.Integral,
                    x.Quantity,
                    Size = "",
                    x.SizeList
                })
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetProductSize(long id)
        {
            
            var result = GoodsService.Instance.GetGoods(id);

            return Json(new WechatResponse()
            {
                data = result.SizeList
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AddOrder(string qty, string contactor, string mobile, string doorplate, string postCode, long goodsId, string size)
        {
            var order = new OrderDto()
            {
                ContactPhone = mobile,
                Quantity = qty,
                Address = doorplate,
                Receiver = contactor,
                PostCode = postCode,
                GoodsId = goodsId,
                Size = size
            };
            
            return ExceptionCatch.WechatInvoke(() =>
            {
                OrderService.Instance.AddOrder(order);
            }, "兑换成功");
            

            return Json(new WechatResponse()
            {
                data = "兑换成功"
            }, JsonRequestBehavior.AllowGet);
        }

    }
}
