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
    public class GoodsController : BaseController
    {
        //
        // GET: /Goods/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult UpSet()
        {
            return View();
        }

        public JsonResult UpSetGoods(GoodsDto Goods)
        {
            return ExceptionCatch.Invoke(() =>
            {
                if (Goods.Id > 0)
                {
                    GoodsService.Instance.UpdateGoods(Goods);
                }
                else
                {
                    GoodsService.Instance.AddGoods(Goods);
                }
            });
        }

        public JsonResult GetGoods(long? id)
        {
            var result = GoodsService.Instance.GetGoods(id.Value);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteGoods(long? id)
        {
            return ExceptionCatch.Invoke(() =>
            {
                GoodsService.Instance.DeleteGoods(id.Value);
            });
        }

        public JsonResult GetList(string goodsName, int page, int rows)
        {
            int total;
            var result = GoodsService.Instance.GetGoodsListByPage(goodsName, rows, page, out total);

            return Json(new
            {
                rows = result.Select(x => new
                {
                    x.Id,
                    x.GoodsName,
                    x.Integral,
                    x.Quantity,
                    CreateTime = x.CreateTime.ToString(),
                }),
                total
            }, JsonRequestBehavior.AllowGet);
        }

    }
}
