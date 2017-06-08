using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Helper
{
    public class ExceptionCatch
    {
        /// <summary>
        /// 返回类型{message=successMessage,success=true/false,data=successMessage}
        /// </summary>
        /// <param name="action"></param>
        /// <param name="successMessage"></param>
        /// <returns></returns>
        public static JsonResult Invoke(Action action, string successMessage = "操作成功！")
        {
            return Invoke
            (
                () =>
                {
                    action();
                    return successMessage;
                },
                x => x
            );
        }

        public static JsonResult WechatInvoke(Action action, string msg = "")
        {
            return WechatInvoke
            (
                () =>
                {
                    action();
                    return string.IsNullOrEmpty(msg) ? "操作成功": msg;
                }
            );
        }

        /// <summary>
        /// 返回类型{message=messageConvert(),success=true/false,data=funData()}
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <typeparam name="TData"></typeparam>
        /// <param name="funData"></param>
        /// <param name="messageConvert"></param>
        /// <returns></returns>
        private static JsonResult Invoke<TResult>(Func<TResult> funData, Func<TResult, string> messageConvert)
        {
            string message = null;
            bool success = true;
            object data = null;
            try
            {
                TResult receive = funData();
                data = receive;
                message = messageConvert(receive);
            }
            catch (Exception ex)
            {
                success = false;
                if (ex is ServiceException)
                {
                    message = ex.Message;
                }
                else
                {
                    message = "操作失败！";
                    LogHelper.Error("操作失败", ex);
                }
            }
            var json = new JsonResult();
            json.Data = new { success, message, data };
            json.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return json;
        }

        private static JsonResult WechatInvoke<TResult>(Func<TResult> funData)
        {
            var response = new WcResponse();
            try
            {
                response.data = funData();
            }
            catch (Exception ex)
            {
                
                if (ex is ServiceException)
                {
                    response.data = ex.Message;
                }
                else
                {
                    LogHelper.Error("操作失败", ex);
                }
            }
            var json = new JsonResult();
            json.Data = response;
            json.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return json;
        }
    }

    public class WcResponse
    {
        public WcResponse()
        {
            status = 200;
        }

        public int status { get; set; }

        public object data { get; set; }
    }
}
