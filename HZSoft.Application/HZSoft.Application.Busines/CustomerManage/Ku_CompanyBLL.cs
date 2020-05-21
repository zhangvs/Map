using HZSoft.Application.Entity.CustomerManage;
using HZSoft.Application.IService.CustomerManage;
using HZSoft.Application.Service.CustomerManage;
using HZSoft.Util.WebControl;
using System.Collections.Generic;
using System;
using System.Data;

namespace HZSoft.Application.Busines.CustomerManage
{
    /// <summary>
    /// 版 本 6.1
    /// 
    /// 创 建：超级管理员
    /// 日 期：2018-03-23 16:28
    /// 描 述：公司库
    /// </summary>
    public class Ku_CompanyBLL
    {
        private Ku_CompanyIService service = new Ku_CompanyService();

        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<Ku_CompanyEntity> GetPageList(Pagination pagination, string queryJson)
        {
            return service.GetPageList(pagination, queryJson);
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<Ku_CompanyEntity> MyGetPageList(Pagination pagination, string queryJson)
        {
            return service.MyGetPageList(pagination, queryJson);
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<Ku_CompanyEntity> GetList(string queryJson)
        {
            return service.GetList(queryJson);
        }
        public IEnumerable<Ku_CompanyEntity> GetListByUserId(string searchName,string state)
        {
            return service.GetListByUserId(searchName, state);
        }
        /// <summary>
        /// 手机端-个人中心页面状态数量
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public DataTable GetFollowState()
        {
            return service.GetFollowState();
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public Ku_CompanyEntity GetEntity(int? keyValue)
        {
            return service.GetEntity(keyValue);
        }
        /// <summary>
        /// 获取列表自动补全公司名称
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<Ku_CompanyEntity> GetListByName(string CompanyName)
        {
            return service.GetListByName(CompanyName);
        }
        /// <summary>
        /// 获取列表自动补全公司名称（私池）
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<Ku_CompanyEntity> GetListByNameSi(string CompanyName)
        {
            return service.GetListByNameSi(CompanyName);
        }
        #endregion

        #region 验证数据
        /// <summary>
        /// 客户名称不能重复
        /// </summary>
        /// <param name="fullName">名称</param>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public bool ExistFullName(string CompanyName, string keyValue)
        {
            return service.ExistFullName(CompanyName, keyValue);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void RemoveForm(int? keyValue)
        {
            try
            {
                service.RemoveForm(keyValue);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public void SaveForm(int? keyValue, Ku_CompanyEntity entity)
        {
            try
            {
                service.SaveForm(keyValue, entity);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 分配
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public void ObtainForm(int? keyValue)
        {
            try
            {
                service.ObtainForm(keyValue);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 保存跟进人
        /// </summary>
        /// <param name="keyValues">客户ids</param>
        /// <param name="TraceUserId">员工id</param>
        /// <param name="TraceUserName">员工名称</param>
        public void SaveAssign(int? keyValue, string ObtainUserId, string ObtainUserName)
        {
            try
            {
                service.SaveAssign(keyValue, ObtainUserId, ObtainUserName);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 放入公海
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public void ThrowForm(int? keyValue)
        {
            try
            {
                service.ThrowForm(keyValue);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 批量领取
        /// </summary>
        /// <returns></returns>
        public string BatchObtain(DataTable dtSource)
        {
            try
            {
                string returnMsg = service.BatchObtain(dtSource);
                return returnMsg;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        #endregion
    }
}
