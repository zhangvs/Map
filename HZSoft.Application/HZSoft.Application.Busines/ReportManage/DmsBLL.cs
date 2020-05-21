using HZSoft.Application.Entity.AuthorizeManage;
using HZSoft.Application.Entity.ReportManage;
using HZSoft.Application.IService.ReportManage;
using HZSoft.Application.Service.ReportManage;
using System;
using System.Collections.Generic;
using System.Data;

namespace HZSoft.Application.Busines.ReportManage
{
    /// <summary>
    /// 版 本 6.1
    /// 
    /// 创建人：刘晓雷
    /// 日 期：2016.1.14 14:27
    /// 描 述：报表模板管理
    /// </summary>
    public class DmsBLL
    {
        private IDmsService service = new DmsService();
        /// <summary>
        /// 员工报表
        /// </summary>
        /// <param name="postData">起始，结束日期json</param>
        /// <returns></returns>
        public DataTable GetDateOrder_emp(string queryJson)
        {
            return service.GetDateOrder_emp(queryJson);
        }
        /// <summary>
        /// 员工报表
        /// </summary>
        /// <param name="postData">起始，结束日期json</param>
        /// <returns></returns>
        public DataTable GetPosData(string queryJson)
        {
            return service.GetPosData(queryJson);
        }
        /// <summary>
        /// 店铺写字楼，坐标
        /// </summary>
        /// <param name="postData">起始，结束日期json</param>
        /// <returns></returns>
        public DataTable GetMapData(string queryJson)
        {
            return service.GetMapData(queryJson);
        }

        /// <summary>
        /// 二合一
        /// </summary>
        /// <param name="postData">起始，结束日期json</param>
        /// <returns></returns>
        public DataTable GetLocationData(string queryJson)
        {
            return service.GetLocationData(queryJson);
        }

        /// <summary>
        /// 我的私池坐标
        /// </summary>
        /// <param name="postData">起始，结束日期json</param>
        /// <returns></returns>
        public DataTable GetMyLocationData(string queryJson)
        {
            return service.GetMyLocationData(queryJson);
        }

        /// <summary>
        /// 公司和店铺，客户下拉列表
        /// </summary>
        /// <param name="postData">起始，结束日期json</param>
        /// <returns></returns>
        public DataTable GetCustomerData(string queryJson)
        {
            return service.GetCustomerData(queryJson);
        }
        /// <summary>
        /// 公司剩余库存
        /// </summary>
        /// <param name="postData">起始，结束日期json</param>
        /// <returns></returns>
        public DataTable GetSaleCustomerData(string queryJson)
        {
            return service.GetSaleCustomerData(queryJson);
        }

        /// <summary>
        /// 员工月报
        /// </summary>
        /// <param name="postData">起始，结束日期json</param>
        /// <returns></returns>
        public DataTable GetMonthEmpData(string queryJson)
        {
            return service.GetMonthEmpData(queryJson);
        }


        #region 首页数据

        public DataTable GetCompanyYearData()
        {
            return service.GetCompanyYearData();
        }
        public DataTable GetWeekAreaData(string start, string end)
        {
            return service.GetWeekAreaData(start, end);
        }
        public DataTable GetWeekSectorData(string start, string end)
        {
            return service.GetWeekSectorData(start, end);
        }
        public DataTable GetPoiTypeData(string start, string end)
        {
            return service.GetPoiTypeData(start, end);
        }
        #endregion

        #region 月报指标
        /// <summary>
        /// 员工查看次数月报
        /// </summary>
        /// <param name="postData">起始，结束日期json</param>
        /// <returns></returns>
        public DataTable GetMonthSeeData(string queryJson)
        {
            return service.GetMonthSeeData(queryJson);
        }

        /// <summary>
        /// 员工获取次数月报
        /// </summary>
        /// <param name="postData">起始，结束日期json</param>
        /// <returns></returns>
        public DataTable GetMonthObtainData(string queryJson)
        {
            return service.GetMonthObtainData(queryJson);
        }
        /// <summary>
        /// 员工跟进条数月报
        /// </summary>
        /// <param name="postData">起始，结束日期json</param>
        /// <returns></returns>
        public DataTable GetMonthRecordData(string queryJson)
        {
            return service.GetMonthRecordData(queryJson);
        }
        /// <summary>
        /// 员工跟进状态
        /// </summary>
        /// <param name="postData">起始，结束日期json</param>
        /// <returns></returns>
        public DataTable GetMonthFollowStateData(string queryJson)
        {
            return service.GetMonthFollowStateData(queryJson);
        }
        #endregion

        #region 员工指标
        /// <summary>
        /// 员工获取个数
        /// </summary>
        /// <param name="postData">起始，结束日期json</param>
        /// <returns></returns>
        public DataTable GetEmpObtainData(string queryJson)
        {
            return service.GetEmpObtainData(queryJson);
        }
        /// <summary>
        /// 员工获取个数
        /// </summary>
        /// <param name="postData">起始，结束日期json</param>
        /// <returns></returns>
        public DataTable GetEmpRecordData(string queryJson)
        {
            return service.GetEmpRecordData(queryJson);
        }
        /// <summary>
        /// 员工获取个数
        /// </summary>
        /// <param name="postData">起始，结束日期json</param>
        /// <returns></returns>
        public DataTable GetEmpSeeData(string queryJson)
        {
            return service.GetEmpSeeData(queryJson);
        }

        #endregion
    }
}
