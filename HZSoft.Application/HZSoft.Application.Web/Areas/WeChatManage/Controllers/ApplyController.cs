using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HZSoft.Application.Web.Areas.WeChatManage.Controllers
{
    public class ApplyController : BaseWxUserController
    {
        //
        // GET: /WeChatManage/Apply/

        public ActionResult List()
        {
            string returnUrl = Url.Action("Index");
            string url = WeixinAuth(returnUrl);
            if (!string.IsNullOrEmpty(url))
                return Redirect(url);

            return View();
        }

    }
}
