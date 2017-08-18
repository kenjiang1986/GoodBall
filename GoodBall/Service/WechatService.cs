using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Helper;
using Senparc.Weixin.Context;
using Senparc.Weixin.MP.AdvancedAPIs;
using Senparc.Weixin.MP.AdvancedAPIs.TemplateMessage;
using Senparc.Weixin.MP.CommonAPIs;
using Senparc.Weixin.MP.Entities.Request;
using Senparc.Weixin.MP.MessageHandlers;
using Senparc.Weixin.MP.Entities;
using Service.Dto;


namespace Service
{
    public class WechatService : MessageHandler<MessageContext<IRequestMessageBase, IResponseMessageBase>>
    {

        private static string msgContent = string.Format("客服已收到您的消息，将会尽快进行回复，如有紧急问题，请拨打客服电话：{0}", ConfigHelper.CustomerPhone);
        public WechatService(Stream inputStream, PostModel postModel, int maxRecordCount = 0)
            : base(inputStream, postModel, maxRecordCount)
        {

        }


         /// <summary>
         /// 默认处理
         /// </summary>
         /// <param name="requestMessage"></param>
         /// <returns></returns>
         public override IResponseMessageBase DefaultResponseMessage(IRequestMessageBase requestMessage)
         {
             var responseMessage = base.CreateResponseMessage<ResponseMessageText>();
             responseMessage.Content = "公众号即将上线，敬请关注！";
             return responseMessage;
         }

         /// <summary>
         /// 文字消息处理
         /// </summary>
         /// <param name="requestMessage"></param>
         /// <returns></returns>
         public override IResponseMessageBase OnTextRequest(RequestMessageText requestMessage)
         {
             var responseMessage = base.CreateResponseMessage<ResponseMessageText>();
             CustomerService.Instance.AddCustomer(new Dto.CustomerDto()
             {
                 Question = requestMessage.Content,
                 OpenId = requestMessage.FromUserName
             });
             responseMessage.Content = msgContent;
             return responseMessage;
         }

         /// <summary>
         /// 发送推介消息
         /// </summary>
         /// <param name="request"></param>
         /// <returns></returns>
         public static SendTemplateMessageResult SendPromoteMessage(WechatPromoteDto request)
         {
             var templateId = string.Format("{0}", ConfigurationManager.AppSettings["PromoteTemplateId"]);//模板Id
             var accessToken = AccessTokenContainer.GetAccessToken(ConfigHelper.WeChatAppId);
             var message = new
             {
                 first = new TemplateDataItem("比赛推介内容：", "#000000"),
                 keyword1 = new TemplateDataItem(request.MatchTime, "#000000"),
                 keyword2 = new TemplateDataItem(request.Match, "#000000"),
                 keyword3 = new TemplateDataItem(request.MatchResult, "#000000"),
                 remark = new TemplateDataItem(string.Format("推介结果：{0}",request.Result), "#000000")
             };
             var result = TemplateApi.SendTemplateMessage(accessToken, request.OpenId, templateId, "#000000", "", message);
             return result;
         }

         /// <summary>
         /// 发送客服消息
         /// </summary>
         /// <param name="request"></param>
         /// <returns></returns>
         public static SendTemplateMessageResult SendCustomerMessage(string remark, string openId)
         {
             var templateId = string.Format("{0}", ConfigurationManager.AppSettings["CustomerTemplateId"]);//模板Id
             var accessToken = AccessTokenContainer.GetAccessToken(ConfigHelper.WeChatAppId);
             var message = new
             {
                 first = new TemplateDataItem("问题反馈回复", "#000000"),
                 keyword1 = new TemplateDataItem("问题", "#000000"),
                 keyword2 = new TemplateDataItem("客服", "#000000"),
                 keyword3 = new TemplateDataItem(ConfigHelper.CustomerPhone, "#000000"),
                 remark = new TemplateDataItem( remark, "#000000")
             };
             var result = TemplateApi.SendTemplateMessage(accessToken, openId, templateId, "#000000", "", message);
             return result;
         }
    }
}
