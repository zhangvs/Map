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
    /// 日 期：2018-03-23 16:49
    /// 描 述：公司联系人库
    /// </summary>
    public class Ku_CompanyContactService : RepositoryFactory<Ku_CompanyContactEntity>, Ku_CompanyContactIService
    {
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<Ku_CompanyContactEntity> GetPageList(Pagination pagination, string queryJson)
        {
            var expression = LinqExtensions.True<Ku_CompanyContactEntity>();
            var queryParam = queryJson.ToJObject();
            //客户Id
            if (!queryParam["companyId"].IsEmpty())
            {
                int? CompanyId = Convert.ToInt32(queryParam["companyId"]);
                expression = expression.And(t => t.CompanyId == CompanyId);
            }
            //查询条件
            if (!queryParam["condition"].IsEmpty() && !queryParam["keyword"].IsEmpty())
            {
                string condition = queryParam["condition"].ToString();
                string keyword = queryParam["keyword"].ToString();
                switch (condition)
                {
                    case "Contact":         //联系人
                        expression = expression.And(t => t.Contact.Contains(keyword));
                        break;
                    case "Mobile":          //手机
                        expression = expression.And(t => t.Mobile.Contains(keyword));
                        break;
                    case "Tel":             //电话
                        expression = expression.And(t => t.Tel.Contains(keyword));
                        break;
                    case "QQ":              //QQ
                        expression = expression.And(t => t.QQ.Contains(keyword));
                        break;
                    default:
                        break;
                }
            }
            return this.BaseRepository().IQueryable(expression).OrderByDescending(t => t.CreateDate).ToList();
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<Ku_CompanyContactEntity> GetList(string queryJson)
        {
            return this.BaseRepository().IQueryable().ToList();
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public Ku_CompanyContactEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="CompanyId">主键值</param>
        /// <returns></returns>
        public IEnumerable<Ku_CompanyContactEntity> GetCompanyContactList(int? CompanyId)
        {
            return this.BaseRepository().FindList("select * from Ku_CompanyContact where CompanyId=" + CompanyId);
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
        public void SaveForm(string keyValue, Ku_CompanyContactEntity entity)
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
