using HZSoft.Application.Entity.BaseManage;
using HZSoft.Application.Entity.CustomerManage;
using HZSoft.Application.IService.BaseManage;
using HZSoft.Application.IService.CustomerManage;
using HZSoft.Data.Repository;
using HZSoft.Util;
using HZSoft.Util.Extension;
using HZSoft.Util.WebControl;
using System.Collections.Generic;
using System.Linq;

namespace HZSoft.Application.Service.CustomerManage
{
    /// <summary>
    /// 版 本 6.1
    /// 
    /// 创 建：超级管理员
    /// 日 期：2017-12-18 13:59
    /// 描 述：产品分类
    /// </summary>
    public class Pet_TypeService : RepositoryFactory<Pet_TypeEntity>, Pet_TypeIService
    {
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<Pet_TypeEntity> GetPageList(Pagination pagination, string queryJson)
        {
            var expression = LinqExtensions.True<Pet_TypeEntity>();
            var queryParam = queryJson.ToJObject();
            string strSql = "select * from Pet_Type where 1 = 1";

            //分类id
            if (!queryParam["ItemId"].IsEmpty())
            {
                string ItemId = queryParam["ItemId"].ToString();
                strSql += " and ItemId = '" + ItemId + "'";
            }
            //父分类id
            if (!queryParam["ParentId"].IsEmpty())
            {
                string ParentId = queryParam["ParentId"].ToString();
                strSql += " and ParentId = '" + ParentId + "'";
            }
            else
            {
                strSql += " and ParentId <> '0'";
            }

            if (!queryParam["keyword"].IsEmpty())
            {
                string keyword = queryParam["keyword"].ToString();
                strSql += " and (ItemName like '%" + keyword + "%' or ItemCode like '%" + keyword + "%')";
            }

            return this.BaseRepository().FindList(strSql.ToString(), pagination);
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<Pet_TypeEntity> GetList(string queryJson)
        {
            var expression = LinqExtensions.True<Pet_TypeEntity>();
            var queryParam = queryJson.ToJObject();
            string strSql = "select * from Pet_Type where 1 = 1";
            string strOrder = " ORDER BY sortcode asc";

            //分类id
            if (!queryParam["ItemId"].IsEmpty())
            {
                string ItemId = queryParam["ItemId"].ToString();
                strSql += " and ItemId = '" + ItemId + "'";
            }
            //父分类id
            if (!queryParam["ParentId"].IsEmpty())
            {
                string ParentId = queryParam["ParentId"].ToString();
                strSql += " and ParentId = '" + ParentId + "'";
            }
            else
            {
                strSql += " and ParentId <> '0'";
            }

            if (!queryParam["keyword"].IsEmpty())
            {
                string keyword = queryParam["keyword"].ToString();
                strSql += " and (ItemName like '%" + keyword + "%' or ItemCode like '%" + keyword + "%')";
            }

            strSql += strOrder;
            return this.BaseRepository().FindList(strSql.ToString());
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns>返回列表</returns>
        public IEnumerable<Pet_TypeEntity> GetList()
        {
            return this.BaseRepository().IQueryable().ToList();
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public Pet_TypeEntity GetEntity(string keyValue)
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
        public void SaveForm(string keyValue, Pet_TypeEntity entity)
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

        #region 验证数据
        /// <summary>
        /// 项目值不能重复
        /// </summary>
        /// <param name="itemValue">项目值</param>
        /// <param name="keyValue">主键</param>
        /// <param name="itemId">分类Id</param>
        /// <returns></returns>
        public bool ExistItemValue(string itemValue, string keyValue, string itemId)
        {
            var expression = LinqExtensions.True<Pet_TypeEntity>();
            expression = expression.And(t => t.ItemCode == itemValue).And(t => t.ParentId == itemId);
            if (!string.IsNullOrEmpty(keyValue))
            {
                expression = expression.And(t => t.ItemId != keyValue);
            }
            return this.BaseRepository().IQueryable(expression).Count() == 0 ? true : false;
        }
        /// <summary>
        /// 项目名不能重复
        /// </summary>
        /// <param name="itemName">项目名</param>
        /// <param name="keyValue">主键</param>
        /// <param name="itemId">分类Id</param>
        /// <returns></returns>
        public bool ExistItemName(string itemName, string keyValue, string itemId)
        {
            var expression = LinqExtensions.True<Pet_TypeEntity>();
            expression = expression.And(t => t.ItemName == itemName).And(t => t.ParentId == itemId);
            if (!string.IsNullOrEmpty(keyValue))
            {
                expression = expression.And(t => t.ItemId != keyValue);
            }
            return this.BaseRepository().IQueryable(expression).Count() == 0 ? true : false;
        }
        #endregion
    }
}
