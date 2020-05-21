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
    /// 日 期：2017-12-11 18:53
    /// 描 述：拓展写字楼
    /// </summary>
    public class POS_OfficeService : RepositoryFactory<POS_OfficeEntity>, POS_OfficeIService
    {
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<POS_OfficeEntity> GetPageList(Pagination pagination, string queryJson)
        {

            var expression = LinqExtensions.True<POS_OfficeEntity>();
            var queryParam = queryJson.ToJObject();
            string strSql = "select * from POS_Office where 1=1";
            //单据日期
            if (!queryParam["StartTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
            {
                DateTime startTime = queryParam["StartTime"].ToDate();
                DateTime endTime = queryParam["EndTime"].ToDate().AddDays(1);
                strSql += " and CreateTime >= '" + startTime + "' and CreateTime < '" + endTime + "'";
            }
            //店铺ID
            if (!queryParam["Id"].IsEmpty())
            {
                string Id = queryParam["Id"].ToString();
                strSql += " and Id = '" + Id + "'";
            }
            //店铺名
            if (!queryParam["OfficeName"].IsEmpty())
            {
                string OfficeName = queryParam["OfficeName"].ToString();
                strSql += " and OfficeName = '" + OfficeName + "'";
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
                strSql += " and SellerId = '" + SellerId + "'";
            }
            else
            {
                if (!OperatorProvider.Provider.Current().IsSystem)
                {
                    //限制城市权限
                    string des = OperatorProvider.Provider.Current().Description;
                    if (!string.IsNullOrEmpty(des))
                    {
                        des = des.Replace("|", "','");
                        strSql += " and (province in ('" + des + "') or city in ('" + des + "') or district in ('" + des + "')) ";
                    }
                }
            }

            return this.BaseRepository().FindList(strSql.ToString(), pagination);
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<POS_OfficeEntity> GetList(string queryJson)
        {
            var expression = LinqExtensions.True<POS_OfficeEntity>();
            var queryParam = queryJson.ToJObject();
            string wxLon = "";
            string wxLat = "";
            string strSql = "";
            string strOrder = "";
            if (!queryParam["wxLon"].IsEmpty()&& !queryParam["wxLat"].IsEmpty())
            {
                wxLon = queryParam["wxLon"].ToString();
                wxLat = queryParam["wxLat"].ToString();
                strSql = "SELECT TOP 50 id,wxlon,wxlat,officename,officepicture,OfficeAddress,dbo.f_GetDistance(" + wxLon + "," + wxLat + ",wxlon,wxlat) as Description FROM POS_Office where 1=1";
                strOrder= " order by dbo.f_GetDistance(" + wxLon + "," + wxLat + ",wxlon,wxlat) asc";
            }
            else
            {
                strSql = "select * from POS_Office where 1 = 1 ";
                strOrder = " ORDER BY CreateTime desc";
            }

            //店铺名
            if (!queryParam["SearchName"].IsEmpty())
            {
                string OfficeName = queryParam["SearchName"].ToString();
                strSql += " and OfficeName = '" + OfficeName + "'";
            }
            
            //销售人
            if (!OperatorProvider.Provider.Current().IsSystem)
            {
                //限制城市权限
                string des = OperatorProvider.Provider.Current().Description;
                if (!string.IsNullOrEmpty(des))
                {
                    des = des.Replace("|", "','");
                    strSql += " and (province in ('" + des + "') or city in ('" + des + "') or district in ('" + des + "')) ";
                }
            }

            strSql += strOrder;

            return this.BaseRepository().FindList(strSql.ToString());
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public POS_OfficeEntity GetEntity(string keyValue)
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
        public void SaveForm(string keyValue, POS_OfficeEntity entity)
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
