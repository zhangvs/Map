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
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace HZSoft.Application.Web.Areas.WeChatManage.Controllers
{
    /// <summary>
    /// 扫街：写字楼
    /// </summary>
    [HandlerWXLoginAttribute(LoginMode.Enforce)]
    public class OfficeController : Controller
    {
        WeChat_UsersBLL wechatUserBll = new WeChat_UsersBLL();
        UserBLL userBLL = new UserBLL();
        POS_OfficeBLL posOfficeBll = new POS_OfficeBLL();

        #region 视图功能
        /// <summary>
        /// 写字楼列表
        /// </summary>
        /// <param name="searchName"></param>
        /// <returns></returns>
        [HandlerLocationAttribute(LoginMode.Enforce)]
        public ActionResult OfficeList(string searchName)
        {
            string currentLng = Request.Cookies["currlong"].Value;
            string currentLat = Request.Cookies["currlat"].Value;
            QueryWhere entity = new QueryWhere()
            {
                SearchName = searchName,
                //OpenId = CurrentWxUser.OpenId,
                wxLat = currentLat,
                wxLon = currentLng
            };
            string queryJson = entity.ToJson();
            ViewBag.listgt = posOfficeBll.GetList(queryJson);
            return View();
        }

        /// <summary>
        /// 添加店铺
        /// </summary>
        /// <returns></returns>
        [HandlerLocationAttribute(LoginMode.Enforce)]
        public ActionResult AddOffice(string currentLon, string currentLat, string address)
        {
            var jssdkUiPackage = Senparc.Weixin.MP.Helpers.JSSDKHelper.GetJsSdkUiPackage(WeixinConfig.AppID, WeixinConfig.AppSecret, Request.Url.AbsoluteUri.Split('#')[0]);
            ViewBag.JSSDKUiPackage = jssdkUiPackage;
            ViewBag.SellerName = Code.OperatorProvider.Provider.Current().UserName;
            ViewBag.SellerId = Code.OperatorProvider.Provider.Current().UserId;
            ViewBag.currentLon = currentLon;
            ViewBag.currentLat = currentLat;
            ViewBag.address = address;
            return View();
        }

        /// <summary>
        /// 店铺地图
        /// </summary>
        /// <returns></returns>
        [HandlerLocationAttribute(LoginMode.Enforce)]
        public ActionResult OfficeMap()
        {
            ViewBag.currentLng = Request.Cookies["currlong"].Value;
            ViewBag.currentLat = Request.Cookies["currlat"].Value;
            return View();
        }

        /// <summary>
        /// 写字楼地图
        /// </summary>
        /// <returns></returns>
        [HandlerLocationAttribute(LoginMode.Enforce)]
        public ActionResult MapList()
        {
            string currentLng = Request.Cookies["currlong"].Value;
            string currentLat = Request.Cookies["currlat"].Value;
            QueryWhere entity = new QueryWhere()
            {
                wxLat = currentLat,
                wxLon = currentLng,
            };
            string queryJson = entity.ToJson();
            var data = posOfficeBll.GetList(queryJson);
            return Content(data.ToJson());
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
            POS_OfficeEntity entity = posOfficeBll.GetEntity(keyValue);
            if (string.IsNullOrEmpty(entity.SellerId))
            {
                entity.SellerName = Code.OperatorProvider.Provider.Current().UserName;
                entity.SellerId = Code.OperatorProvider.Provider.Current().UserId;
            }

            return Content(entity.ToJson());
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 提交拓客
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddOffice(string keyValue, POS_OfficeEntity entity)
        {
            double Lon = double.Parse(entity.wxLon);
            double Lat = double.Parse(entity.wxLat);
            double bdLat, bdLon;
            MapConverter.GCJ02ToBD09(Lat, Lon, out bdLat, out bdLon);
            entity.bdLat = bdLat.ToString();
            entity.bdLon = bdLon.ToString();

            entity.OpenId = CurrentWxUser.OpenId;
            entity.NickName = CurrentWxUser.NickName;

            //根据坐标获取省市县信息
            string url = @"http://restapi.amap.com/v3/geocode/regeo?location="+ Lon + "," + Lat + "&key=cf3dd05a8192fd1839628b39e589c89e&radius=1000&extensions=all";//output=XML&
            string responseJson = HttpClientHelper.Get(url);
            RestLocation restLocation = JsonConvert.DeserializeObject<RestLocation>(responseJson.Replace("[]", "\"\""));
            entity.Province = restLocation.regeocode.addressComponent.province;
            entity.City = restLocation.regeocode.addressComponent.city;
            entity.CityCode = restLocation.regeocode.addressComponent.citycode;
            entity.District = restLocation.regeocode.addressComponent.district;

            //插入店铺表
            posOfficeBll.SaveForm(keyValue, entity);

            return Content("true");
        }
        #endregion
        
    }
}
