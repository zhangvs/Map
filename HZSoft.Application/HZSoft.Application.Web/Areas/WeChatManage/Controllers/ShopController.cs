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
using HZSoft.Util.Extension;
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
    /// 扫街店铺
    /// </summary>
    //[HandlerWXAuthorizeAttribute(LoginMode.Enforce)]
    [HandlerWXLoginAttribute(LoginMode.Enforce)]
    public class ShopController : BaseWxUserController
    {
        WeChat_UsersBLL wechatUserBll = new WeChat_UsersBLL();
        UserBLL userBLL = new UserBLL();
        POS_ShopBLL posShopBll = new POS_ShopBLL();

        #region 视图功能

        /// <summary>
        /// 商品列表
        /// </summary>
        /// <param name="searchName">搜索关键字</param>
        /// <returns></returns>
        [HandlerLocationAttribute(LoginMode.Enforce)]
        public ActionResult ShopList(string searchName)
        {
            string currentLng = Request.Cookies["currlong"].Value;
            string currentLat = Request.Cookies["currlat"].Value;
            QueryWhere entity = new QueryWhere()
            {
                SearchName = searchName,
                wxLat = currentLat,
                wxLon = currentLng
            };
            string queryJson = entity.ToJson();
            ViewBag.listgt = posShopBll.GetList(queryJson);
            return View();
        }
        /// <summary>
        /// 添加店铺
        /// </summary>
        /// <returns></returns>
        [HandlerLocationAttribute(LoginMode.Enforce)]
        public ActionResult AddShop(string currentLon,string currentLat,string address)
        {
            var jssdkUiPackage = Senparc.Weixin.MP.Helpers.JSSDKHelper.GetJsSdkUiPackage(WeixinConfig.AppID, WeixinConfig.AppSecret, Request.Url.AbsoluteUri.Split('#')[0]);
            ViewBag.JSSDKUiPackage = jssdkUiPackage;
            //ViewBag.SellerName = HttpUtility.UrlDecode(Request.Cookies["SellerName"].Value, Encoding.GetEncoding("UTF-8"));
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
        public ActionResult ShopMap()
        {
            //ViewBag.currentLng = Request.Cookies["currlong"] == null ? "118.33954" : Request.Cookies["currlong"].Value;
            //ViewBag.currentLat = Request.Cookies["currlat"] == null ? "35.06199" : Request.Cookies["currlat"].Value;
            ViewBag.currentLng = Request.Cookies["currlong"].Value;
            ViewBag.currentLat = Request.Cookies["currlat"].Value;
            return View();
        }
        /// <summary>
        /// 店铺地图
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
                wxLon = currentLng
            };
            string queryJson = entity.ToJson();
            var data = posShopBll.GetList(queryJson);

            return Content(data.ToJson());
        }

        /// <summary>
        /// 店铺详情
        /// </summary>
        /// <returns></returns>
        public ActionResult ShopDetail(string keyValue)
        {
            POS_ShopEntity entity = posShopBll.GetEntity(keyValue);
            ViewBag.model = entity;
            return View();
        }
        #endregion

        #region 获取数据
        /// <summary>
        /// 店铺详情
        /// </summary>
        /// <returns></returns>
        public ActionResult GetFormJson(string keyValue)
        {
            string returnUrl = Request.Url.ToString();
            POS_ShopEntity entity = posShopBll.GetEntity(keyValue);
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
        public ActionResult AddShop(string keyValue,POS_ShopEntity entity)
        {
            //微信火星转百度
            double Lon = double.Parse(entity.wxLon);
            double Lat = double.Parse(entity.wxLat);
            double bdLat, bdLon;
            MapConverter.GCJ02ToBD09(Lat, Lon, out bdLat, out bdLon);
            entity.bdLat = bdLat.ToString();
            entity.bdLon = bdLon.ToString();
            entity.OpenId = CurrentWxUser.OpenId;
            entity.NickName = CurrentWxUser.NickName;

            //根据坐标获取省市县信息
            string url = @"http://restapi.amap.com/v3/geocode/regeo?location=" + Lon + "," + Lat + "&key=cf3dd05a8192fd1839628b39e589c89e&radius=1000&extensions=all";//output=XML&
            string responseJson = HttpClientHelper.Get(url);
            RestLocation restLocation = JsonConvert.DeserializeObject<RestLocation>(responseJson.Replace("[]", "\"\""));
            entity.Province = restLocation.regeocode.addressComponent.province;
            entity.City = restLocation.regeocode.addressComponent.city;
            entity.CityCode = restLocation.regeocode.addressComponent.citycode;
            entity.District = restLocation.regeocode.addressComponent.district;

            //插入店铺表
            posShopBll.SaveForm(keyValue, entity);

            return Content("true");
        }
        #endregion

        private TrailRecordBLL chancetrailbll = new TrailRecordBLL();
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="objectId">Id</param>
        /// <returns>返回列表Json</returns>
        [HttpGet]
        public ActionResult GetListJson(string objectId)
        {
            var data = chancetrailbll.GetList(objectId);
            Dictionary<string, string> dictionaryDate = new Dictionary<string, string>();
            foreach (TrailRecordEntity item in data)
            {
                string key = item.CreateDate.ToDate().ToString("yyyy-MM-dd");
                string currentTime = DateTime.Now.ToString("yyyy-MM-dd");
                if (item.CreateDate.ToDate().ToString("yyyy-MM-dd") == currentTime)
                {
                    key = "今天";
                }
                if (!dictionaryDate.ContainsKey(key))
                {
                    dictionaryDate.Add(key, item.CreateDate.ToDate().ToString("yyyy-MM-dd"));
                }
            }
            var jsonData = new
            {
                timeline = dictionaryDate,
                rows = data,
            };
            return Content(jsonData.ToJson());
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult SaveForm(string keyValue, TrailRecordEntity entity)
        {
            chancetrailbll.SaveForm(keyValue, entity);
            return Content("操作成功。");
        }
        #endregion
    }
}
