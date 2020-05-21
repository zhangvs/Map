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
    /// 扫街：订单
    /// </summary>
    //[HandlerWXAuthorizeAttribute(LoginMode.Enforce)]
    [HandlerWXLoginAttribute(LoginMode.Enforce)]
    public class POS_SaleController : Controller
    {
        private CodeRuleBLL codeRuleBLL = new CodeRuleBLL();
        Sales_ContractBLL saleBLL = new Sales_ContractBLL();
        POS_ProductBLL productBLL = new POS_ProductBLL();


        /// <summary>
        /// 订单列表
        /// </summary>
        /// <param name="searchName"></param>
        /// <returns></returns>
        public ActionResult SaleList(string searchName)
        {
            QueryWhere entity = new QueryWhere()
            {
                SearchName = searchName
            };
            string queryJson = entity.ToJson();
            var listgt = saleBLL.GetList(queryJson);

            foreach (var sale in listgt)
            {
                var listgtitem = saleBLL.GetDetails(sale.Id);
                foreach (var item in listgtitem)
                {
                    //产品：50(个) * 10 = 500
                    sale.SpareField += item.ProductName + ":&nbsp;" + item.Count + "*" + item.ProductUnit + "=" + item.Amount + "<br/> ";
                }
            }

            ViewBag.listgt = listgt;
            return View();

        }
        /// <summary>
        /// 订单详情页面
        /// </summary>
        /// <param name="CustomerId"></param>
        /// <param name="CustomerName"></param>
        /// <param name="CustomerCompany"></param>
        /// <returns></returns>
        public ActionResult SaleForm(string CustomerId, string CustomerName, string CustomerCompany)
        {
            if (Request["keyValue"] == null)
            {
                ViewBag.ContractNum = codeRuleBLL.GetBillCode("12cf9ca5-f1e3-49e5-8360-8cd4271b854f");
            }

            ViewBag.UserName = Code.OperatorProvider.Provider.Current().UserName;
            ViewBag.UserId = Code.OperatorProvider.Provider.Current().UserId;
            ViewBag.CustomerId = CustomerId;
            ViewBag.CustomerName = CustomerName;
            ViewBag.CustomerCompany = CustomerCompany;
            ViewBag.productList = productBLL.GetList("{\"ParentId\":\"0\"}");
            return View();
            //ViewBag.UserName = "梁茂斌";
            //ViewBag.UserId = "5348b6c5-0f20-4fda-bee4-b5d3ed50ca5d";
            //ViewBag.CustomerId = "c2e300d4-ed46-45a6-8d34-d42d19a281b9";
            //ViewBag.CustomerName = "张三";
            //ViewBag.CustomerCompany = "张三店11";
            //ViewBag.productList = productBLL.GetList("{\"ParentId\":\"0\"}");
            //return View();
        }

        /// <summary>
        /// 获取实体 
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>返回对象Json</returns>
        public ActionResult GetFormJson(string keyValue)
        {
            var data = saleBLL.GetEntity(keyValue);
            var childData = saleBLL.GetDetails(keyValue);
            var jsonData = new { entity = data, childEntity = childData };
            return Content(jsonData.ToJson());
        }


        //提交出库单信息
        [HttpPost]
        public ActionResult SaveForm(string keyValue, string strEntity, string strChildEntitys)
        {
            var entity = strEntity.ToObject<Sales_ContractEntity>();
            var childEntitys = strChildEntitys.ToList<Sales_Contract_ItemEntity>();
            saleBLL.SaveForm(keyValue, entity, childEntitys);
            return Content("true");
        }
    }
}
