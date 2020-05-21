using HZSoft.Application.Entity.ReportManage;
using HZSoft.Application.IService.ReportManage;
using HZSoft.Data;
using HZSoft.Data.Repository;
using HZSoft.Util.Extension;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using HZSoft.Util;
using HZSoft.Application.Entity.AuthorizeManage;
using HZSoft.Application.Code;
using System.Text.RegularExpressions;

namespace HZSoft.Application.Service.ReportManage
{
    /// <summary>
    /// 版 本 6.1
    /// 
    /// 创建人：佘赐雄
    /// 日 期：2016.1.14 14:27
    /// 描 述：报表模板管理
    /// </summary>
    public class DmsService : RepositoryFactory, IDmsService
    {
        #region 获取数据
        /// <summary>
        /// 员工报表
        /// </summary>
        /// <param name="queryJson">起始，结束日期json</param>
        /// <returns></returns>
        public DataTable GetDateOrder_emp(string queryJson)
        {
            var queryParam = queryJson.ToJObject();
            string strSql = "SELECT UserName AS name, SUM(TotalAmount) AS emp_amount, count(1) AS emp_count FROM Sales_Contract where 1=1";
            if (!OperatorProvider.Provider.Current().IsSystem)
            {
                string dataAutor = string.Format(OperatorProvider.Provider.Current().DataAuthorize.ReadAutorize, OperatorProvider.Provider.Current().UserId);
                strSql += " and UserId in (" + dataAutor + ")";
            }
            //单据日期
            if (!queryParam["StartTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
            {
                DateTime startTime = queryParam["StartTime"].ToDate();
                DateTime endTime = queryParam["EndTime"].ToDate().AddDays(1);
                strSql += " and CreateDate >= '" + startTime + "' and CreateDate < '" + endTime + "'";
            }
            strSql += " group by UserName order by emp_amount desc";

            return this.BaseRepository().FindTable(strSql.ToString());
        }


        /// <summary>
        /// pos申请报表
        /// </summary>
        /// <param name="queryJson">起始，结束日期json</param>
        /// <returns></returns>
        public DataTable GetPosData(string queryJson)
        {
            var queryParam = queryJson.ToJObject();
            string strSql = "SELECT recommender AS name, count(1) AS emp_count FROM POS_Apply where 1=1 ";
            //单据日期
            if (!queryParam["StartTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
            {
                DateTime startTime = queryParam["StartTime"].ToDate();
                DateTime endTime = queryParam["EndTime"].ToDate().AddDays(1);
                strSql += " and CreateTime >= '" + startTime + "' and CreateTime < '" + endTime + "'";
            }
            if (!OperatorProvider.Provider.Current().IsSystem && OperatorProvider.Provider.Current().Account != "pos")
            {
                string dataAutor = string.Format(OperatorProvider.Provider.Current().DataAuthorize.ReadAutorize, OperatorProvider.Provider.Current().UserId);
                dataAutor = dataAutor.Replace("select UserId from", "select RealName from");
                strSql += " and Recommender in (" + dataAutor + ")";
            }
            strSql += " group by recommender order by emp_count desc";

            return this.BaseRepository().FindTable(strSql.ToString());
        }
        /// <summary>
        /// Map
        /// </summary>
        /// <param name="queryJson">起始，结束日期json</param>
        /// <returns></returns>
        public DataTable GetMapData(string queryJson)
        {
            var queryParam = queryJson.ToJObject();
            string strSql = @"SELECT * FROM (
SELECT 'Shop' type, Id, bdlon, bdlat, state, issee, shopname name,shopaddress address,province,city,district, shoppicture picture,telphone,CreateTime,SellerId,1 count FROM POS_Shop 
UNION ALL
SELECT 'Office' type, o.Id, o.bdlon, o.bdlat, o.state, o.issee, o.officename name,o.officeaddress address,province,city,district, officepicture picture,'' telphone,o.CreateTime,o.SellerId,count(*) count FROM POS_Office o
LEFT JOIN POS_OfficeCompany com ON o.Id=com.OfficeId
GROUP BY o.Id, o.bdlon, o.bdlat, o.state, o.issee, o.officename,o.officeaddress,province,city,district, officepicture,o.CreateTime,o.SellerId) t 
where 1=1";
            //单据日期
            if (!queryParam["StartTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
            {
                DateTime startTime = queryParam["StartTime"].ToDate();
                DateTime endTime = queryParam["EndTime"].ToDate().AddDays(1);
                strSql += " and CreateTime >= '" + startTime + "' and CreateTime < '" + endTime + "'";
            }

            //销售人
            if (!queryParam["SellerId"].IsEmpty())
            {
                string SellerId = queryParam["SellerId"].ToString();
                strSql += " and SellerId = '" + SellerId + "'";
            }
            else
            {
                if (!OperatorProvider.Provider.Current().IsSystem)
                {
                    string dataAutor = string.Format(OperatorProvider.Provider.Current().DataAuthorize.ReadAutorize, OperatorProvider.Provider.Current().UserId);
                    strSql += " and (SellerId in (" + dataAutor + ") or SellerId is NULL)";
                }
            }

            //限制城市
            string des = OperatorProvider.Provider.Current().Description;
            if (!string.IsNullOrEmpty(des))
            {
                if (!OperatorProvider.Provider.Current().IsSystem)
                {
                    des = des.Replace("|", "','");
                    strSql += " and (province in ('" + des + "') or city in ('" + des + "') or district in ('" + des + "') and SellerId is NULL)";//导入的没有SellerId，区域限制
                }
            }

            strSql += " order by CreateTime desc";

            return this.BaseRepository().FindTable(strSql.ToString());
        }

        


        /// <summary>
        /// 全城企业分布
        /// </summary>
        /// <param name="queryJson">起始，结束日期json</param>
        /// <returns></returns>
        public DataTable GetLocationData(string queryJson)
        {
            var queryParam = queryJson.ToJObject();
            string locationSql = GetLocationSql();//限制区县，坐标表

            string strSql = @"SELECT t.*,count(1) count FROM (SELECT l.Id, bdlon, bdlat,l.RegeoId code ,l.RegeoName name 
                                FROM " + locationSql +@" l
                                LEFT JOIN Ku_Company c ON l.Id=c.LocationId where ManageState=1 AND LocationId>0 AND c.District!='' ";
            //坐标位置商圈ID存在的话，不以输入的商圈文字搜索
            if (!queryParam["LocationId"].IsEmpty())
            {
                string LocationId = queryParam["LocationId"].ToString();
                strSql += " and LocationId = " + LocationId;
            }
            else if (!queryParam["RegeoName"].IsEmpty())
            {
                string RegeoName = queryParam["RegeoName"].ToString();
                strSql += " and c.RegeoName  like '%" + RegeoName + "%' ";
            }
            //位置ID
            if (!queryParam["LocationIds"].IsEmpty())
            {
                string LocationIds = queryParam["LocationIds"].ToString();
                strSql += " and LocationId in (" + LocationIds + ")";
            }
            //单据日期
            if (!queryParam["StartTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
            {
                DateTime startTime = queryParam["StartTime"].ToDate();
                DateTime endTime = queryParam["EndTime"].ToDate().AddDays(1);
                strSql += " and BuildTime >= '" + startTime + "' and BuildTime < '" + endTime + "'";
            }
            //公司名
            if (!queryParam["CompanyName"].IsEmpty())
            {
                string CompanyName = queryParam["CompanyName"].ToString();
                strSql += " and c.CompanyName  like '%" + CompanyName + "%' ";
            }
            //经营范围
            if (!queryParam["Scope"].IsEmpty())
            {
                string Scope = queryParam["Scope"].ToString();
                strSql += " and c.Scope  like '%" + Scope + "%' ";
            }
            //行业
            if (!queryParam["Sector"].IsEmpty())
            {
                string Sector = queryParam["Sector"].ToString();
                Sector = Sector.Replace("[", "").Replace("]", "").Replace("\"", "'");
                strSql += " and c.Sector  in(" + Sector + ")";
            }
            //区域
            if (!queryParam["District"].IsEmpty())
            {
                string District = queryParam["District"].ToString();
                District = District.Replace("[", "").Replace("]", "").Replace("\"", "'");
                strSql += " and c.District in(" + District + ")";
            }
            //POI分布
            if (!queryParam["TypeName"].IsEmpty())
            {
                string TypeName = queryParam["TypeName"].ToString();
                TypeName = TypeName.Replace("[", "").Replace("]", "").Replace("\"", "'");
                strSql += " and l.TypeName in(" + TypeName + ")";
            }
            //销售人
            if (!queryParam["ObtainUserId"].IsEmpty())
            {
                string ObtainUserId = queryParam["ObtainUserId"].ToString();
                strSql += " and c.ObtainUserId = '" + ObtainUserId + "'";
            }
            //状态
            if (!queryParam["FollowState"].IsEmpty())
            {
                string FollowState = queryParam["FollowState"].ToString();
                strSql += " and c.FollowState = '" + FollowState + "'";
            }
            strSql += " )t  GROUP BY id, bdlon, bdlat,code,name ";

            return this.BaseRepository().FindTable(strSql.ToString());
        }

        /// <summary>
        /// 地理信息表
        /// </summary>
        /// <param name="queryJson">起始，结束日期json</param>
        /// <returns></returns>
        public DataTable GetMyLocationData(string queryJson)
        {
            var queryParam = queryJson.ToJObject();
            string locationSql = GetLocationSql();//限制区县，坐标表

            string strSql = @"SELECT t.*,count(1) count FROM (SELECT l.Id, bdlon, bdlat,l.RegeoId code,l.RegeoName name 
                                FROM " + locationSql + @" l
                                LEFT JOIN Ku_Company c ON l.Id=c.LocationId where ObtainState=1 AND ManageState=1 AND LocationId>0 AND c.District!='' ";
            //坐标位置商圈ID存在的话，不以输入的商圈文字搜索
            if (!queryParam["LocationId"].IsEmpty())
            {
                string LocationId = queryParam["LocationId"].ToString();
                strSql += " and LocationId = " + LocationId;
            }
            else if (!queryParam["RegeoName"].IsEmpty())
            {
                string RegeoName = queryParam["RegeoName"].ToString();
                strSql += " and c.RegeoName  like '%" + RegeoName + "%' ";
            }
            //位置ID
            if (!queryParam["LocationIds"].IsEmpty())
            {
                string LocationIds = queryParam["LocationIds"].ToString();
                strSql += " and LocationId in (" + LocationIds + ")";
            }
            //单据日期
            if (!queryParam["StartTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
            {
                DateTime startTime = queryParam["StartTime"].ToDate();
                DateTime endTime = queryParam["EndTime"].ToDate().AddDays(1);
                strSql += " and BuildTime >= '" + startTime + "' and BuildTime < '" + endTime + "'";
            }
            //商圈
            if (!queryParam["RegeoName"].IsEmpty())
            {
                string RegeoName = queryParam["RegeoName"].ToString();
                strSql += " and c.RegeoName  like '%" + RegeoName + "%' ";
            }
            //公司名
            if (!queryParam["CompanyName"].IsEmpty())
            {
                string CompanyName = queryParam["CompanyName"].ToString();
                strSql += " and c.CompanyName  like '%" + CompanyName + "%' ";
            }
            //经营范围
            if (!queryParam["Scope"].IsEmpty())
            {
                string Scope = queryParam["Scope"].ToString();
                strSql += " and c.Scope  like '%" + Scope + "%' ";
            }
            //行业
            if (!queryParam["Sector"].IsEmpty())
            {
                string Sector = queryParam["Sector"].ToString();
                Sector = Sector.Replace("[", "").Replace("]", "").Replace("\"", "'");
                strSql += " and c.Sector  in(" + Sector + ")";
            }
            //区域
            if (!queryParam["District"].IsEmpty())
            {
                string District = queryParam["District"].ToString();
                District = District.Replace("[", "").Replace("]", "").Replace("\"", "'");
                strSql += " and c.District in(" + District + ")";
            }
            //POI分布
            if (!queryParam["TypeName"].IsEmpty())
            {
                string TypeName = queryParam["TypeName"].ToString();
                TypeName = TypeName.Replace("[", "").Replace("]", "").Replace("\"", "'");
                strSql += " and l.TypeName in(" + TypeName + ")";
            }
            //销售人
            if (!queryParam["ObtainUserId"].IsEmpty())
            {
                string ObtainUserId = queryParam["ObtainUserId"].ToString();
                strSql += " and c.ObtainUserId = '" + ObtainUserId + "'";
            }
            else
            {
                if (!OperatorProvider.Provider.Current().IsSystem)
                {
                    string dataAutor = string.Format(OperatorProvider.Provider.Current().DataAuthorize.ReadAutorize, OperatorProvider.Provider.Current().UserId);
                    strSql += " and (c.ObtainUserId in (" + dataAutor + "))";
                }
            }
            //状态
            if (!queryParam["FollowState"].IsEmpty())
            {
                string FollowState = queryParam["FollowState"].ToString();
                strSql += " and c.FollowState = '" + FollowState + "'";
            }
            strSql += " )t  GROUP BY id, bdlon, bdlat,code,name";
            return this.BaseRepository().FindTable(strSql.ToString());
        }

        /// <summary>
        /// Map
        /// </summary>
        /// <param name="queryJson">起始，结束日期json</param>
        /// <returns></returns>
        public DataTable GetCustomerData(string queryJson)
        {
            var queryParam = queryJson.ToJObject();
            string strSql = @" SELECT * FROM (
SELECT Id, shopname name,CreateTime,SellerId FROM POS_Shop 
UNION ALL
SELECT Id, companyname name,CreateTime,SellerId FROM POS_OfficeCompany) t where 1=1 ";
            //单据日期
            if (!queryParam["StartTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
            {
                DateTime startTime = queryParam["StartTime"].ToDate();
                DateTime endTime = queryParam["EndTime"].ToDate().AddDays(1);
                strSql += " and CreateTime >= '" + startTime + "' and CreateTime < '" + endTime + "'";
            }

            //销售人
            if (!queryParam["SellerId"].IsEmpty())
            {
                string SellerId = queryParam["SellerId"].ToString();
                strSql += " and SellerId = '" + SellerId + "'";
            }
            else
            {
                if (!OperatorProvider.Provider.Current().IsSystem)
                {
                    string dataAutor = string.Format(OperatorProvider.Provider.Current().DataAuthorize.ReadAutorize, OperatorProvider.Provider.Current().UserId);
                    strSql += " and SellerId in (" + dataAutor + ")";
                }
            }
            strSql += " order by CreateTime desc";

            return this.BaseRepository().FindTable(strSql.ToString());
        }


        /// <summary>
        /// 客户销售表
        /// </summary>
        /// <param name="queryJson">起始，结束日期json</param>
        /// <returns></returns>
        public DataTable GetSaleCustomerData(string queryJson)
        {
            var queryParam = queryJson.ToJObject();
            string strSql = "SELECT CustomerCompany,SumTotalAmount,SumTotalCount,CASE WHEN SumYuCount IS NULL  THEN SumTotalCount ELSE SumYuCount END SumYuCount,CASE WHEN SumSaleCount IS NULL  THEN 0 ELSE SumSaleCount END SumSaleCount FROM Sale_Customer where 1=1 ";
            //单据日期
            if (!queryParam["StartTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
            {
                DateTime startTime = queryParam["StartTime"].ToDate();
                DateTime endTime = queryParam["EndTime"].ToDate().AddDays(1);
                strSql += " and CreateDate >= '" + startTime + "' and CreateDate < '" + endTime + "'";
            }
            if (!OperatorProvider.Provider.Current().IsSystem)
            {
                string dataAutor = string.Format(OperatorProvider.Provider.Current().DataAuthorize.ReadAutorize, OperatorProvider.Provider.Current().UserId);
                dataAutor = dataAutor.Replace("select UserId from", "select RealName from");
                strSql += " and CreateUserName in (" + dataAutor + ")";
            }
            strSql += "order by SumTotalAmount desc";

            return this.BaseRepository().FindTable(strSql.ToString());
        }

        /// <summary>
        /// 员工销售月报
        /// </summary>
        /// <param name="queryJson">起始，结束日期json</param>
        /// <returns></returns>
        public DataTable GetMonthEmpData(string queryJson)
        {
            var queryParam = queryJson.ToJObject();
            string strSql = @"SELECT SUM(TotalAmount) AS salemoney, count(1) AS salecount,right(100+day(CreateDate),2) as orderdate 
                FROM Sales_Contract sales where 1=1 ";
            //单据日期
            if (!queryParam["syear"].IsEmpty())
            {
                string syear = queryParam["syear"].ToString();
                strSql += " and year(CreateDate)=" + syear;
            }
            //单据日期
            if (!queryParam["smonth"].IsEmpty() )
            {
                string smonth = queryParam["smonth"].ToString();
                strSql += " and month(CreateDate)=" + smonth;
            }
            //销售人
            if (!queryParam["UserId"].IsEmpty())
            {
                string UserId = queryParam["UserId"].ToString();
                strSql += " and UserId = '" + UserId + "'";
            }
            else
            {
                if (!OperatorProvider.Provider.Current().IsSystem)
                {
                    string dataAutor = string.Format(OperatorProvider.Provider.Current().DataAuthorize.ReadAutorize, OperatorProvider.Provider.Current().UserId);
                    strSql += " and UserId in (" + dataAutor + ")";
                }
            }
            strSql += " group by right(100+day(CreateDate),2) order by orderdate";

            return this.BaseRepository().FindTable(strSql.ToString());
        }

        public string GetLocationSql()
        {
            string locationSql = "";
            string des = OperatorProvider.Provider.Current().Description;
            if (!string.IsNullOrEmpty(des))
            {
                Regex r = new Regex(@"[\u4e00-\u9fa5]+");//包括中文
                if (!r.IsMatch(des))
                {
                    //半径圈
                    if (des.IndexOf('|') > 0)
                    {
                        string[] locations = des.Split('|');
                        for (int i = 0; i < locations.Length; i++)
                        {
                            locationSql += "SELECT * FROM Ku_Location where dbo.f_GetDistance(" + locations[i] + @",bdlon,bdlat)<=1000 UNION ";//多个区县用|分隔
                        }
                        locationSql = "(" + locationSql.Substring(0, locationSql.Length - 6) + ")";
                    }
                    else
                    {
                        locationSql = "(SELECT * FROM Ku_Location where dbo.f_GetDistance(" + des + @",bdlon,bdlat)<=1000) ";//导入的没有SellerId，区域限制
                    }
                }
                else
                {
                    //区县
                    if (des.IndexOf('|') > 0)
                    {
                        des = des.Replace("|", "','");
                        locationSql = "(SELECT * FROM Ku_Location where district in (" + des + "))";//多个区县用|分隔
                    }
                    else
                    {
                        locationSql = "(SELECT * FROM Ku_Location where district ='" + des + "')";//导入的没有SellerId，区域限制
                    }
                }
            }
            else
            {
                locationSql = "Ku_Location";
            }
            return locationSql;
        }
        /// <summary>
        /// 本周区县分布
        /// </summary>
        /// <returns></returns>
        public DataTable GetWeekAreaData(string start, string end)
        {
            string locationSql = GetLocationSql();
            //限制区县，坐标表
            string strSql = @"SELECT c.District,count(1) count FROM " + locationSql + @" l LEFT JOIN Ku_Company c ON l.Id=c.LocationId  
                              WHERE ManageState=1 AND LocationId>0 AND c.District!=''";
            
            if (!start.IsEmpty() && !end.IsEmpty())
            {
                DateTime endTime = end.ToDate().AddDays(1);
                strSql += " and BuildTime>='" + start + "' AND BuildTime <'" + endTime + "'";
            }
            strSql += " GROUP BY c.District ORDER BY count desc ";
            return this.BaseRepository().FindTable(strSql.ToString());
        }
        /// <summary>
        /// 本周更新行业分布
        /// </summary>
        /// <returns></returns>
        public DataTable GetWeekSectorData(string start, string end)
        {
            string locationSql = GetLocationSql();
            //限制区县，坐标表
            string strSql = @"SELECT sector,count(1) count FROM " + locationSql + @" l LEFT JOIN Ku_Company c ON l.Id=c.LocationId  
                              WHERE ManageState=1 AND LocationId>0 AND c.District!=''";
            if (!start.IsEmpty() && !end.IsEmpty())
            {
                DateTime endTime = end.ToDate().AddDays(1);
                strSql += " and BuildTime>='" + start + "' AND BuildTime <'" + endTime + "'";
            }
            strSql += " GROUP BY sector ORDER BY count desc ";
            return this.BaseRepository().FindTable(strSql.ToString());
        }

        /// <summary>
        /// 坐标POI类型柱状图
        /// </summary>
        /// <returns></returns>
        public DataTable GetPoiTypeData(string start, string end)
        {
            string locationSql = GetLocationSql();
            //限制区县，坐标表
            string strSql = @"SELECT top 50 TypeCode,TypeName,count(1) count FROM " + locationSql + @" l LEFT JOIN Ku_Company c ON l.Id=c.LocationId  
                              WHERE ManageState=1 AND LocationId>0 AND c.District!='' and TypeCode IS NOT NULL";
            
            if (!start.IsEmpty() && !end.IsEmpty())
            {
                DateTime endTime = end.ToDate().AddDays(1);
                strSql += " and BuildTime>='" + start + "' AND BuildTime <'" + endTime + "'";
            }
            strSql += " GROUP BY TypeCode,TypeName ORDER BY count desc ";
            return this.BaseRepository().FindTable(strSql.ToString());
        }

        /// <summary>
        /// 2018年各个月份成立公司曲线
        /// </summary>
        /// <returns></returns>
        public DataTable GetCompanyYearData()
        {
            int year = DateTime.Now.Year;
            int nextyear = year + 1;
            string locationSql = GetLocationSql();
            //限制区县，坐标表
            string strSql = @"SELECT right(100+month(BuildTime),2) as month,count(1) count FROM " + locationSql + @" l LEFT JOIN Ku_Company c ON l.Id=c.LocationId  
                              WHERE ManageState=1 AND LocationId>0 AND c.District!='' AND BuildTime>='" + year + @"/01/01 00:00:00' AND BuildTime <'" + nextyear + @"/01/01 00:00:00' 
GROUP BY right(100+month(BuildTime),2) ORDER BY right(100+month(BuildTime),2)  ";

            return this.BaseRepository().FindTable(strSql.ToString());
        }
        #endregion

        #region 月报指标
        /// <summary>
        /// 员工查看公司次数月报
        /// </summary>
        /// <param name="queryJson">起始，结束日期json</param>
        /// <returns></returns>
        public DataTable GetMonthSeeData(string queryJson)
        {
            var queryParam = queryJson.ToJObject();
            string strSql = @"SELECT count(1) AS seecount,right(100+day(SeeDate),2) as orderdate  FROM Ku_CompanySee where 1=1 ";
            //单据日期
            if (!queryParam["syear"].IsEmpty())
            {
                string syear = queryParam["syear"].ToString();
                strSql += " and year(SeeDate)=" + syear;
            }
            //单据日期
            if (!queryParam["smonth"].IsEmpty())
            {
                string smonth = queryParam["smonth"].ToString();
                strSql += " and month(SeeDate)=" + smonth;
            }
            //销售人
            if (!queryParam["UserId"].IsEmpty())
            {
                string UserId = queryParam["UserId"].ToString();
                strSql += " and SeeUserId = '" + UserId + "'";
            }
            else
            {
                if (!OperatorProvider.Provider.Current().IsSystem)
                {
                    string dataAutor = string.Format(OperatorProvider.Provider.Current().DataAuthorize.ReadAutorize, OperatorProvider.Provider.Current().UserId);
                    strSql += " and SeeUserId in (" + dataAutor + ")";
                }
            }
            strSql += " group by right(100+day(SeeDate),2) order by orderdate";

            return this.BaseRepository().FindTable(strSql.ToString());
        }

        /// <summary>
        /// 员工获取公司次数月报
        /// </summary>
        /// <param name="queryJson">起始，结束日期json</param>
        /// <returns></returns>
        public DataTable GetMonthObtainData(string queryJson)
        {
            var queryParam = queryJson.ToJObject();
            string strSql = @"SELECT count(1) AS obtaincount,right(100+day(ObtainDate),2) as orderdate FROM Ku_Company where ManageState=1 and ObtainState=1 ";
            //单据日期
            if (!queryParam["syear"].IsEmpty())
            {
                string syear = queryParam["syear"].ToString();
                strSql += " and year(ObtainDate)=" + syear;
            }
            //单据日期
            if (!queryParam["smonth"].IsEmpty())
            {
                string smonth = queryParam["smonth"].ToString();
                strSql += " and month(ObtainDate)=" + smonth;
            }
            //销售人
            if (!queryParam["UserId"].IsEmpty())
            {
                string UserId = queryParam["UserId"].ToString();
                strSql += " and ObtainUserId = '" + UserId + "'";
            }
            else
            {
                if (!OperatorProvider.Provider.Current().IsSystem)
                {
                    string dataAutor = string.Format(OperatorProvider.Provider.Current().DataAuthorize.ReadAutorize, OperatorProvider.Provider.Current().UserId);
                    strSql += " and ObtainUserId in (" + dataAutor + ")";
                }
            }
            strSql += " group by right(100+day(ObtainDate),2) order by orderdate";

            return this.BaseRepository().FindTable(strSql.ToString());
        }
        /// <summary>
        /// 员工跟进记录月报
        /// </summary>
        /// <param name="queryJson">起始，结束日期json</param>
        /// <returns></returns>
        public DataTable GetMonthRecordData(string queryJson)
        {
            var queryParam = queryJson.ToJObject();
            //string strSql = @"SELECT count(1) AS recordcount,right(100+day(CreateDate),2) as orderdate FROM Client_TrailRecord where 1=1 ";
            string strSql = @"select  count(1) AS recordcount,right(100+day(c.ModifyDate),2) as orderdate from Ku_Company c LEFT JOIN Ku_Location l ON l.Id=c.LocationId 
                                where ObtainState=1 and ManageState=1 AND LocationId>0 AND c.District!='' ";
            //单据日期
            if (!queryParam["syear"].IsEmpty())
            {
                string syear = queryParam["syear"].ToString();
                strSql += " and year(c.ModifyDate)=" + syear;
            }
            //单据日期
            if (!queryParam["smonth"].IsEmpty())
            {
                string smonth = queryParam["smonth"].ToString();
                strSql += " and month(c.ModifyDate)=" + smonth;
            }
            //销售人
            if (!queryParam["UserId"].IsEmpty())
            {
                string UserId = queryParam["UserId"].ToString();
                strSql += " and c.CreateUserId = '" + UserId + "'";
            }
            else
            {
                if (!OperatorProvider.Provider.Current().IsSystem)
                {
                    string dataAutor = string.Format(OperatorProvider.Provider.Current().DataAuthorize.ReadAutorize, OperatorProvider.Provider.Current().UserId);
                    strSql += " and c.CreateUserId in (" + dataAutor + ")";
                }
            }
            strSql += " group by right(100+day(c.ModifyDate),2) order by orderdate";

            return this.BaseRepository().FindTable(strSql.ToString());
        }

        /// <summary>
        /// 员工合作状态          月报
        /// </summary>
        /// <param name="queryJson">起始，结束日期json</param>
        /// <returns></returns>
        public DataTable GetMonthFollowStateData(string queryJson)
        {
            var queryParam = queryJson.ToJObject();
            string strSql = @"SELECT FollowState, CASE FollowState WHEN 1 THEN '已拒绝' WHEN 2 THEN '有意向' WHEN 3 THEN '已合作' WHEN 4 THEN '待跟进' WHEN 5 THEN '非意向' END FollowStateName ,
count(1) count FROM Ku_Company WHERE ManageState=1 and FollowState <>0 ";

            //单据日期
            if (!queryParam["syear"].IsEmpty())
            {
                string syear = queryParam["syear"].ToString();
                strSql += " and year(ModifyDate)=" + syear;
            }
            //单据日期
            if (!queryParam["smonth"].IsEmpty())
            {
                string smonth = queryParam["smonth"].ToString();
                strSql += " and month(ModifyDate)=" + smonth;
            }
            //销售人
            if (!queryParam["UserId"].IsEmpty())
            {
                string UserId = queryParam["UserId"].ToString();
                strSql += " and ObtainUserId = '" + UserId + "'";
            }
            else
            {
                if (!OperatorProvider.Provider.Current().IsSystem)
                {
                    string dataAutor = string.Format(OperatorProvider.Provider.Current().DataAuthorize.ReadAutorize, OperatorProvider.Provider.Current().UserId);
                    strSql += " and ObtainUserId in (" + dataAutor + ")";
                }
            }
            strSql += " group by FollowState order by count";

            return this.BaseRepository().FindTable(strSql.ToString());
        }
        #endregion

        #region 员工详情

        /// <summary>
        /// 员工获取条数
        /// </summary>
        /// <param name="queryJson">起始，结束日期json</param>
        /// <returns></returns>
        public DataTable GetEmpObtainData(string queryJson)
        {
            var queryParam = queryJson.ToJObject();
            string strSql = "SELECT ObtainUserName,count(1) ObtainCount FROM Ku_Company where ManageState=1 and ObtainState=1 ";
            //单据日期
            if (!queryParam["StartTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
            {
                DateTime startTime = queryParam["StartTime"].ToDate();
                DateTime endTime = queryParam["EndTime"].ToDate().AddDays(1);
                strSql += " and ObtainDate >= '" + startTime + "' and ObtainDate < '" + endTime + "'";
            }
            if (!OperatorProvider.Provider.Current().IsSystem)
            {
                string dataAutor = string.Format(OperatorProvider.Provider.Current().DataAuthorize.ReadAutorize, OperatorProvider.Provider.Current().UserId);
                strSql += " and ObtainUserId in (" + dataAutor + ")";
            }
            strSql += " group by ObtainUserName order by ObtainCount desc";

            return this.BaseRepository().FindTable(strSql.ToString());
        }
        /// <summary>
        /// 员工更近条数
        /// </summary>
        /// <param name="queryJson">起始，结束日期json</param>
        /// <returns></returns>
        public DataTable GetEmpRecordData(string queryJson)
        {
            var queryParam = queryJson.ToJObject();
            //string strSql = "SELECT CreateUserName,count(1) RecordCount FROM Client_TrailRecord where 1=1 ";
            string strSql = @"select  count(1) AS RecordCount,c.ModifyUserName from Ku_Company c LEFT JOIN Ku_Location l ON l.Id=c.LocationId where ObtainState=1 and ManageState=1 AND LocationId>0 AND c.District!='' ";

            //单据日期
            if (!queryParam["StartTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
            {
                DateTime startTime = queryParam["StartTime"].ToDate();
                DateTime endTime = queryParam["EndTime"].ToDate().AddDays(1);
                strSql += " and c.ModifyDate >= '" + startTime + "' and c.ModifyDate < '" + endTime + "'";
            }
            if (!OperatorProvider.Provider.Current().IsSystem)
            {
                string dataAutor = string.Format(OperatorProvider.Provider.Current().DataAuthorize.ReadAutorize, OperatorProvider.Provider.Current().UserId);
                strSql += " and c.ModifyUserId in (" + dataAutor + ")";
            }
            strSql += " group by c.ModifyUserName order by RecordCount  desc";

            return this.BaseRepository().FindTable(strSql.ToString());
        }
        /// <summary>
        /// 员工查看条数
        /// </summary>
        /// <param name="queryJson">起始，结束日期json</param>
        /// <returns></returns>
        public DataTable GetEmpSeeData(string queryJson)
        {
            var queryParam = queryJson.ToJObject();
            string strSql = "SELECT SeeUserName,count(1) SeeCount FROM Ku_CompanySee where 1=1 ";
            //单据日期
            if (!queryParam["StartTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
            {
                DateTime startTime = queryParam["StartTime"].ToDate();
                DateTime endTime = queryParam["EndTime"].ToDate().AddDays(1);
                strSql += " and SeeDate >= '" + startTime + "' and SeeDate < '" + endTime + "'";
            }
            if (!OperatorProvider.Provider.Current().IsSystem)
            {
                string dataAutor = string.Format(OperatorProvider.Provider.Current().DataAuthorize.ReadAutorize, OperatorProvider.Provider.Current().UserId);
                strSql += " and SeeUserId in (" + dataAutor + ")";
            }
            strSql += " group by SeeUserName order by SeeCount desc";

            return this.BaseRepository().FindTable(strSql.ToString());
        }
        #endregion
    }
}
