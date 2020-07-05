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
    /// 日 期：2020-05-23 10:50
    /// 描 述：宠主
    /// </summary>
    public class Pet_MasterService : RepositoryFactory<Pet_MasterEntity>, Pet_MasterIService
    {
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<Pet_MasterEntity> GetPageList(Pagination pagination, string queryJson)
        {
            var expression = LinqExtensions.True<Pet_MasterEntity>();
            var queryParam = queryJson.ToJObject();
            string strSql = $"select * from Pet_Master where 1=1 ";
            //单据日期
            if (!queryParam["StartTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
            {
                DateTime startTime = queryParam["StartTime"].ToDate();
                DateTime endTime = queryParam["EndTime"].ToDate().AddDays(1);
                strSql += " and ModifyDate >= '" + startTime + "' and ModifyDate < '" + endTime + "'";
            }
            //商圈
            if (!queryParam["Name"].IsEmpty())
            {
                string Name = queryParam["Name"].ToString();
                strSql += " and Name  like '%" + Name + "%' ";
            }
            //区域
            if (!queryParam["District"].IsEmpty())
            {
                string District = queryParam["District"].ToString();
                District = District.Replace("[", "").Replace("]", "").Replace("\"", "'");
                strSql += " and District in(" + District + ")";
            }
            //POI分布
            if (!queryParam["TypeName"].IsEmpty())
            {
                string TypeName = queryParam["TypeName"].ToString();
                TypeName = TypeName.Replace("[", "").Replace("]", "").Replace("\"", "'");
                strSql += " and TypeName in(" + TypeName + ")";
            }
            return this.BaseRepository().FindList(strSql.ToString(), pagination);
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<Pet_MasterEntity> GetList(string queryJson)
        {
            var queryParam = queryJson.ToJObject();
            //限坐标表
            string strOrder = "";
            string strSql = "";
            //假如有微信坐标的话
            if (!queryParam["wxLon"].IsEmpty() && !queryParam["wxLat"].IsEmpty())
            {
                string wxLon = queryParam["wxLon"].ToString();
                string wxLat = queryParam["wxLat"].ToString();
                strSql = "select top 100 id,wxlon,wxlat,name,address,petsname,dbo.f_GetDistance(" + wxLon + "," + wxLat + ",wxlon,wxlat) as description,picture FROM Pet_Master t where 1=1";
                strOrder = " order by dbo.f_GetDistance(" + wxLon + "," + wxLat + ",wxlon,wxlat) asc";
            }
            else
            {
                strSql = "select top 100 id,wxlon,wxlat,name,address,petsname from Pet_Master where 1 = 1 ";
                strOrder = " ORDER BY CreateDate desc";
            }
            //店铺名
            if (!queryParam["SearchName"].IsEmpty())
            {
                string SearchName = queryParam["SearchName"].ToString();
                strSql += " and name = '" + SearchName + "'";
            }
            strSql += strOrder;

            return this.BaseRepository().FindList(strSql.ToString());
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public Pet_MasterEntity GetEntity(int? keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void RemoveForm(int? keyValue)
        {
            this.BaseRepository().Delete(keyValue);
        }
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public void SaveForm(int? keyValue, Pet_MasterEntity entity)
        {
            if (keyValue!=null)
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
