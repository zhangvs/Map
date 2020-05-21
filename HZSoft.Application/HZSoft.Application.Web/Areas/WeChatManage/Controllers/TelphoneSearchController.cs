using HZSoft.Application.Busines.CustomerManage;
using HZSoft.Application.Entity.CustomerManage;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HZSoft.Application.Web.Areas.WeChatManage.Controllers
{
    /// <summary>
    /// 公众号号码查询页面
    /// </summary>
    public class TelphoneSearchController : Controller
    {
        private TelphoneSourceBLL telphonesourcebll = new TelphoneSourceBLL();
        /// <summary>
        /// 查询页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 查询结果
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SearchTel(string telphone)
        {
            TelphoneSourceEntity entity = telphonesourcebll.GetEntity(telphone);
            return Content(JsonConvert.SerializeObject(entity));
        }

        /// <summary>
        /// 查询结果
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult TelAuto(string telphone)
        {
            var entity = telphonesourcebll.GetList(telphone);
            return Content(JsonConvert.SerializeObject(entity));
        }
    }
}
