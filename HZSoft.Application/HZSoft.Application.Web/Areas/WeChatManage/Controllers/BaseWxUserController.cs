using HZSoft.Application.Entity.WeChatManage;
using HZSoft.Application.Web.Utility;
using Senparc.Weixin.MP.CommonAPIs;
using Senparc.Weixin.MP.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HZSoft.Application.Web.Areas.WeChatManage.Controllers
{
    public class BaseWxUserController : Controller
    {
        #region [Private Method]
        /// <summary>
        /// 微信授权登录
        /// </summary>
        protected string WeixinAuth(string returnUrl)
        {
            //判断是否登录
            if (CurrentWxUser.Users == null)
            {
                string url = string.Format(WeixinConfig.GetCodeUrl, returnUrl);
                return url;
            }
            return "";
        }
        #endregion
    }
}
