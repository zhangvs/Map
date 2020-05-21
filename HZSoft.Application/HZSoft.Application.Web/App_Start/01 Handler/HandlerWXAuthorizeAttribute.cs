using System.Web.Mvc;
using HZSoft.Application.Code;
using HZSoft.Util;
using System.Web;
using HZSoft.Application.Web.Utility;

namespace HZSoft.Application.Web
{
    /// <summary>
    /// 版 本 6.1
    /// 
    /// 创建人：佘赐雄
    /// 日 期：2015.11.9 10:45
    /// 描 述：登录认证（会话验证组件）
    /// </summary>
    public class HandlerWXAuthorizeAttribute : AuthorizeAttribute
    {
        private LoginMode _customMode;
        /// <summary>默认构造</summary>
        /// <param name="Mode">认证模式</param>
        public HandlerWXAuthorizeAttribute(LoginMode Mode)
        {
            _customMode = Mode;
        }

        /// <summary>
        /// 响应前执行登录验证,查看当前用户是否有效 
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            //登录拦截是否忽略
            if (_customMode == LoginMode.Ignore)
            {
                return;
            }
            //请求地址
            HttpRequest request = HttpContext.Current.Request;
            string RequestUri = !string.IsNullOrEmpty(request.Params["urlstr"]) ? request.Params["urlstr"] : request.FilePath;//AbsoluteUri
            //判断是否微信通过认证
            if (CurrentWxUser.Users == null)
            {
                filterContext.Result = new RedirectResult(string.Format(WeixinConfig.GetCodeUrl, HttpUtility.UrlEncode(RequestUri)));
                return;
            }
        }



    }
}