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
    /// 日 期：2018-06-15 14:39
    /// 描 述：关联公司
    /// </summary>
    public class Ku_RelationCompanyService : RepositoryFactory<Ku_RelationCompanyEntity>, Ku_RelationCompanyIService
    {
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<Ku_RelationCompanyEntity> GetPageList(Pagination pagination, string queryJson)
        {
            var expression = LinqExtensions.True<Ku_RelationCompanyEntity>();
            var queryParam = queryJson.ToJObject();
            //客户Id
            if (!queryParam["companyId"].IsEmpty())
            {
                int? CompanyId = Convert.ToInt32(queryParam["companyId"]);
                expression = expression.And(t => t.CompanyId == CompanyId);
            }
            //客户Id
            if (!queryParam["keyword"].IsEmpty())
            {
                string keyword = queryParam["keyword"].ToString();
                expression = expression.And(t => t.CompanyName.Contains(keyword) || t.RelationCompanyName.Contains(keyword));
            }

            return this.BaseRepository().IQueryable(expression).OrderByDescending(t => t.CreateDate).ToList();
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<Ku_RelationCompanyEntity> GetList(string queryJson)
        {
            return this.BaseRepository().IQueryable().ToList();
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public Ku_RelationCompanyEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }
        #endregion

        #region 验证数据
        /// <summary>
        /// 客户名称不能重复
        /// </summary>
        /// <param name="RelationCompanyName">名称</param>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public bool ExistFullName(string RelationCompanyName, string keyValue)
        {
            var expression = LinqExtensions.True<Ku_RelationCompanyEntity>();
            expression = expression.And(t => t.RelationCompanyName == RelationCompanyName);
            if (!string.IsNullOrEmpty(keyValue))
            {
                expression = expression.And(t => t.Id != keyValue);
            }
            return this.BaseRepository().IQueryable(expression).Count() == 0 ? true : false;
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void RemoveForm(string keyValue)
        {
            var entity = this.BaseRepository().FindEntity(keyValue);
            IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();
            var company = db.FindEntity<Ku_CompanyEntity>(t => t.Id == entity.CompanyId);
            //关联个数-1
            company.RelationCount = --company.RelationCount;
            db.Update<Ku_CompanyEntity>(company);
            db.Commit();

            this.BaseRepository().Delete(keyValue);
        }
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, Ku_RelationCompanyEntity entity)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                entity.Modify(keyValue);
                this.BaseRepository().Update(entity);
            }
            else
            {

                IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();

                //var relationData = db.FindEntity<Ku_RelationCompanyEntity>(t => t.CompanyId == entity.CompanyId && t.RelationCompanyId== entity.RelationCompanyId);//关联公司的重复判断  
                //if (relationData!=null)
                //{
                //    throw new Exception($"已存在与该公司[{entity.RelationCompanyName}]的关联！");
                //}

                var company = db.FindEntity<Ku_CompanyEntity>(t => t.Id == entity.CompanyId);

                //关联个数+1
                if (company.RelationCount == null)
                {
                    company.RelationCount = 1;
                }
                else
                {
                    company.RelationCount = ++company.RelationCount;
                }
                db.Update<Ku_CompanyEntity>(company);
                db.Commit();

                entity.Create();
                this.BaseRepository().Insert(entity);
            }
        }
        #endregion
    }
}
