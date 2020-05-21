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
    /// 热点
    /// </summary>
    //[HandlerWXAuthorizeAttribute(LoginMode.Enforce)]
    [HandlerWXLoginAttribute(LoginMode.Enforce)]
    public class NearbyController : Controller
    {
        WeChat_UsersBLL wechatUserBll = new WeChat_UsersBLL();
        UserBLL userBLL = new UserBLL();
        Ku_LocationBLL locationBll = new Ku_LocationBLL();


        #region 视图功能
        /// <summary>
        /// 附近的热点
        /// </summary>
        /// <returns></returns>
        [HandlerLocationAttribute(LoginMode.Enforce)]
        public ActionResult NearbyMap()
        {
            ViewBag.currentLon = Request.Cookies["currlong"].Value;
            ViewBag.currentLat = Request.Cookies["currlat"].Value;
            return View();

        }


        /// <summary>
        /// 附近热点列表页
        /// </summary>
        /// <param name="searchName"></param>
        /// <returns></returns>
        [HandlerLocationAttribute(LoginMode.Enforce)]
        public ActionResult NearbyList(string searchName)
        {
            string currentLon = Request.Cookies["currlong"].Value;
            string currentLat = Request.Cookies["currlat"].Value;

            QueryWhere entity = new QueryWhere()
            {
                SearchName = searchName,
                wxLat = currentLat,
                wxLon = currentLon
            };
            string queryJson = entity.ToJson();
            ViewBag.listgt = locationBll.GetList(queryJson);
            return View();
            //string currentLon = "";
            //string currentLat = "";
            //if (currentLon == "" || currentLat == "")
            //{
            //    currentLon = "118.33954";
            //    currentLat = "35.06199";
            //}
            //QueryWhere entity = new QueryWhere()
            //{
            //    SearchName = searchName,
            //    wxLat = currentLat,
            //    wxLon = currentLon
            //};
            //string queryJson = entity.ToJson();
            //ViewBag.listgt = locationBll.GetList(queryJson);
            //return View();
        }


        /// <summary>
        /// 添加店铺
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult NearbyForm()
        {
            var jssdkUiPackage = Senparc.Weixin.MP.Helpers.JSSDKHelper.GetJsSdkUiPackage(WeixinConfig.AppID, WeixinConfig.AppSecret, Request.Url.AbsoluteUri.Split('#')[0]);
            ViewBag.JSSDKUiPackage = jssdkUiPackage;
            return View();
        }

        #endregion

        #region 获取数据
        /// <summary>
        /// 列表数据
        /// </summary>
        /// <returns></returns>
        [HandlerLocationAttribute(LoginMode.Enforce)]
        public ActionResult GetList()
        {
            string currentLon = Request.Cookies["currlong"].Value;
            string currentLat = Request.Cookies["currlat"].Value;

            QueryWhere entity = new QueryWhere()
            {
                wxLat = currentLat,
                wxLon = currentLon,
            };
            string queryJson = entity.ToJson();
            var data = locationBll.GetList(queryJson);
            return Content(data.ToJson());
        }
        /// <summary>
        /// 写字楼详情
        /// </summary>
        /// <returns></returns>
        public ActionResult GetFormJson(int? keyValue)
        {
            string returnUrl = Request.Url.ToString();
            Ku_LocationEntity entity = locationBll.GetEntity(keyValue);
            return Content(entity.ToJson());
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 提交拓客
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult NearbyForm(int? keyValue, Ku_LocationEntity entity)
        {
            //double Lon = (double)entity.wxLon;
            //double Lat = (double)entity.wxLat;
            //double bdLat, bdLon;
            //MapConverter.GCJ02ToBD09(Lat, Lon, out bdLat, out bdLon);
            //entity.bdLat = (decimal)bdLat;
            //entity.bdLon = (decimal)bdLon;
            
            
            ////根据坐标获取省市县信息
            //string url = @"http://restapi.amap.com/v3/geocode/regeo?location="+ Lon + "," + Lat + "&key=cf3dd05a8192fd1839628b39e589c89e&radius=1000&extensions=all";//output=XML&
            //string responseJson = HttpClientHelper.Get(url);
            //RestLocation restLocation = JsonConvert.DeserializeObject<RestLocation>(responseJson.Replace("[]", "\"\""));
            //entity.Province = restLocation.regeocode.addressComponent.province;
            //entity.City = restLocation.regeocode.addressComponent.city;
            //entity.CityCode = restLocation.regeocode.addressComponent.citycode;
            //entity.District = restLocation.regeocode.addressComponent.district;

            //插入热点表
            locationBll.SaveForm(keyValue, entity);

            return Content("true");
        }
        #endregion
        
    }
}
