using HZSoft.Application.Entity.AuthorizeManage;
using HZSoft.Application.Entity.ReportManage;
using System.Collections.Generic;
using System.Data;

namespace HZSoft.Application.IService.ReportManage
{
    /// <summary>
    /// 版 本 6.1
    /// 
    /// 创建人：刘晓雷
    /// 日 期：2016.1.14 14:27
    /// 描 述：报表模板管理
    /// </summary>
    public interface IDmsService
    {
        #region 获取数据
        /// <summary>
        /// 定位报表
        /// </summary>
        /// <param name="postData">起始，结束日期json</param>
        /// <returns></returns>
        DataTable GetMapData(string queryJson);
        DataTable GetLocationData(string queryJson);
        DataTable GetMyLocationData(string queryJson);
        /// <summary>
        /// 员工报表
        /// </summary>
        /// <param name="postData">起始，结束日期json</param>
        /// <returns></returns>
        DataTable GetDateOrder_emp(string queryJson);
        /// <summary>
        /// 员工报表
        /// </summary>
        /// <param name="postData">起始，结束日期json</param>
        /// <returns></returns>
        DataTable GetPosData(string queryJson);
        DataTable GetCustomerData(string queryJson);
        DataTable GetSaleCustomerData(string queryJson);
        DataTable GetMonthEmpData(string queryJson);
        DataTable GetCompanyYearData(); 
        DataTable GetWeekAreaData(string start, string end);
        DataTable GetPoiTypeData(string start, string end); 
        DataTable GetWeekSectorData(string start, string end);

        //月报指标
        DataTable GetMonthSeeData(string queryJson);
        DataTable GetMonthObtainData(string queryJson);
        DataTable GetMonthRecordData(string queryJson);
        DataTable GetMonthFollowStateData(string queryJson);

        //员工指标
        DataTable GetEmpObtainData(string queryJson);
        DataTable GetEmpRecordData(string queryJson);
        DataTable GetEmpSeeData(string queryJson);

        #endregion

    }
}
