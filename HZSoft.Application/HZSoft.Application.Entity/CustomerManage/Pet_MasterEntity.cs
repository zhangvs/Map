using System;
using HZSoft.Application.Code;

namespace HZSoft.Application.Entity.CustomerManage
{
    /// <summary>
    /// 版 本
    /// 
    /// 创 建：超级管理员
    /// 日 期：2020-05-23 10:50
    /// 描 述：宠主
    /// </summary>
    public class Pet_MasterEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// Id
        /// </summary>
        /// <returns></returns>
        public int? Id { get; set; }
        /// <summary>
        /// 经度
        /// </summary>
        /// <returns></returns>
        public string wxLon { get; set; }
        /// <summary>
        /// 纬度
        /// </summary>
        /// <returns></returns>
        public string wxLat { get; set; }
        /// <summary>
        /// 百度经度
        /// </summary>
        /// <returns></returns>
        public string bdLon { get; set; }
        /// <summary>
        /// 百度经度
        /// </summary>
        /// <returns></returns>
        public string bdLat { get; set; }
        /// <summary>
        /// 省份
        /// </summary>
        /// <returns></returns>
        public string Province { get; set; }
        /// <summary>
        /// 区号
        /// </summary>
        /// <returns></returns>
        public string CityCode { get; set; }
        /// <summary>
        /// 城市
        /// </summary>
        /// <returns></returns>
        public string City { get; set; }
        /// <summary>
        /// 区县
        /// </summary>
        /// <returns></returns>
        public string District { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        /// <returns></returns>
        public string Address { get; set; }
        /// <summary>
        /// 宠主级别
        /// </summary>
        /// <returns></returns>
        public int? Lv { get; set; }
        /// <summary>
        /// 主人头像
        /// </summary>
        /// <returns></returns>
        public string Photo { get; set; }
        /// <summary>
        /// 主人名称
        /// </summary>
        /// <returns></returns>
        public string Name { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        /// <returns></returns>
        public int? Gender { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        /// <returns></returns>
        public string Telphone { get; set; }
        /// <summary>
        /// 宠物数量
        /// </summary>
        /// <returns></returns>
        public int? PetsCount { get; set; }
        /// <summary>
        /// 宠物
        /// </summary>
        /// <returns></returns>
        public string PetsName { get; set; }
        /// <summary>
        /// 微信Id
        /// </summary>
        /// <returns></returns>
        public string OpenId { get; set; }
        /// <summary>
        /// 微信昵称
        /// </summary>
        /// <returns></returns>
        public string NickName { get; set; }
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
            this.CreateDate = DateTime.Now;
            //this.CreateUserId = OperatorProvider.Provider.Current().UserId;
            //this.CreateUserName = OperatorProvider.Provider.Current().UserName;
            PetsCount = 0;
            DeleteMark = 0;
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(int? keyValue)
        {
            this.Id = keyValue;
            this.ModifyDate = DateTime.Now;
            //this.ModifyUserId = OperatorProvider.Provider.Current().UserId;
            //this.ModifyUserName = OperatorProvider.Provider.Current().UserName;
        }
        #endregion
    }
}