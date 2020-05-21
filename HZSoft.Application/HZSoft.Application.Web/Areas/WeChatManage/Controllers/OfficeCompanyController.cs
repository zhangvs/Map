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

namespace HZSoft.Application.Web.Areas.WeChatManage.Controllers
{
    /// <summary>
    /// 扫街：写字楼公司
    /// </summary>
    //[HandlerWXAuthorizeAttribute(LoginMode.Enforce)]
    [HandlerWXLoginAttribute(LoginMode.Enforce)]
    public class OfficeCompanyController : Controller
    {
        WeChat_UsersBLL wechatUserBll = new WeChat_UsersBLL();
        UserBLL userBLL = new UserBLL();
        POS_OfficeCompanyBLL posOfficeCompanyBll = new POS_OfficeCompanyBLL();
        POS_OfficeBLL posOfficeBll = new POS_OfficeBLL();

        #region 视图功能
        /// <summary>
        /// 
        /// </summary>
        /// <param name="officeId"></param>
        /// <param name="officeName"></param>
        /// <param name="searchName"></param>
        /// <returns></returns>
        [HandlerLocationAttribute(LoginMode.Enforce)]
        public ActionResult OfficeCompanyList(string officeId, string officeName, string searchName)
        {
            string currentLng = Request.Cookies["currlong"].Value;
            string currentLat = Request.Cookies["currlat"].Value;
            QueryWhere entity = new QueryWhere()
            {
                OfficeId = officeId,
                SearchName = searchName,
                wxLat = currentLat,
                wxLon = currentLng
            };
            string queryJson = entity.ToJson();
            ViewBag.listgt = posOfficeCompanyBll.GetList(queryJson);
            ViewBag.officeId = officeId;
            ViewBag.officeName = officeName;
            return View();
        }
        /// <summary>
        /// 添加写字楼公司
        /// </summary>
        /// <returns></returns>
        [HandlerLocationAttribute(LoginMode.Enforce)]
        public ActionResult AddOfficeCompany(string officeId, string officeName)
        {
            var jssdkUiPackage = Senparc.Weixin.MP.Helpers.JSSDKHelper.GetJsSdkUiPackage(WeixinConfig.AppID, WeixinConfig.AppSecret, Request.Url.AbsoluteUri.Split('#')[0]);
            ViewBag.JSSDKUiPackage = jssdkUiPackage;
            ViewBag.SellerName = Code.OperatorProvider.Provider.Current().UserName;
            ViewBag.SellerId = Code.OperatorProvider.Provider.Current().UserId;
            ViewBag.officeId = officeId;
            ViewBag.officeName = officeName;
            ViewBag.officeList = posOfficeBll.GetList("{}");
            return View();
        }        
        #endregion

        #region 获取数据
        /// <summary>
        /// 写字楼详情
        /// </summary>
        /// <returns></returns>
        public ActionResult GetFormJson(string keyValue)
        {
            string returnUrl = Request.Url.ToString();
            POS_OfficeCompanyEntity entity = posOfficeCompanyBll.GetEntity(keyValue);
            if (string.IsNullOrEmpty(entity.SellerId))
            {
                entity.SellerName = Code.OperatorProvider.Provider.Current().UserName;
                entity.SellerId = Code.OperatorProvider.Provider.Current().UserId;
            }
            return Content(entity.ToJson());
        }

        /// <summary>
        /// 公司详情
        /// </summary>
        /// <returns></returns>
        public ActionResult OfficeCompanyDetail(string keyValue)
        {
            POS_OfficeCompanyEntity entity = posOfficeCompanyBll.GetEntity(keyValue);
            ViewBag.model = entity;
            return View();
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 提交拓客
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddOfficeCompany(string keyValue, POS_OfficeCompanyEntity entity)
        {
            entity.OpenId = CurrentWxUser.OpenId;
            entity.NickName = CurrentWxUser.NickName;
            //插入店铺表
            posOfficeCompanyBll.SaveForm(keyValue, entity);

            return Content("true");
        }
        #endregion
        
    }
}
