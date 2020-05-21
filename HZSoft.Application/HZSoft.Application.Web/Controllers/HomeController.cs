using HZSoft.Application.Busines;
using HZSoft.Application.Busines.BaseManage;
using HZSoft.Application.Busines.CustomerManage;
using HZSoft.Application.Busines.ReportManage;
using HZSoft.Application.Busines.SystemManage;
using HZSoft.Application.Code;
using HZSoft.Application.Entity;
using HZSoft.Application.Entity.SystemManage;
using HZSoft.Util;
using HZSoft.Util.Attributes;
using HZSoft.Util.Log;
using HZSoft.Util.Offices;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HZSoft.Application.Web.Controllers
{
    /// <summary>
    /// 版 本 6.1
    /// 
    /// 创建人：佘赐雄
    /// 日 期：2015.09.01 13:32
    /// 描 述：系统首页
    /// </summary>
    [HandlerLogin(LoginMode.Enforce)]
    public class HomeController : Controller
    {
        UserBLL user = new UserBLL();
        DepartmentBLL department = new DepartmentBLL();

        #region 视图功能
        /// <summary>
        /// 后台框架页
        /// </summary>
        /// <returns></returns>
        public ActionResult AdminDefault()
        {
            //string des = OperatorProvider.Provider.Current().Description;
            //if (!string.IsNullOrEmpty(des))
            //{
            //    //坐标含有逗号
            //    if (des.IndexOf(',') > 0)
            //    {
            //        ViewBag.homeLink="/ReportManage/Report/dms_radius";
            //    }
            //    else
            //    {
            //        ViewBag.homeLink = "/ReportManage/Report/dms_area";
            //    }
            //}
            //else
            //{
            //    ViewBag.homeLink = "/Home/AdminDefaultDesktop";
            //}
            ViewBag.homeLink = "/Home/AdminDefaultDesktop";
            return View();         
        }
        public ActionResult AdminLTE()
        {
            return View();
        }
        public ActionResult AdminWindos()
        {
            return View();
        }
        /// <summary>
        /// 后台框架页
        /// </summary>
        /// <returns></returns>
        public ActionResult AdminBeyond()
        {
            return View();
        }
        /// <summary>
        /// 我的桌面
        /// </summary>
        /// <returns></returns>
        public ActionResult Desktop()
        {
            return View();
        }

        Ku_CompanyCountBLL ku_companycountbll = new Ku_CompanyCountBLL();
        public ActionResult AdminDefaultDesktop()
        {
            DateTime dt = DateTime.Now;  //当前时间
            string dtNow=dt.ToString("yyyy-MM-dd");
            //string ThisWeekStartTime = dt.AddDays(1 - Convert.ToInt32(dt.DayOfWeek.ToString("d"))).ToString("yyyy-MM-dd"); //本周周一（美国本周一为上周日，修改之）
            //string ThisWeekEndTime = dt.AddDays(1 - Convert.ToInt32(dt.DayOfWeek.ToString("d"))+6).ToString("yyyy-MM-dd");  //本周周日

            //string LastWeekStartTime = dt.AddDays(1 - Convert.ToInt32(dt.DayOfWeek.ToString("d")) - 7).ToString("yyyy-MM-dd");//上周一
            //string LastWeekEndTime = dt.AddDays(1 - Convert.ToInt32(dt.DayOfWeek.ToString("d")) - 1).ToString("yyyy-MM-dd");//上周日
            //string MonthStartTime = dt.ToString("yyyy-MM-01");
            //string YearStartTime = dt.ToString("yyyy-01-01");

            //string ThisWeekStartTime = "2018-03-12"; //本周周一（美国本周一为上周日，修改之）
            //string ThisWeekEndTime = "2018-03-18";  //本周周日

            //string LastWeekStartTime = "2018-03-05";//上周一
            //string LastWeekEndTime = "2018-03-11";//上周日
            //string MonthStartTime = "2018-03-01";
            //string YearStartTime = "2018-01-01";
            string ThisWeekStartTime = "2018-01-29"; //本周周一（美国本周一为上周日，修改之）
            string ThisWeekEndTime = "2018-01-31";  //本周周日

            string LastWeekStartTime = "2018-01-22";//上周一
            string LastWeekEndTime = "2018-01-28";//上周日
            string MonthStartTime = "2018-01-01";
            string YearStartTime = "2018-01-01";

            ViewBag.ThisWeekStartTime = ThisWeekStartTime;
            ViewBag.ThisWeekEndTime = ThisWeekEndTime;
            ViewBag.LastWeekStartTime = LastWeekStartTime;
            ViewBag.LastWeekEndTime = LastWeekEndTime;
            ViewBag.MonthStartTime = MonthStartTime;
            ViewBag.MonthEndTime = ThisWeekEndTime;
            ViewBag.YearStartTime = YearStartTime;
            ViewBag.YearEndTime = ThisWeekEndTime;

            var dtCount = ku_companycountbll.GetThisWeekCountData(ThisWeekStartTime, ThisWeekEndTime);
            ViewBag.ThisWeek = dtCount.WeekCount;
            ViewBag.MonthCount = dtCount.MonthCount;
            ViewBag.YearCount = dtCount.YearCount;
            ViewBag.AllCount = dtCount.AllCount;
            return View();
        }
        public ActionResult AdminLTEDesktop()
        {
            return View();
        }
        public ActionResult AdminWindosDesktop()
        {
            return View();
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 访问功能
        /// </summary>
        /// <param name="moduleId">功能Id</param>
        /// <param name="moduleName">功能模块</param>
        /// <param name="moduleUrl">访问路径</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult VisitModule(string moduleId, string moduleName, string moduleUrl)
        {
            LogEntity logEntity = new LogEntity();
            logEntity.CategoryId = 2;
            logEntity.OperateTypeId = ((int)OperationType.Visit).ToString();
            logEntity.OperateType = EnumAttribute.GetDescription(OperationType.Visit);
            logEntity.OperateAccount = OperatorProvider.Provider.Current().Account;
            logEntity.OperateUserId = OperatorProvider.Provider.Current().UserId;
            logEntity.ModuleId = moduleId;
            logEntity.Module = moduleName;
            logEntity.ExecuteResult = 1;
            logEntity.ExecuteResultJson = "访问地址：" + moduleUrl;
            logEntity.WriteLog();
            return Content(moduleId);
        }
        /// <summary>
        /// 离开功能
        /// </summary>
        /// <param name="moduleId">功能模块Id</param>
        /// <returns></returns>
        public ActionResult LeaveModule(string moduleId)
        {
            return null;
        }
        #endregion
    }
}
