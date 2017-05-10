using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Service;

namespace Web.Controllers
{
    public class WechatBaseController : Controller
    {
        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            if (UserService.GetCurrentUser() == null)
            {
                requestContext.HttpContext.Response.Redirect("/WechatLogin/WechatLoginIndex");
            }
            base.Initialize(requestContext);
        }
    }
}
