using HZSoft.Application.Code;
using HZSoft.Application.Entity.CustomerManage;
using HZSoft.Application.Entity.WeChatManage;
using HZSoft.Application.IService.CustomerManage;
using HZSoft.Application.IService.WeChatManage;
using HZSoft.Data.Repository;
using HZSoft.Util;
using HZSoft.Util.Extension;
using HZSoft.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HZSoft.Application.Service.CustomerManage
{
    /// <summary>
    /// 版 本 6.1
    /// 
    /// 创 建：超级管理员
    /// 日 期：2017-12-12 10:37
    /// 描 述：拓展目标
    /// </summary>
    public class POS_OfficeCompanyService : RepositoryFactory<POS_OfficeCompanyEntity>, POS_OfficeCompanyIService
    {
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<POS_OfficeCompanyEntity> GetPageList(Pagination pagination, string queryJson)
        {

            var expression = LinqExtensions.True<POS_OfficeCompanyEntity>();
            var queryParam = queryJson.ToJObject();

            //string strSql = "select * from POS_OfficeCompany where 1=1";
            string strSql = "SELECT company.id,companyname,companypicture,officeid,company.officename,Contacts,Floor,Room,Telphone,company.IsSee,company.State,company.CreateTime,company.NickName,company.SellerName"
                     + " FROM POS_OfficeCompany company "
                     + " LEFT JOIN POS_Office office  ON office.Id = company.OfficeId  where 1=1";

            //单据日期
            if (!queryParam["StartTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
            {
                DateTime startTime = queryParam["StartTime"].ToDate();
                DateTime endTime = queryParam["EndTime"].ToDate().AddDays(1);
                strSql += " and CreateTime >= '" + startTime + "' and CreateTime < '" + endTime + "'";
            }
            //写字楼ID
            if (!queryParam["OfficeId"].IsEmpty())
            {
                string OfficeId = queryParam["OfficeId"].ToString();
                strSql += " and OfficeId = '" + OfficeId + "'";
            }
            //写字楼名
            if (!queryParam["OfficeName"].IsEmpty())
            {
                string OfficeName = queryParam["OfficeName"].ToString();
                strSql += " and OfficeName = '" + OfficeName + "'";
            }
            //公司名
            if (!queryParam["CompanyName"].IsEmpty())
            {
                string CompanyName = queryParam["CompanyName"].ToString();
                strSql += " and CompanyName = '" + CompanyName + "'";
            }
            //手机号
            if (!queryParam["Telphone"].IsEmpty())
            {
                string Telphone = queryParam["Telphone"].ToString();
                strSql += " and Telphone = '" + Telphone + "'";
            }
            //联系人
            if (!queryParam["Contacts"].IsEmpty())
            {
                string Contacts = queryParam["Contacts"].ToString();
                strSql += " and Contacts = '" + Contacts + "'";
            }
            //状态
            if (!queryParam["State"].IsEmpty())
            {
                string State = queryParam["State"].ToString();
                strSql += " and State = '" + State + "'";
            }

            //销售人
            if (!queryParam["SellerId"].IsEmpty())
            {
                string SellerId = queryParam["SellerId"].ToString();
                strSql += " and company.SellerId = '" + SellerId + "'";
            }
            else
            {
                if (!OperatorProvider.Provider.Current().IsSystem)
                {
                    //限制角色权限
                    string strSqlSeller = "";
                    string dataAutor = string.Format(OperatorProvider.Provider.Current().DataAuthorize.ReadAutorize, OperatorProvider.Provider.Current().UserId);
                    strSqlSeller = " company.SellerId in (" + dataAutor + ") ";

                    //限制城市权限
                    string des = OperatorProvider.Provider.Current().Description;
                    string strSqlArea = "";
                    if (!string.IsNullOrEmpty(des))
                    {
                        des = des.Replace("|", "','");
                        strSqlArea = " or (province in ('" + des + "') or city in ('" + des + "') or district in ('" + des + "')  and company.SellerId is null) ";
                    }
                    else
                    {
                        strSqlArea = " or company.SellerId is null ";
                    }
                    //除了角色范围内的，还包括or城市权限范围内的（上传人不为空的，被别人编辑之后的就不包含在内）
                    strSql += " and (" + strSqlSeller + strSqlArea + ")";
                }
            }


            return this.BaseRepository().FindList(strSql.ToString(), pagination);
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<POS_OfficeCompanyEntity> GetList(string queryJson)
        {
            var expression = LinqExtensions.True<POS_OfficeEntity>();
            var queryParam = queryJson.ToJObject();
            string wxLon = "";
            string wxLat = "";
            string strSql = "";
            string strOrder = "";
            if (!queryParam["wxLon"].IsEmpty() && !queryParam["wxLat"].IsEmpty())
            {
                wxLon = queryParam["wxLon"].ToString();
                wxLat = queryParam["wxLat"].ToString();

                strSql = "SELECT TOP 50 company.id,companyname,companypicture,officeid,company.officename,Contacts,Floor,Room,Telphone,dbo.f_GetDistance(" + wxLon + "," + wxLat
                         + ",wxlon,wxlat) as Description FROM POS_OfficeCompany company"
                         + " LEFT JOIN POS_Office office  ON office.Id = company.OfficeId  where 1=1";
                strOrder = " order by dbo.f_GetDistance(" + wxLon + "," + wxLat + ",wxlon,wxlat) asc,convert(int,floor) desc";
            }
            else
            {
                strSql = "select * from POS_OfficeCompany where 1 = 1 ";
                strOrder = " ORDER BY CreateTime desc";
            }
            //公司名
            if (!queryParam["SearchName"].IsEmpty())
            {
                string CompanyName = queryParam["SearchName"].ToString();
                strSql += " and CompanyName = '" + CompanyName + "'";
            }
            //写字楼ID
            if (!queryParam["OfficeId"].IsEmpty())
            {
                string OfficeId = queryParam["OfficeId"].ToString();
                strSql += " and OfficeId = '" + OfficeId + "'";
            }
            
            if (!OperatorProvider.Provider.Current().IsSystem)
            {
                //限制角色权限
                string strSqlSeller = "";
                string dataAutor = string.Format(OperatorProvider.Provider.Current().DataAuthorize.ReadAutorize, OperatorProvider.Provider.Current().UserId);
                strSqlSeller = " company.SellerId in (" + dataAutor + ") ";

                //限制城市权限
                string des = OperatorProvider.Provider.Current().Description;
                string strSqlArea = "";
                if (!string.IsNullOrEmpty(des))
                {
                    des = des.Replace("|", "','");
                    strSqlArea = " or (province in ('" + des + "') or city in ('" + des + "') or district in ('" + des + "')  and company.SellerId is null) ";
                }
                else
                {
                    strSqlArea = " or company.SellerId is null ";
                }
                //除了角色范围内的，还包括or城市权限范围内的（上传人不为空的，被别人编辑之后的就不包含在内）
                strSql += " and (" + strSqlSeller + strSqlArea + ")";
            }

            strSql += strOrder;

            return this.BaseRepository().FindList(strSql.ToString());
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public POS_OfficeCompanyEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void RemoveForm(string keyValue)
        {
            this.BaseRepository().Delete(keyValue);
        }
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, POS_OfficeCompanyEntity entity)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                entity.Modify(keyValue);
                this.BaseRepository().Update(entity);
            }
            else
            {
                entity.Create();
                this.BaseRepository().Insert(entity);
            }
        }
        #endregion
    }
}
