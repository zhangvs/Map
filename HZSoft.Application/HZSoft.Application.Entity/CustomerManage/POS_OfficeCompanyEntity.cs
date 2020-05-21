using System;
using HZSoft.Application.Code;

namespace HZSoft.Application.Entity.CustomerManage
{
    /// <summary>
    /// 版 本
    /// 
    /// 创 建：超级管理员
    /// 日 期：2017-12-12 10:37
    /// 描 述：拓展目标
    /// </summary>
    public class POS_OfficeCompanyEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// 唯一标识
        /// </summary>
        /// <returns></returns>
        public string Id { get; set; }
        /// <summary>
        /// 写字楼Id
        /// </summary>
        /// <returns></returns>
        public string OfficeId { get; set; }
        /// <summary>
        /// 写字楼
        /// </summary>
        /// <returns></returns>
        public string OfficeName { get; set; }
        /// <summary>
        /// 公司名
        /// </summary>
        /// <returns></returns>
        public string CompanyName { get; set; }
        /// <summary>
        /// 公司地址
        /// </summary>
        /// <returns></returns>
        public string CompanyAddress { get; set; }
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
        /// 公司照片
        /// </summary>
        /// <returns></returns>
        public string CompanyPicture { get; set; }
        /// <summary>
        /// 楼层
        /// </summary>
        /// <returns></returns>
        public string Floor { get; set; }
        /// <summary>
        /// 房间
        /// </summary>
        /// <returns></returns>
        public string Room { get; set; }
        /// <summary>
        /// 联系人
        /// </summary>
        /// <returns></returns>
        public string Contacts { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        /// <returns></returns>
        public string Telphone { get; set; }
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
        /// 查看状态
        /// </summary>
        /// <returns></returns>
        public int? State { get; set; }
        /// <summary>
        /// 上传人
        /// </summary>
        /// <returns></returns>
        public string SellerId { get; set; }
        
        /// <summary>
        /// 上传人
        /// </summary>
        /// <returns></returns>
        public string SellerName { get; set; }
        /// <summary>
        /// 是否是老板
        /// </summary>
        /// <returns></returns>
        public int? IsBoss { get; set; }
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