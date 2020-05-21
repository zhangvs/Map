using HZSoft.Application.Busines.BaseManage;
using HZSoft.Application.Busines.CustomerManage;
using HZSoft.Application.Busines.SystemManage;
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
    /// 订单余量
    /// </summary>
    //[HandlerWXAuthorizeAttribute(LoginMode.Enforce)]
    [HandlerWXLoginAttribute(LoginMode.Enforce)]
    public class POS_YuController : Controller
    {
        private CodeRuleBLL codeRuleBLL = new CodeRuleBLL();
        WeChat_UsersBLL wechatUserBll = new WeChat_UsersBLL();
        UserBLL userBLL = new UserBLL();
        POS_OfficeCompanyBLL posOfficeCompanyBll = new POS_OfficeCompanyBLL();
        POS_OfficeBLL posOfficeBll = new POS_OfficeBLL();
        Sale_CustomerBLL yuBLL = new Sale_CustomerBLL();
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="CustomerId"></param>
        /// <param name="CustomerName"></param>
        /// <param name="CustomerCompany"></param>
        /// <returns></returns>
        public ActionResult YuForm(string CustomerId, string CustomerName, string CustomerCompany)
        {
            ViewBag.ModifyUserName = Code.OperatorProvider.Provider.Current().UserName;
            ViewBag.ModifyUserId = Code.OperatorProvider.Provider.Current().UserId;
            ViewBag.CustomerId = CustomerId;
            ViewBag.CustomerName = CustomerName;
            ViewBag.CustomerCompany = CustomerCompany;
            return View();

            //ViewBag.ModifyUserName = "梁茂斌";
            //ViewBag.ModifyUserId = "5348b6c5-0f20-4fda-bee4-b5d3ed50ca5d";   
            //ViewBag.CustomerId = "c2e300d4-ed46-45a6-8d34-d42d19a281b9";
            //ViewBag.CustomerCompany = "张三店11";
            //return View();
        }

        /// <summary>
        /// 获取实体 
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>返回对象Json</returns>
        public ActionResult GetFormJson(string keyValue)
        {
            var data = yuBLL.GetEntity(keyValue);
            var childData = yuBLL.GetDetails(keyValue);
            var jsonData = new { entity = data, childEntity = childData };
            return Content(jsonData.ToJson());
        }


        /// <summary>
        /// 提交剩余库存单信息
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="strEntity"></param>
        /// <param name="strChildEntitys"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SaveForm(string keyValue, string strEntity, string strChildEntitys)
        {
            var entity = strEntity.ToObject<Sale_CustomerEntity>();
            var childEntitys = strChildEntitys.ToList<Sale_Customer_ItemEntity>();

            yuBLL.SaveForm(keyValue, entity, childEntitys);
            return Content("true");
        }
    }
}
