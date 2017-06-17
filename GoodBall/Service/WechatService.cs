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
             responseMessage.Content = "本公众号不提供此服务，请迟点再试！";
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
             });
             responseMessage.Content = "客服已收到您的消息，将会尽快进行回复，如有紧急问题，请拨打客服电话：13570111111";
             return responseMessage;
         }

         /// <summary>
         /// 发送微信消息
         /// </summary>
         /// <param name="request"></param>
         /// <returns></returns>
         public static SendTemplateMessageResult SendPromoteMessage(WechatPromoteDto request)
         {
             var templateId = string.Format("{0}", ConfigurationManager.AppSettings["ProjectTemplateId"]);//模板Id
             var accessToken = AccessTokenContainer.GetAccessToken(ConfigHelper.WeChatAppId);
             var message = new
             {
                 first = new TemplateDataItem(request.Match, "#000000"),
                 keyword1 = new TemplateDataItem(request.Result, "#000000"),
                 //keyword2 = new TemplateDataItem(request.ReportNo, "#000000"),
                 //keyword3 = new TemplateDataItem(request.ProjectAddress, "#000000"),
                 //remark = new TemplateDataItem(request.Remark, "#000000")
             };
             var result = TemplateApi.SendTemplateMessage(accessToken, request.OpenId, templateId, "#000000", "", message);
             return result;
         }
    }
}
