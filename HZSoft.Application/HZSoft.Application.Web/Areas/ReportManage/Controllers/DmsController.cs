using HZSoft.Application.Busines;
using HZSoft.Application.Entity.ReportManage;
using HZSoft.Util;
using HZSoft.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using HZSoft.Application.Busines.ReportManage;
using HZSoft.Application.Entity.AuthorizeManage;
using HZSoft.Application.Busines.AuthorizeManage;
using HZSoft.Application.Code;

namespace HZSoft.Application.Web.Areas.ReportManage.Controllers
{
    /// <summary>
    /// 版 本 6.1
    /// 
    /// 创建人：刘晓雷
    /// 日 期：2016.1.14 14:27
    /// 描 述：报表管理
    /// </summary>
    public class DmsController : MvcControllerBase
    {
        DmsBLL dmsBLL = new DmsBLL();

        #region 地图定位展示
        /// <summary>
        /// 拓展pos数量排名
        /// </summary>
        /// <returns></returns>
        public ActionResult dms_map()
        {
            return View();
        }
        /// <summary>
        /// 店铺写字楼地理坐标
        /// </summary>
        /// <param name="queryJson">起始，结束日期json</param>
        /// <returns></returns>
        public ActionResult GetMapData(string queryJson)
        {
            var dt = dmsBLL.GetMapData(queryJson);
            return Content(dt.ToJson());
        }
        #endregion

        #region 地图定位展示
        /// <summary>
        /// 拓展pos数量排名
        /// </summary>
        /// <returns></returns>
        public ActionResult dms_location()
        {
            return View();
        }
        /// <summary>
        /// 店铺写字楼地理坐标
        /// </summary>
        /// <param name="queryJson">起始，结束日期json</param>
        /// <returns></returns>
        public ActionResult GetLocationData(string queryJson)
        {
            var dt = dmsBLL.GetLocationData(queryJson);
            return Content(dt.ToJson());
        }
        #endregion

        #region 地图定位展示
        /// <summary>
        /// 拓展pos数量排名
        /// </summary>
        /// <returns></returns>
        public ActionResult dms_mylocation()
        {
            return View();
        }
        /// <summary>
        /// 店铺写字楼地理坐标
        /// </summary>
        /// <param name="queryJson">起始，结束日期json</param>
        /// <returns></returns>
        public ActionResult GetMyLocationData(string queryJson)
        {
            var dt = dmsBLL.GetMyLocationData(queryJson);
            return Content(dt.ToJson());
        }
        #endregion


        #region 客户销售汇总表
        /// <summary>
        /// 客户销售情况
        /// </summary>
        /// <returns></returns>
        public ActionResult dms_sale()
        {
            return View();
        }

        /// <summary>
        /// 店铺，公司，客户列表Sale_CustomerForm
        /// </summary>
        /// <param name="queryJson">起始，结束日期json</param>
        /// <returns></returns>
        public ActionResult GetCustomerData(string queryJson)
        {
            var dt = dmsBLL.GetCustomerData(queryJson);
            return Content(dt.ToJson());
        }

        /// <summary>
        /// 店铺，公司，客户剩余库存
        /// </summary>
        /// <param name="queryJson">起始，结束日期json</param>
        /// <returns></returns>
        public ActionResult GetSaleCustomerData(string queryJson)
        {
            var dt = dmsBLL.GetSaleCustomerData(queryJson);
            return Content(dt.ToJson());
        }
        #endregion

        #region 员工销售汇总表
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult dms_emp()
        {
            return View();
        }
        /// <summary>
        /// 员工报表
        /// </summary>
        /// <param name="queryJson">起始，结束日期json</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult getDateOrder_emp(string queryJson)
        {
            var dt = dmsBLL.GetDateOrder_emp(queryJson);
            return Content(dt.ToJson());
        }
        #endregion

        #region 员工月报
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult dms_month_emp()
        {
            return View();
        }

        /// <summary>
        /// 员工报表
        /// </summary>
        /// <param name="queryJson">起始，结束日期json</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetMonthEmpData(string queryJson)
        {
            var dt = dmsBLL.GetMonthEmpData(queryJson);
            return Content(dt.ToJson());
        }

