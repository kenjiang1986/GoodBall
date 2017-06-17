using GoodBall.Dto;
using Helper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Service;
using Service.Dto;

namespace Web.Controllers
{
    public class LayerController : Controller
    {
        //
        // GET: /Layer/

        public ActionResult UploadImage()
        {
            return View();
        }

        public ActionResult ChooseMatch()
        {
            return View();
        }



        public ActionResult Upload()
        {
            string message = "上传成功";
            try
            {
                ProcessRequest(this);
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            
            return Content("<script>alert(\"" + message + "\");parent.Refresh();</script>", "text/html");
        }


        private void ProcessRequest(Controller context)
        {
            bool isSuccess = false;

            if (context.Request.Files.Count <= 0)
            {
                throw new ServiceException("请选择上传文件");
            }
            var file = context.Request.Files[0];
            var extension = Path.GetExtension(file.FileName);
            var allowExtension = new string[] { ".jpg", ".bmp", ".png", ".jpeg" };
            if (!allowExtension.Contains(extension))
            {
                throw new ServiceException("只能上传图片文件");
            }
           
            if (file.ContentLength <= 0)
            {
                throw new ServiceException("上传文档必须大于0KB");
            }
            long fileLength = file.InputStream.Length;
            if (fileLength > 1 * 1024 * 1024)
                throw new ServiceException("上传文件最大为1M");
            context.Response.ContentType = "text/plain";
            string[] arrKeys = context.Request.Form.AllKeys;

            string imageType = "";
            string id= "";

            string fileName = Path.GetFileName(file.FileName);
            if (arrKeys.Contains("id"))
            {
                id = HttpUtility.UrlDecode(context.Request["id"]);
            }
            if (arrKeys.Contains("imageType"))
            {
                imageType = HttpUtility.UrlDecode(context.Request["imageType"]);
            }

            var upFile = context.Request.Files[0];
            string uploadFolder = @"\" + "ImageFilePath" + @"\";
            uploadFolder += id + @"\" + imageType + @"\";
            string path = context.Server.MapPath("~" + uploadFolder);

            string FileFormat = System.IO.Path.GetExtension(file.FileName); //文件后缀
            string FileName = Guid.NewGuid().ToString() + FileFormat;
            string savePath = path + FileName; //物理路径
            string RelativelyPath = uploadFolder + FileName; //相对路径


            string Direct = System.IO.Path.GetDirectoryName(savePath);
            if (!Directory.Exists(Direct))
            {
                Directory.CreateDirectory(Direct);
            }
            if (System.IO.File.Exists(savePath))
            {
                System.IO.File.Delete(savePath);
            }
            file.SaveAs(savePath);

            switch (imageType)
            {
                case "News":
                     var newsDto = new NewsDto()
                    {
                        Id = long.Parse(id),
                        TitleImageUrl = RelativelyPath
                    };
                    NewsService.Instance.UpdateNewsImage(newsDto);
                    break;
                case "Goods":
                    var goodsDto = new GoodsDto()
                    {
                        Id = long.Parse(id),
                        GoodsImage = RelativelyPath
                    };
                    GoodsService.Instance.UpdateGoodsImage(goodsDto);
                    break;
                case "User":
                    var userDto = new UserDto()
                    {
                        Id = long.Parse(id),
                        IconUrl = RelativelyPath
                    };
                    UserService.Instance.UpdateUserIcon(userDto);
                    break;
            }
        }
    }
}
