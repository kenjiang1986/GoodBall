using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Helper;
using Senparc.Weixin.MP.AdvancedAPIs;
using Senparc.Weixin.MP.CommonAPIs;
using Senparc.Weixin.MP.Entities;
using Senparc.Weixin.MP.Helpers;
using Senparc.Weixin.MP.TenPayLib;
using Service.Dto;

namespace Service
{
    public  class WechatPayService
    {

        //这里通过官方的一个实体，用户自行使用，我这里是直接读取的CONFIG文件
        private static Senparc.Weixin.MP.TenPayLibV3.TenPayV3Info tenPayV3Info = new Senparc.Weixin.MP.TenPayLibV3.TenPayV3Info(ConfigHelper.WeChatAppId, ConfigHelper.WeChatSecret, "1440864202", "haobo63880haobo63881haobo63882hb", "http://127.0.0.1");

        /// <summary>
        /// 微信预支付
        /// </summary>
        /// <param name="attach"></param>
        /// <param name="body"></param>
        /// <param name="openid"></param>
        /// <param name="price"></param>
        /// <param name="orderNum"></param>
        /// <returns></returns>
        public static WechatOrderInfo PayInfo(string attach, string body, string openid, string price, string orderNum = "1833431773763549")
        {
            RequestHandler requestHandler = new RequestHandler(HttpContext.Current);
            //微信分配的公众账号ID（企业号corpid即为此appId）
            requestHandler.SetParameter("appid", tenPayV3Info.AppId);
            //附加数据，在查询API和支付通知中原样返回，该字段主要用于商户携带订单的自定义数据
            requestHandler.SetParameter("attach", attach);
            //商品或支付单简要描述
            requestHandler.SetParameter("body", body);
            //微信支付分配的商户号
            requestHandler.SetParameter("mch_id", tenPayV3Info.MchId);
            //随机字符串，不长于32位。
            requestHandler.SetParameter("nonce_str", TenPayUtil.GetNoncestr());
            //接收微信支付异步通知回调地址，通知url必须为直接可访问的url，不能携带参数。
            requestHandler.SetParameter("notify_url", tenPayV3Info.TenPayV3Notify);
            //trade_type=JSAPI，此参数必传，用户在商户公众号appid下的唯一标识。
            requestHandler.SetParameter("openid", openid);
            //商户系统内部的订单号,32个字符内、可包含字母，自己生成
            requestHandler.SetParameter("out_trade_no", orderNum);
            //APP和网页支付提交用户端ip，Native支付填调用微信支付API的机器IP。
            requestHandler.SetParameter("spbill_create_ip", "127.0.0.1");
            //订单总金额，单位为分，做过银联支付的朋友应该知道，代表金额为12位，末位分分
            requestHandler.SetParameter("total_fee", price);
            //取值如下：JSAPI，NATIVE，APP，我们这里使用JSAPI
            requestHandler.SetParameter("trade_type", "JSAPI");
            //设置KEY
            requestHandler.SetKey(tenPayV3Info.Key);

            requestHandler.CreateMd5Sign();
            requestHandler.GetRequestURL();
            requestHandler.CreateSHA1Sign();
            string data = requestHandler.ParseXML();
            requestHandler.GetDebugInfo();

            //获取并返回预支付XML信息
            return XmlHelper.XmlDeserialize <WechatOrderInfo>(TenPayV3.Unifiedorder(data));
        }

        public static ShareInfo GetPayInfo(string prepayid)
        {
            var shareInfo = new ShareInfo();
            //检查是否已经注册jssdk
            if (!JsApiTicketContainer.CheckRegistered(ConfigHelper.WeChatAppId))
            {
                JsApiTicketContainer.Register(ConfigHelper.WeChatAppId, ConfigHelper.WeChatSecret);
            }
            JsApiTicketResult jsApiTicket = JsApiTicketContainer.GetJsApiTicketResult(ConfigHelper.WeChatAppId);
            JSSDKHelper jssdkHelper = new JSSDKHelper();
            shareInfo.Ticket = jsApiTicket.ticket;
            shareInfo.CorpId = ConfigHelper.WeChatAppId.ToLower();
            shareInfo.Noncestr = JSSDKHelper.GetNoncestr().ToLower();
            shareInfo.Timestamp = JSSDKHelper.GetTimestamp().ToLower();
            shareInfo.Package="prepay_id=" + prepayid.ToLower();

            RequestHandler requestHandler = new RequestHandler(HttpContext.Current);

            requestHandler.SetParameter("appId", shareInfo.CorpId);
            requestHandler.SetParameter("timeStamp", shareInfo.Timestamp);
            requestHandler.SetParameter("nonceStr", shareInfo.Noncestr);
            requestHandler.SetParameter("package", shareInfo.Package);
            requestHandler.SetParameter("signType", "MD5");

            requestHandler.SetKey(tenPayV3Info.Key);
            requestHandler.CreateMd5Sign();
            requestHandler.GetRequestURL();
            requestHandler.CreateSHA1Sign();
            shareInfo.PaySign = (requestHandler.GetAllParameters()["sign"]).ToString();
            return shareInfo;
        }

    }
    
}
