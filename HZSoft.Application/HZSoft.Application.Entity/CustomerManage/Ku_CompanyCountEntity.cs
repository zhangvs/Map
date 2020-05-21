using System;
using HZSoft.Application.Code;

namespace HZSoft.Application.Entity.CustomerManage
{
    /// <summary>
    /// 版 本
    /// 
    /// 创 建：超级管理员
    /// 日 期：2018-04-24 10:20
    /// 描 述：公司更新个数
    /// </summary>
    public class Ku_CompanyCountEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// Id
        /// </summary>
        /// <returns></returns>
        public int? Id { get; set; }
        /// <summary>
        /// 区域范围
        /// </summary>
        /// <returns></returns>
        public string AreaRange { get; set; }
        /// <summary>
        /// 本周起
        /// </summary>
        /// <returns></returns>
        public string WeekStartDate { get; set; }
        /// <summary>
        /// 本周止
        /// </summary>
        /// <returns></returns>
        public string WeekEndDate { get; set; }
        /// <summary>
        /// 本周个数
        /// </summary>
        /// <returns></returns>
        public string WeekCount { get; set; }
        /// <summary>
        /// 本月个数
        /// </summary>
        /// <returns></returns>
        public string MonthCount { get; set; }
        /// <summary>
        /// 本年个数
        /// </summary>
        /// <returns></returns>
        public string YearCount { get; set; }
        /// <summary>
        /// 总数
        /// </summary>
        /// <returns></returns>
        public string AllCount { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        /// <returns></returns>
        public string Description { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>
        /// <returns></returns>
        public DateTime? CreateDate { get; set; }
        /// <summary>
        /// 创建用户主键
        /// </summary>
        /// <returns></returns>
        public string CreateUserId { get; set; }
        /// <summary>
        /// 创建用户
        /// </summary>
        /// <returns></returns>
        public string CreateUserName { get; set; }
        /// <summary>
        /// 修改日期
        /// </summary>
        /// <returns></returns>
        public DateTime? ModifyDate { get; set; }
        /// <summary>
        /// 修改用户主键
        /// </summary>
        /// <returns></returns>
        public string ModifyUserId { get; set; }
        /// <summary>
        /// 修改用户
        /// </summary>
        /// <returns></returns>
        public string ModifyUserName { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public override void Create()
        {
            this.CreateDate = DateTime.Now;
            this.CreateUserId = OperatorProvider.Provider.Current().UserId;
            this.CreateUserName = OperatorProvider.Provider.Current().UserName;
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(int? keyValue)
        {
            this.Id = keyValue;
            this.ModifyDate = DateTime.Now;
            this.ModifyUserId = OperatorProvider.Provider.Current().UserId;
            this.ModifyUserName = OperatorProvider.Provider.Current().UserName;
        }
        #endregion
    }
}