﻿using HZSoft.Application.Code;
using HZSoft.Application.Entity.BaseManage;
using HZSoft.Application.IService.BaseManage;
using HZSoft.Data;
using HZSoft.Data.Repository;
using HZSoft.Util.Extension;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace HZSoft.Application.Service.BaseManage
{
    /// <summary>
    /// 版 本 6.1
    /// 
    /// 创建人：佘赐雄
    /// 日 期：2015.11.02 14:27
    /// 描 述：部门管理
    /// </summary>
    public class DepartmentService : RepositoryFactory<DepartmentEntity>, IDepartmentService
    {
        #region 获取数据
        /// <summary>
        /// 部门列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<DepartmentEntity> GetList()
        {
            return this.BaseRepository().IQueryable().OrderByDescending(t => t.CreateDate).ToList();
        }
        /// <summary>
        /// 部门列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<DepartmentEntity> GetListByIds()
        {
            string strSql = "SELECT * FROM Base_Department ";
            if (!OperatorProvider.Provider.Current().IsSystem)
            {
                string dataAutor = string.Format(OperatorProvider.Provider.Current().DataAuthorize.ReadAutorize, OperatorProvider.Provider.Current().UserId);
                strSql += " WHERE DepartmentId IN( select DepartmentId from Base_User where 1=1";
                strSql += " and UserId in (" + dataAutor + ") GROUP BY DepartmentId )";
            }
            strSql += " ORDER BY SortCode ";
            return this.BaseRepository().FindList(strSql.ToString());
        }
        /// <summary>
        /// 部门实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public DepartmentEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }
        #endregion

        #region 验证数据
        /// <summary>
        /// 部门编号不能重复
        /// </summary>
        /// <param name="enCode">编号</param>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public bool ExistEnCode(string enCode, string keyValue)
        {
            var expression = LinqExtensions.True<DepartmentEntity>();
            expression = expression.And(t => t.EnCode == enCode);
            if (!string.IsNullOrEmpty(keyValue))
            {
                expression = expression.And(t => t.DepartmentId != keyValue);
            }
            return this.BaseRepository().IQueryable(expression).Count() == 0 ? true : false;
        }
        /// <summary>
        /// 部门名称不能重复
        /// </summary>
        /// <param name="fullName">名称</param>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public bool ExistFullName(string fullName, string keyValue)
        {
            var expression = LinqExtensions.True<DepartmentEntity>();
            expression = expression.And(t => t.FullName == fullName);
            if (!string.IsNullOrEmpty(keyValue))
            {
                expression = expression.And(t => t.DepartmentId != keyValue);
            }
            return this.BaseRepository().IQueryable(expression).Count() == 0 ? true : false;
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除部门
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void RemoveForm(string keyValue)
        {
            int count = this.BaseRepository().IQueryable(t => t.ParentId == keyValue).Count();
            if (count > 0)
            {
                throw new Exception("当前所选数据有子节点数据！");
            }
            this.BaseRepository().Delete(keyValue);
        }
        /// <summary>
        /// 保存部门表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="departmentEntity">机构实体</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, DepartmentEntity departmentEntity)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                departmentEntity.Modify(keyValue);
                this.BaseRepository().Update(departmentEntity);
            }
            else
            {
                departmentEntity.Create();
                this.BaseRepository().Insert(departmentEntity);
            }
        }
        #endregion
    }
}