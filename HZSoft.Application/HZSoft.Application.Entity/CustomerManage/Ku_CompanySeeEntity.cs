using System;
using HZSoft.Application.Code;

namespace HZSoft.Application.Entity.CustomerManage
{
    /// <summary>
    /// 版 本
    /// 
    /// 创 建：超级管理员
    /// 日 期：2018-06-27 14:31
    /// 描 述：公司浏览记录
    /// </summary>
    public class Ku_CompanySeeEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// 主键
        /// </summary>
        /// <returns></returns>
        public string Id { get; set; }
        /// <summary>
        /// 公司ID
        /// </summary>
        /// <returns></returns>
        public int? CompanyId { get; set; }
        /// <summary>
        /// 公司名称
        /// </summary>
        /// <returns></returns>
        public string CompanyName { get; set; }
        /// <summary>
        /// 查看日期
        /// </summary>
        /// <returns></returns>
        public DateTime? SeeDate { get; set; }
        /// <summary>
        /// 查看用户主键
        /// </summary>
        /// <returns></returns>
        public string SeeUserId { get; set; }
        /// <summary>
        /// 查看用户
        /// </summary>
        /// <returns></returns>
        public string SeeUserName { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public override void Create()
        {
            this.Id = Guid.NewGuid().ToString();
            this.SeeDate = DateTime.Now;
            this.SeeUserId = OperatorProvider.Provider.Current().UserId;
            this.SeeUserName = OperatorProvider.Provider.Current().UserName;
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.Id = keyValue;
            this.SeeDate = DateTime.Now;
            this.SeeUserId = OperatorProvider.Provider.Current().UserId;
            this.SeeUserName = OperatorProvider.Provider.Current().UserName;
        }
        #endregion
    }
}