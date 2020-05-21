using System;
using HZSoft.Application.Code;

namespace HZSoft.Application.Entity.CustomerManage
{
    /// <summary>
    /// 版 本
    /// 
    /// 创 建：超级管理员
    /// 日 期：2017-12-02 16:17
    /// 描 述：POS_Shop
    /// </summary>
    public class POS_ShopEntity : BaseEntity
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
        /// 店铺名
        /// </summary>
        /// <returns></returns>
        public string ShopName { get; set; }
        /// <summary>
        /// 面积
        /// </summary>
        /// <returns></returns>
        public string Area { get; set; }
        /// <summary>
        /// 人数
        /// </summary>
        /// <returns></returns>
        public string Employee { get; set; }
        /// <summary>
        /// 店铺地址
        /// </summary>
        /// <returns></returns>
        public string ShopAddress { get; set; }
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
        /// 店铺照片
        /// </summary>
        /// <returns></returns>
        public string ShopPicture { get; set; }
        /// <summary>
        /// 联系人
        /// </summary>
        /// <returns></returns>
        public string Contacts { get; set; }
        /// <summary>
        /// 联系方式
        /// </summary>
        /// <returns></returns>
        public string Telphone { get; set; }
        /// <summary>
        /// 是否是老板
        /// </summary>
        /// <returns></returns>
        public int? IsBoss { get; set; }
        /// <summary>
        /// 备用联系人
        /// </summary>
        /// <returns></returns>
        public string ContactsSpare { get; set; }
        /// <summary>
        /// 备用联系方式
        /// </summary>
        /// <returns></returns>
        public string TelphoneSpare { get; set; }

        /// <summary>
        /// 注册资本
        /// </summary>
        /// <returns></returns>
        public string Capital { get; set; }
        /// <summary>
        /// 成立日期
        /// </summary>
        /// <returns></returns>
        public DateTime? BuildTime { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        /// <returns></returns>
        public string Mailbox { get; set; }
        /// <summary>
        /// 网站
        /// </summary>
        /// <returns></returns>
        public string Website { get; set; }
        /// <summary>
        /// 经营范围
        /// </summary>
        /// <returns></returns>
        public string Scope { get; set; }
        


        /// <summary>
        /// 状态
        /// </summary>
        /// <returns></returns>
        public int? IsSee { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        /// <returns></returns>
        public int? State { get; set; }
        /// <summary>
        /// 上传人id
        /// </summary>
        /// <returns></returns>
        public string SellerId { get; set; }
        /// <summary>
        /// 上传人姓名
        /// </summary>
        /// <returns></returns>
        public string SellerName { get; set; }
        /// <summary>
        /// 上传人微信ID
        /// </summary>
        /// <returns></returns>
        public string OpenId { get; set; }
        /// <summary>
        /// 上传人微信昵称
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