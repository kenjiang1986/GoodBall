using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Helper;
using Senparc.Weixin.MP;
using Senparc.Weixin.MP.Entities.Request;
using Service;

namespace Web.Controllers
{
    public class MessageController : Controller
    {
        /// <summary>
        /// 微信后台验证地址
        /// </summary>
        [HttpGet]
        [ActionName("Index")]
        public ActionResult Get(PostModel postModel, string echostr)
        {
            if (CheckSignature.Check(postModel.Signature, postModel.Timestamp, postModel.Nonce, ConfigHelper.WeChatToken))
            {
                return Content(echostr); //返回随机字符串则表示验证通过
            }
            else
            {
                return Content("failed:" + postModel.Signature + "," + CheckSignature.GetSignature(postModel.Timestamp, postModel.Nonce, ConfigHelper.WeChatToken) + "。");
            }
        }

        /// <summary>
        /// 用户发送消息后，微信平台自动Post一个请求到这里，并等待响应XML。
        /// </summary>
        [HttpPost]
        [ActionName("Index")]
        public ActionResult Post(PostModel postModel)
        {
            if (!CheckSignature.Check(postModel.Signature, postModel.Timestamp, postModel.Nonce, ConfigHelper.WeChatToken))
            {
                return Content("参数错误！");
            }
            postModel.Token = ConfigHelper.WeChatToken;
            postModel.EncodingAESKey = ConfigHelper.WeChatEncodingAESKey;
            postModel.AppId = ConfigHelper.WeChatAppId;

            var messageHandler = new WechatService(Request.InputStream, postModel);//接收消息
            messageHandler.Execute();//执行微信处理过程
            return Content(messageHandler.ResponseDocument.ToString());//返回xml信息
        }

    }
}