        #endregion

        #region POS销售汇总表
        /// <summary>
        /// 拓展pos数量排名
        /// </summary>
        /// <returns></returns>
        public ActionResult dms_pos()
        {
            return View();
        }
        /// <summary>
        /// pos申请排名
        /// </summary>
        /// <param name="queryJson">起始，结束日期json</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetPosData(string queryJson)
        {
            var dt = dmsBLL.GetPosData(queryJson);
            return Content(dt.ToJson());
        }
        #endregion


        #region 首页报表
        /// <summary>
        /// 周行业分布
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetWeekAreaData(string start,string end)
        {
            var dt = dmsBLL.GetWeekAreaData(start, end);
            return Content(dt.ToJson());
        }
        /// <summary>
        /// 周行业分布
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetWeekSectorData(string start, string end)
        {
            var dt = dmsBLL.GetWeekSectorData(start, end);
            return Content(dt.ToJson());
        }
        /// <summary>
        /// POI类别分布
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetPoiTypeData(string start, string end)
        {
            var dt = dmsBLL.GetPoiTypeData(start, end);
            return Content(dt.ToJson());
        }
        /// <summary>
        /// 年曲线
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetCompanyYearData()
        {
            var dt = dmsBLL.GetCompanyYearData();
            return Content(dt.ToJson());
        }
        #endregion


        #region 月报指标
        /// <summary>
        /// 员工查看次数报表
        /// </summary>
        /// <returns></returns>
        public ActionResult dms_month_see()
        {
            return View();
        }
        /// <summary>
        /// 员工查看次数数据
        /// </summary>
        /// <param name="queryJson">起始，结束日期json</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetMonthSeeData(string queryJson)
        {
            var dt = dmsBLL.GetMonthSeeData(queryJson);
            return Content(dt.ToJson());
        }
        /// <summary>
        /// 员工获取次数数据
        /// </summary>
        /// <param name="queryJson">起始，结束日期json</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetMonthObtainData(string queryJson)
        {
            var dt = dmsBLL.GetMonthObtainData(queryJson);
            return Content(dt.ToJson());
        }
        
        /// <summary>
        /// 员工跟进条数数据
        /// </summary>
        /// <param name="queryJson">起始，结束日期json</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetMonthRecordData(string queryJson)
        {
            var dt = dmsBLL.GetMonthRecordData(queryJson);
            return Content(dt.ToJson());
        }

        /// <summary>
        /// 员工跟进状态
        /// </summary>
        /// <param name="queryJson">起始，结束日期json</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetMonthFollowStateData(string queryJson)
        {
            var dt = dmsBLL.GetMonthFollowStateData(queryJson);
            return Content(dt.ToJson());
        }

        #endregion



        #region 员工指标
        /// <summary>
        /// 员工指标
        /// </summary>
        /// <returns></returns>
        public ActionResult dms_location_emp()
        {
            return View();
        }


        /// <summary>
        /// 员工获取个数
        /// </summary>
        /// <param name="queryJson">起始，结束日期json</param>
        /// <returns></returns>
        public ActionResult GetEmpObtainData(string queryJson)
        {
            var dt = dmsBLL.GetEmpObtainData(queryJson);
            return Content(dt.ToJson());
        }
        /// <summary>
        /// 员工获取个数
        /// </summary>
        /// <param name="queryJson">起始，结束日期json</param>
        /// <returns></returns>
        public ActionResult GetEmpRecordData(string queryJson)
        {
            var dt = dmsBLL.GetEmpRecordData(queryJson);
            return Content(dt.ToJson());
        }
        /// <summary>
        /// 员工获取个数
        /// </summary>
        /// <param name="queryJson">起始，结束日期json</param>
        /// <returns></returns>
        public ActionResult GetEmpSeeData(string queryJson)
        {
            var dt = dmsBLL.GetEmpSeeData(queryJson);
            return Content(dt.ToJson());
        }


        #endregion
    }
}
