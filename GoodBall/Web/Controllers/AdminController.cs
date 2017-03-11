using Helper;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Management.Controllers
{
    public class AdminController : Controller 
    {
        public ActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public JsonResult AdminLogin(string userName, string password)
        {
            return ExceptionCatch.Invoke(() =>
            {
                UserService.Instance.AdminLogin(userName, password);
            });
        }
    }
}
