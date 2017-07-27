﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.unionpay.acp.sdk;
using upacp_sdk_net.com.unionpay.sdk;

namespace Service
{
    public class UnionPayService
    {
        /// <summary>
        ///银联支付
        /// </summary>
        public static string Pay()
        {
            Dictionary<string, string> param = new Dictionary<string, string>();

            //以下信息非特殊情况不需要改动
            param["version"] = SDKConfig.Version;//版本号
            param["encoding"] = "UTF-8";//编码方式
            param["txnType"] = "01";//交易类型
            param["txnSubType"] = "01";//交易子类
            param["bizType"] = "000201";//业务类型
            param["signMethod"] = SDKConfig.SignMethod;//签名方法
            param["channelType"] = "08";//渠道类型
            param["accessType"] = "0";//接入类型
            param["frontUrl"] = SDKConfig.FrontUrl;  //前台通知地址      
            param["backUrl"] = SDKConfig.BackUrl;  //后台通知地址
            param["currencyCode"] = "156";//交易币种

            // 订单超时时间。
            // 超过此时间后，除网银交易外，其他交易银联系统会拒绝受理，提示超时。 跳转银行网银交易如果超时后交易成功，会自动退款，大约5个工作日金额返还到持卡人账户。
            // 此时间建议取支付时的北京时间加15分钟。
            // 超过超时时间调查询接口应答origRespCode不是A6或者00的就可以判断为失败。
            param["payTimeout"] = DateTime.Now.AddMinutes(15).ToString("yyyyMMddHHmmss");


            //TODO 以下信息需要填写
            param["merId"] = "777290058148979";//商户号，请改自己的测试商户号，此处默认取demo演示页面传递的参数
            param["orderId"] = "20170726112005";//商户订单号，8-32位数字字母，不能含“-”或“_”，此处默认取demo演示页面传递的参数，可以自行定制规则
            param["txnTime"] = DateTime.Now.ToString("yyyyMMddHHmmss");//订单发送时间，格式为YYYYMMDDhhmmss，取北京时间，此处默认取demo演示页面传递的参数，参考取法： DateTime.Now.ToString("yyyyMMddHHmmss")
            param["txnAmt"] = "1";//交易金额，单位分，此处默认取demo演示页面传递的参数

            // 请求方保留域，
            // 透传字段，查询、通知、对账文件中均会原样出现，如有需要请启用并修改自己希望透传的数据。
            // 出现部分特殊字符时可能影响解析，请按下面建议的方式填写：
            // 1. 如果能确定内容不会出现&={}[]"'等符号时，可以直接填写数据，建议的方法如下。
            //param["reqReserved"] = "透传信息1|透传信息2|透传信息3";
            // 2. 内容可能出现&={}[]"'符号时：
            // 1) 如果需要对账文件里能显示，可将字符替换成全角＆＝｛｝【】“‘字符（自己写代码，此处不演示）；
            // 2) 如果对账文件没有显示要求，可做一下base64（如下）。
            //    注意控制数据长度，实际传输的数据长度不能超过1024位。
            //    查询、通知等接口解析时使用System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(reqReserved))解base64后再对数据做后续解析。
            //param["reqReserved"] = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes("任意格式的信息都可以"));

            //TODO 其他特殊用法请查看 pages/api_01_gateway/special_use_purchase.htm

            AcpService.Sign(param, System.Text.Encoding.UTF8);
            return AcpService.CreateAutoFormHtml(SDKConfig.FrontTransUrl, param, System.Text.Encoding.UTF8);// 将SDKUtil产生的Html文档写入页面，从而引导用户浏览器重定向   
        }
    }
}
