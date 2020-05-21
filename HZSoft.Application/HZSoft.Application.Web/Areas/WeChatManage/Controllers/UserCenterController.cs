using HZSoft.Application.Busines.CustomerManage;
using HZSoft.Application.Busines.WeChatManage;
using HZSoft.Application.Code;
using HZSoft.Application.Entity.WeChatManage;
using HZSoft.Application.Web.Utility;
using HZSoft.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HZSoft.Application.Web.Areas.WeChatManage.Controllers
{
    /// <summary>
    /// 个人中心页面
    /// </summary>
    [HandlerWXLoginAttribute(LoginMode.Enforce)]
    public class UserCenterController : Controller
    {
        WeChat_UsersBLL wechatUserBll = new WeChat_UsersBLL();
        Ku_CompanyBLL kuCompanyBll = new Ku_CompanyBLL();
        Ku_CompanySeeBLL kuCompanySeeBll = new Ku_CompanySeeBLL();

        /// <summary>
        /// 个人中心
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            WeChat_UsersEntity entity = wechatUserBll.GetEntity(CurrentWxUser.OpenId);
            DataTable dt= kuCompanyBll.GetFollowState();
            ViewBag.state3 = dt.Rows[0][0].ToString();
            ViewBag.state2 = dt.Rows[1][0].ToString();
            ViewBag.state4 = dt.Rows[2][0].ToString();
            ViewBag.state0 = dt.Rows[3][0].ToString();
            return View(entity);
        }

        /// <summary>
        /// 个人私池
        /// </summary>
        /// <returns></returns>
        public ActionResult MyCompany(string searchName,string state)
        {
            ViewBag.listgt = kuCompanyBll.GetListByUserId(searchName,state);
            return View();
        }

        /// <summary>
        /// 浏览记录
        /// </summary>
        /// <returns></returns>
        public ActionResult MyBrowse(string searchName)
        {
            ViewBag.listgt = kuCompanySeeBll.GetListByUserId(searchName);
            return View();
        }


    }
}
