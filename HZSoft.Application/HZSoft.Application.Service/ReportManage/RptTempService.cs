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

namespace HZSoft.Application.Service.ReportManage
{
    /// <summary>
    /// 版 本 6.1
    /// 
    /// 创建人：佘赐雄
    /// 日 期：2016.1.14 14:27
    /// 描 述：报表模板管理
    /// </summary>
    public class RptTempService : RepositoryFactory<RptTempEntity>, IRptTempService
    {
        #region 获取数据
        /// <summary>
        /// 报表模板列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<RptTempEntity> GetList(string queryJson)
        {
            var strSql = new StringBuilder();
            strSql.Append(@"SELECT  r.TempId,
                                    r.EnCode ,
                                    r.FullName ,
                                    CASE r.TempType
                                      WHEN 'line' THEN '折线图'
                                      WHEN 'bar' THEN '柱形图'
                                      WHEN 'map' THEN '地图'
                                      WHEN 'pie' THEN '饼图'
                                    END AS TempType ,
                                    r.TempCategory ,
                                    r.Description ,
                                    r.CreateDate
                            FROM    Rpt_Temp r
                            WHERE   1 = 1 ");
            var parameter = new List<DbParameter>();
            var queryParam = queryJson.ToJObject();
            //查询条件
            if (!queryParam["condition"].IsEmpty() && !queryParam["keyword"].IsEmpty())
            {
                string condition = queryParam["condition"].ToString();
                string keyword = queryParam["keyword"].ToString();
                switch (condition)
                {
                    case "EnCode":            //角色编号
                        strSql.Append(" AND r.EnCode LIKE @keyword ");
                        parameter.Add(DbParameters.CreateDbParameter("@keyword", '%' + keyword + '%'));
                        break;
                    case "FullName":          //角色名称
                        strSql.Append(" AND r.FullName LIKE @keyword ");
                        parameter.Add(DbParameters.CreateDbParameter("@keyword", '%' + keyword + '%'));
                        break;
                    default:
                        break;
                }
            }
            if (!queryParam["reportCode"].IsEmpty())
            {
                strSql.Append(" AND r.TempCategory = @TempCategory ");
                parameter.Add(DbParameters.CreateDbParameter("@TempCategory", queryParam["reportCode"].ToString()));
            }
            return this.BaseRepository().FindList(strSql.ToString(), parameter.ToArray());
        }
        /// <summary>
        /// 报表模板实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public RptTempEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }
        /// <summary>
        /// 获得报表数据
        /// </summary>
        /// <param name="reportId">主键值</param>
        /// <returns></returns>
        public string GetReportData(string reportId)
        {
            RptTempEntity rpttempentity = this.BaseRepository().FindEntity(reportId);
            if (rpttempentity.ParamJson != null)
            {
                dynamic paramJson = rpttempentity.ParamJson.ToJson();
                string strSql = paramJson.sqlString;
                string strListSql = paramJson.listSqlString;
                string picTitle = paramJson.title;
                string title = rpttempentity.FullName;
                string tempType = rpttempentity.TempType;
                List<FieldList> listField = new List<FieldList>();
                DataTable picData = new DataTable();
                if (!string.IsNullOrEmpty(strSql))
                {
                    picData = this.BaseRepository().FindTable(strSql);
                }
                DataTable listData = new DataTable();
                if (!string.IsNullOrEmpty(strListSql))
                {
                    listData = this.BaseRepository().FindTable(strListSql);
                    if (listData.Columns.Count > 0)
                    {
                        for (int i = 0; i < listData.Columns.Count; i++)
                        {
                            listField.Add(new FieldList() { Field = listData.Columns[i].ColumnName });
                        }
                    }
                }
                var jsonData = new
                {
                    title = title,
                    tempType = tempType,
                    listField = listField,
                    picTitle = picTitle,
                    picData = picData,
                    listData = listData
                };
                return jsonData.ToJson();
            }
            return null;
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除报表模板
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void RemoveForm(string keyValue)
        {
            this.BaseRepository().Delete(keyValue);
        }
        /// <summary>
        /// 保存报表模板表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="rptTempEntity">报表实体</param>
        /// <param name="moduleEntity">模块实体</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, RptTempEntity rptTempEntity, ModuleEntity moduleEntity)
        {
            IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();
            try
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    rptTempEntity.Modify(keyValue);
                    db.Update(rptTempEntity);
                }
                else
                {
                    rptTempEntity.Create();
                    db.Insert(rptTempEntity);
                    moduleEntity.UrlAddress = "/ReportManage/Report/ReportPreview?keyValue=" + rptTempEntity.TempId;
                    db.Insert(moduleEntity);
                }
                db.Commit();
            }
            catch (Exception)
            {
                db.Rollback();
                throw;
            }
        }
        #endregion

