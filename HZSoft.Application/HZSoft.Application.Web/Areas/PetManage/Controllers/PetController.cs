using HZSoft.Application.Busines.BaseManage;
using HZSoft.Application.Busines.CustomerManage;
using HZSoft.Application.Busines.WeChatManage;
using HZSoft.Application.Code;
using HZSoft.Application.Entity.BaseManage;
using HZSoft.Application.Entity.CustomerManage;
using HZSoft.Application.Entity.WeChatManage;
using HZSoft.Application.Web.Utility;
using HZSoft.Data.Repository;
using HZSoft.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace HZSoft.Application.Web.Areas.PetManage.Controllers
{
    /// <summary>
    /// 宠物
    /// </summary>
    [HandlerWXAuthorizeAttribute(LoginMode.Enforce)]
    public class PetController : Controller
    {
        PetBLL petBll = new PetBLL();
        Ku_LocationBLL locationBll = new Ku_LocationBLL();
        #region 视图功能
        /// <summary>
        /// 公司列表
        /// </summary>
        /// <param name="Id">某个位置id</param>
        /// <param name="searchName">搜索名称</param>
        /// <returns></returns>
        public ActionResult PetList(int? Id, string searchName, string userId)
        {
            if (Id == null)
            {
                return Content("未选择任何热点！");
            }
            Query query = new Query()
            {
                LocationId = Id.ToString(),
                SearchName = searchName,
                UserId = userId
            };
            string queryJson = query.ToJson();
            ViewBag.listgt = petBll.GetList(queryJson);
            ViewBag.LocationId = Id;
            return View();

        }

        /// <summary>
        /// 公司详情
        /// </summary>
        /// <returns></returns>
        public ActionResult PetDetail(int? keyValue)
        {
            PetEntity entity = petBll.GetEntity(keyValue);
            ViewBag.model = entity;
            return View();
        }

        /// <summary>
        /// 添加宠物http://map.lywenkai.cn/PetManage/Pet/PetForm
        /// </summary>
        /// <returns></returns>
        public ActionResult PetForm(string masterId, string masterName)
        {
            var jssdkUiPackage = Senparc.Weixin.MP.Helpers.JSSDKHelper.GetJsSdkUiPackage(WeixinConfig.AppID, WeixinConfig.AppSecret, Request.Url.AbsoluteUri.Split('#')[0]);
            ViewBag.JSSDKUiPackage = jssdkUiPackage;
            ViewBag.masterId = masterId;
            ViewBag.masterName = masterName;

            return View();
        }
        #endregion

        #region 获取数据
        /// <summary>
        /// 公司详情
        /// </summary>
        /// <returns></returns>
        public ActionResult GetFormJson(int? keyValue)
        {
            PetEntity entity = petBll.GetEntity(keyValue);
            return Content(entity.ToJson());
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 提交拓客
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult PetForm(int? keyValue, PetEntity entity)
        {
            petBll.SaveForm(keyValue, entity);
            return Content("true");
        }
        #endregion

    }
}
