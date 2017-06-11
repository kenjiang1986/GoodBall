using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Management.Controllers;
using Service;
using Service.Dto;
using Helper;


namespace Web.Controllers
{
    public class OrderController : BaseController
    {
        //
        // GET: /Order/

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult UpSetOrder(OrderDto Order)
        {
            return ExceptionCatch.Invoke(() =>
            {
                if (Order.Id > 0)
                {
                    OrderService.Instance.UpdateOrder(Order);
                }
                else
                {
                    OrderService.Instance.AddOrder(Order);
                }
            });
        }

        public JsonResult UpdateOrderState(long id, string  state)
        {
            return ExceptionCatch.Invoke(() =>
            {
               
                OrderService.Instance.UpdateOrderState(id, state);
              
            });
        }




        public JsonResult GetOrder(long? id)
        {
            var result = OrderService.Instance.GetOrder(id.Value);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteOrder(long? id)
        {
            return ExceptionCatch.Invoke(() =>
            {
                OrderService.Instance.DeleteOrder(id.Value);
            });
        }

        public JsonResult GetList(string orderNo, int page, int rows)
        {
            int total;
            var result = OrderService.Instance.GetOrderListByPage(0, orderNo, rows, page, out total);

            return Json(new
            {
                rows = result,
                total
            }, JsonRequestBehavior.AllowGet);
        }
    }
}
