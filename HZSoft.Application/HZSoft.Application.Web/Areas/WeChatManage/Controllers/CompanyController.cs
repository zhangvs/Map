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
    /// 公司页面
    /// </summary>
    [HandlerWXAuthorizeAttribute(LoginMode.Enforce)]
    [HandlerWXLoginAttribute(LoginMode.Enforce)]
    public class CompanyController : Controller
    {
        Ku_CompanyBLL companyBll = new Ku_CompanyBLL();
        Ku_LocationBLL locationBll = new Ku_LocationBLL();
        #region 视图功能
        /// <summary>
        /// 公司列表
        /// </summary>
        /// <param name="locationId">某个位置id</param>
        /// <param name="searchName">搜索名称</param>
        /// <returns></returns>
        public ActionResult CompanyList(int? locationId,string searchName,string userId)
        {
            if (locationId==null)
            {
                return Content("未选择任何热点！");
            }
            Query query = new Query()
            {
                LocationId = locationId.ToString(),
                SearchName = searchName,
                UserId = userId
            };
            string queryJson = query.ToJson();
            ViewBag.listgt = companyBll.GetList(queryJson);
            ViewBag.LocationId = locationId;
            return View();

        }

        /// <summary>
        /// 公司详情
        /// </summary>
        /// <returns></returns>
        public ActionResult CompanyDetail(int? keyValue)
        {
            Ku_CompanyEntity entity = companyBll.GetEntity(keyValue);
            ViewBag.model = entity;
            return View();
        }

        /// <summary>
        /// 添加公司
        /// </summary>
        /// <returns></returns>
        public ActionResult CompanyForm()
        {
            var jssdkUiPackage = Senparc.Weixin.MP.Helpers.JSSDKHelper.GetJsSdkUiPackage(WeixinConfig.AppID, WeixinConfig.AppSecret, Request.Url.AbsoluteUri.Split('#')[0]);
            ViewBag.JSSDKUiPackage = jssdkUiPackage;


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
            Ku_CompanyEntity entity = companyBll.GetEntity(keyValue);
            return Content(entity.ToJson());
        }
        /// <summary>
        /// 根据参数获取热点
        /// </summary>
        /// <returns></returns>
        public ActionResult GetLocationJson(int? locationId, string address)
        {
            Ku_LocationEntity location = null;
            //获取位置坐标点
            if (locationId != null)
            {
                location = locationBll.GetEntity(locationId);
            }
            else
            {
                if (!string.IsNullOrEmpty(address))
                {
                    //根据地址获取热点
                    location = locationBll.AddressToLocation(address);
                }
            }

            return Content(location.ToJson());
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 提交拓客
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CompanyForm(int? keyValue, Ku_CompanyEntity entity)
        {
            companyBll.SaveForm(keyValue, entity);
            return Content("true");
        }
        #endregion
        
    }
}