        #region 半径区县地图数据
        /// <summary>
        /// 半径1000米
        /// </summary>
        public DataTable GetRadiusData(string queryJson)
        {
            var queryParam = queryJson.ToJObject();
            //限坐标表
            string des = OperatorProvider.Provider.Current().Description;
            string locationSql = "";
            if (!string.IsNullOrEmpty(des))
            {
                if (des.IndexOf('|') > 0)
                {
                    string[] locations = des.Split('|');
                    for (int i = 0; i < locations.Length; i++)
                    {
                        locationSql += "SELECT * FROM Ku_Location where dbo.f_GetDistance(" + locations[i] + @",bdlon,bdlat)<=1000 UNION ";//多个区县用|分隔
                    }
                    locationSql = locationSql.Substring(0, locationSql.Length - 6);
                }
                else
                {
                    locationSql = "SELECT * FROM Ku_Location where dbo.f_GetDistance(" + des + @",bdlon,bdlat)<=1000 ";//导入的没有SellerId，区域限制
                }
            }

            string strSql = @"SELECT t.*,count(1) count FROM (SELECT l.Id, bdlon, bdlat,l.RegeoName name 
                                FROM ("+ locationSql + @") l
                                LEFT JOIN Ku_Company c ON l.Id=c.LocationId where 1=1  AND ManageState=1 AND LocationId>0 AND c.District!='' ";
            //行业
            if (!queryParam["Sector"].IsEmpty())
            {
                string Sector = queryParam["Sector"].ToString();
                strSql += " and Sector ='" + Sector + "'";
            }
            //区域
            if (!queryParam["District"].IsEmpty())
            {
                string District = queryParam["District"].ToString();
                strSql += " and District ='" + District + "'";
            }
            //POI分布
            if (!queryParam["TypeName"].IsEmpty())
            {
                string TypeName = queryParam["TypeName"].ToString();
                strSql += " and TypeName ='" + TypeName + "'";
            }
            //关键字
            if (!queryParam["condition"].IsEmpty() && !queryParam["keyword"].IsEmpty())
            {
                string condition = queryParam["condition"].ToString();
                string keyword = queryParam["keyword"].ToString();
                strSql += " and " + condition + " like '%" + keyword + "%' ";
            }
            //单据日期
            if (!queryParam["StartTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
            {
                DateTime startTime = queryParam["StartTime"].ToDate();
                DateTime endTime = queryParam["EndTime"].ToDate().AddDays(1);
                strSql += " and BuildTime >= '" + startTime + "' and BuildTime < '" + endTime + "'";
            }
            strSql += " )t  GROUP BY id, bdlon, bdlat,name ";

            return this.BaseRepository().FindTable(strSql.ToString());
        }
        /// <summary>
        /// 2017年之后区县数据
        /// </summary>
        public DataTable GetAreaData(string queryJson)
        {
            var queryParam = queryJson.ToJObject();

            string locationSql = "Ku_Location";
            //限制区县，坐标表
            string des = OperatorProvider.Provider.Current().Description;
            if (!string.IsNullOrEmpty(des))
            {
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

            string strSql = @"SELECT t.*,count(1) count FROM (SELECT l.Id, bdlon, bdlat,l.RegeoName name 
                                FROM " + locationSql + @" l
                                LEFT JOIN Ku_Company c ON l.Id=c.LocationId where 1=1  AND ManageState=1 AND LocationId>0 AND c.District!='' ";
            //行业
            if (!queryParam["Sector"].IsEmpty())
            {
                string Sector = queryParam["Sector"].ToString();
                strSql += " and Sector ='" + Sector + "'";
            }
            //区域
            if (!queryParam["District"].IsEmpty())
            {
                string District = queryParam["District"].ToString();
                strSql += " and District ='" + District + "'";
            }
            //POI分布
            if (!queryParam["TypeName"].IsEmpty())
            {
                string TypeName = queryParam["TypeName"].ToString();
                strSql += " and TypeName ='" + TypeName + "'";
            }
            //关键字
            if (!queryParam["condition"].IsEmpty() && !queryParam["keyword"].IsEmpty())
            {
                string condition = queryParam["condition"].ToString();
                string keyword = queryParam["keyword"].ToString();
                strSql += " and " + condition + " like '%" + keyword + "%' ";
            }
            //单据日期
            if (!queryParam["StartTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
            {
                DateTime startTime = queryParam["StartTime"].ToDate();
                DateTime endTime = queryParam["EndTime"].ToDate().AddDays(1);
                strSql += " and BuildTime >= '" + startTime + "' and BuildTime < '" + endTime + "'";
            }
            else
            {
                DateTime endTime = DateTime.Now;
                strSql += " and BuildTime >= '2017-01-01' and BuildTime < '" + endTime + "'";
            }
            strSql += " )t  GROUP BY id, bdlon, bdlat,name ";

            return this.BaseRepository().FindTable(strSql.ToString());
        }

        #endregion
    }
    class FieldList
    {
        public string Field { get; set; }
    }
}
