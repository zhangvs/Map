using HZSoft.Application.Code;
using HZSoft.Application.Entity.CustomerManage;
using HZSoft.Application.IService.CustomerManage;
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
    /// 日 期：2018-06-27 14:31
    /// 描 述：公司浏览记录
    /// </summary>
    public class Ku_CompanySeeService : RepositoryFactory<Ku_CompanySeeEntity>, Ku_CompanySeeIService
    {
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<Ku_CompanySeeEntity> GetPageList(Pagination pagination, string queryJson)
        {
            var expression = LinqExtensions.True<Ku_CompanySeeEntity>();
            var queryParam = queryJson.ToJObject();
            string strSql = $"select * from Ku_CompanySee where 1=1 ";
            //单据日期
            if (!queryParam["StartTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
            {
                DateTime startTime = queryParam["StartTime"].ToDate();
                DateTime endTime = queryParam["EndTime"].ToDate().AddDays(1);
                strSql += " and SeeDate >= '" + startTime + "' and SeeDate < '" + endTime + "'";
            }
            //公司名
            if (!queryParam["CompanyName"].IsEmpty())
            {
                string CompanyName = queryParam["CompanyName"].ToString();
                strSql += " and CompanyName  like '%" + CompanyName + "%' ";
            }
            if (!OperatorProvider.Provider.Current().IsSystem)
            {
                string dataAutor = string.Format(OperatorProvider.Provider.Current().DataAuthorize.ReadAutorize, OperatorProvider.Provider.Current().UserId);
                strSql += " and (SeeUserId in (" + dataAutor + "))";
            }

            //指标日期一天内的获取详情
            if (!queryParam["SeeDate"].IsEmpty())
            {
                DateTime startTime = queryParam["SeeDate"].ToDate();
                DateTime endTime = queryParam["SeeDate"].ToDate().AddDays(1);
                strSql += " and SeeDate >= '" + startTime + "' and SeeDate < '" + endTime + "'";
            }

            //公司名
            if (!queryParam["SeeUserName"].IsEmpty())
            {
                string SeeUserName = queryParam["SeeUserName"].ToString();
                strSql += " and SeeUserName  like '%" + SeeUserName + "%' ";
            }

            return this.BaseRepository().FindList(strSql.ToString(), pagination);
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<Ku_CompanySeeEntity> GetList(string queryJson)
        {
            return this.BaseRepository().IQueryable().ToList();
        }

        /// <summary>
        /// 浏览记录
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Ku_CompanySeeEntity> GetListByUserId(string searchName)
        {
            string strSql = @"select * from Ku_CompanySee where SeeUserId = '"+OperatorProvider.Provider.Current().UserId+"'";

            //公司名
            if (!searchName.IsEmpty())
            {
                strSql += " and CompanyName like '%" + searchName + "%' ";
            }
            strSql += " ORDER BY SeeDate desc";
            return this.BaseRepository().FindList(strSql.ToString());
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public Ku_CompanySeeEntity GetEntity(string keyValue)
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
        public void SaveForm(string keyValue, Ku_CompanySeeEntity entity)
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
