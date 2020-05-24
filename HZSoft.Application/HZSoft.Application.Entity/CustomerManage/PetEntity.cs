using System;
using HZSoft.Application.Code;

namespace HZSoft.Application.Entity.CustomerManage
{
    /// <summary>
    /// 版 本
    /// 
    /// 创 建：超级管理员
    /// 日 期：2020-05-21 14:42
    /// 描 述：宠物
    /// </summary>
    public class PetEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// Id
        /// </summary>
        /// <returns></returns>
        public int? Id { get; set; }
        /// <summary>
        /// 宠物名称
        /// </summary>
        /// <returns></returns>
        public string Name { get; set; }
        /// <summary>
        /// 主人Id
        /// </summary>
        /// <returns></returns>
        public string MasterId { get; set; }
        /// <summary>
        /// 主人名称
        /// </summary>
        /// <returns></returns>
        public string MasterName { get; set; }
        /// <summary>
        /// 品种id
        /// </summary>
        /// <returns></returns>
        public string ClassId { get; set; }
        /// <summary>
        /// 品种
        /// </summary>
        /// <returns></returns>
        public string ClassName { get; set; }
        /// <summary>
        /// 子类品种id
        /// </summary>
        /// <returns></returns>
        public string SubClassId { get; set; }
        /// <summary>
        /// 子类品种
        /// </summary>
        /// <returns></returns>
        public string SubClassName { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        /// <returns></returns>
        public int? Gender { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        /// <returns></returns>
        public string Photo { get; set; }
        /// <summary>
        /// 生日
        /// </summary>
        /// <returns></returns>
        public DateTime? Birthday { get; set; }
        /// <summary>
        /// 到家时间
        /// </summary>
        /// <returns></returns>
        public DateTime? HomeDate { get; set; }
        /// <summary>
        /// 绝育状态
        /// </summary>
        /// <returns></returns>
        public int? Sterilized { get; set; }
        /// <summary>
        /// 删除标记
        /// </summary>
        /// <returns></returns>
        public int? DeleteMark { get; set; }
        /// <summary>
        /// 有效标志
        /// </summary>
        /// <returns></returns>
        public int? EnabledMark { get; set; }
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
            //this.Id = Guid.NewGuid().ToString();
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