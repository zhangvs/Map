using System;
using HZSoft.Application.Code;

namespace HZSoft.Application.Entity.CustomerManage
{
    /// <summary>
    /// 版 本
    /// 
    /// 创 建：超级管理员
    /// 日 期：2017-12-11 18:53
    /// 描 述：拓展写字楼
    /// </summary>
    public class POS_OfficeEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// 唯一标识
        /// </summary>
        /// <returns></returns>
        public string Id { get; set; }
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
        /// 百度纬度
        /// </summary>
        /// <returns></returns>
        public string bdLat { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        /// <returns></returns>
        public string OfficeAddress { get; set; }
        /// <summary>
        /// 省份
        /// </summary>
        /// <returns></returns>
        public string Province { get; set; }
        /// <summary>
        /// 区县
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
        /// 写字楼名
        /// </summary>
        /// <returns></returns>
        public string OfficeName { get; set; }
        /// <summary>
        /// 写字楼照片
        /// </summary>
        /// <returns></returns>
        public string OfficePicture { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        /// <returns></returns>
        public int? IsSee { get; set; }
        /// <summary>
        /// 查看状态
        /// </summary>
        /// <returns></returns>
        public int? State { get; set; }
        /// <summary>
        /// 上传人Id
        /// </summary>
        /// <returns></returns>
        public string SellerId { get; set; }
        /// <summary>
        /// 上传人姓名
        /// </summary>
        /// <returns></returns>
        public string SellerName { get; set; }
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
        /// 备注
        /// </summary>
        /// <returns></returns>
        public string Description { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>
        /// <returns></returns>
        public DateTime? CreateTime { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public override void Create()
        {
            this.Id = Guid.NewGuid().ToString();
            CreateTime = DateTime.Now;
                                            }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.Id = keyValue;
                                            }
        #endregion
    }
}