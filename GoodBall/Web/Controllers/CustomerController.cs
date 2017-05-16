using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Management.Controllers;
using Helper;
using Service;
using Service.Dto;

namespace Web.Controllers
{
    public class CustomerController : BaseController
    {
        //
        // GET: /Customer/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Answer()
        {
            return View();
        }

        public JsonResult AddCustomer(CustomerDto Customer)
        {
            return ExceptionCatch.Invoke(() =>
            {
                CustomerService.Instance.AddCustomer(Customer);
            });
        }

        public JsonResult UpdateCustomer(CustomerDto Customer)
        {
            return ExceptionCatch.Invoke(() =>
            {
                CustomerService.Instance.UpdateCustomer(Customer);
                //todo:发送到微信端 
            });
        }

        public JsonResult DeleteCustomer(long? id)
        {
            return ExceptionCatch.Invoke(() =>
            {
                CustomerService.Instance.DeleteCustomer(id.Value);
            });
        }

        public JsonResult GetList(string title, int page, int rows)
        {
            int total;
            var result = CustomerService.Instance.GetCustomerListByPage(title, rows, page, out total);

            return Json(new
            {
                rows = result.Select(x => new
                {
                    x.Id,
                    x.Question,
                    x.Answer,
                    AnswerTime = x.AnswerTime.ToString(),
                    CreateTime = x.CreateTime.ToString(),
                }),
                total
            }, JsonRequestBehavior.AllowGet);
        }
    }
}
