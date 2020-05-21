using System;
using HZSoft.Application.Code;

namespace HZSoft.Application.Entity.CustomerManage
{
    /// <summary>
    /// 版 本
    /// 
    /// 创 建：超级管理员
    /// 日 期：2018-03-23 16:28
    /// 描 述：公司库
    /// </summary>
    public class Ku_CompanyEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// 唯一标识
        /// </summary>
        /// <returns></returns>
        public int? Id { get; set; }
        /// <summary>
        /// 公司名
        /// </summary>
        /// <returns></returns>
        public string CompanyName { get; set; }
        /// <summary>
        /// 曾用名
        /// </summary>
        /// <returns></returns>
        public string CompanyOldName { get; set; }
        /// <summary>
        /// 注册号
        /// </summary>
        /// <returns></returns>
        public string CreditCode { get; set; }
        /// <summary>
        /// 注册号
        /// </summary>
        /// <returns></returns>
        public string CompanyCode { get; set; }
        /// <summary>
        /// 成立时间
        /// </summary>
        /// <returns></returns>
        public DateTime? BuildTime { get; set; }
        /// <summary>
        /// 法人代表
        /// </summary>
        /// <returns></returns>
        public string LegalPerson { get; set; }
        /// <summary>
        /// 经营状态
        /// </summary>
        /// <returns></returns>
        public int? ManageState { get; set; }
        /// <summary>
        /// 注册资本
        /// </summary>
        /// <returns></returns>
        public decimal? Capital { get; set; }
        /// <summary>
        /// 注册币种
        /// </summary>
        /// <returns></returns>
        public string CapitalCurrency { get; set; }
        /// <summary>
        /// 公司地址
        /// </summary>
        /// <returns></returns>
        public string CompanyAddress { get; set; }
        /// <summary>
        /// 公司类型
        /// </summary>
        /// <returns></returns>
        public string CompanyType { get; set; }
        /// <summary>
        /// 所属行业
        /// </summary>
        /// <returns></returns>
        public string Sector { get; set; }
        /// <summary>
        /// 经营范围
        /// </summary>
        /// <returns></returns>
        public string Scope { get; set; }
        /// <summary>
        /// 登记机关
        /// </summary>
        /// <returns></returns>
        public string RegistrationAgency { get; set; }
        /// <summary>
        /// 经营期限起
        /// </summary>
        /// <returns></returns>
        public DateTime? ManageTermStart { get; set; }
        /// <summary>
        /// 经营期限止
        /// </summary>
        /// <returns></returns>
        public DateTime? ManageTermEnd { get; set; }
        /// <summary>
        /// 核准日期
        /// </summary>
        /// <returns></returns>
        public DateTime? ApprovalDate { get; set; }
        /// <summary>
        /// 死亡日期
        /// </summary>
        /// <returns></returns>
        public DateTime? DeathDate { get; set; }
        /// <summary>
        /// 吊销日期
        /// </summary>
        /// <returns></returns>
        public DateTime? RevocationlDate { get; set; }
        /// <summary>
        /// 注销日期
        /// </summary>
        /// <returns></returns>
        public DateTime? LogoutDate { get; set; }
        /// <summary>
        /// 手机
        /// </summary>
        /// <returns></returns>
        public string Mobilephone { get; set; }
        /// <summary>
        /// 固话
        /// </summary>
        /// <returns></returns>
        public string Telphone { get; set; }
        /// <summary>
        /// 实际地址
        /// </summary>
        /// <returns></returns>
        public string RealAddress { get; set; }
        /// <summary>
        /// 备用地址
        /// </summary>
        /// <returns></returns>
        public string SpareAddress { get; set; }
        /// <summary>
        /// 实际行业分类
        /// </summary>
        /// <returns></returns>
        public string RealIndustryCategory { get; set; }
        /// <summary>
        /// 实际分类
        /// </summary>
        /// <returns></returns>
        public string RealIndustry { get; set; }
        /// <summary>
        /// 省
        /// </summary>
        /// <returns></returns>
        public string Province { get; set; }
        /// <summary>
        /// 市
        /// </summary>
        /// <returns></returns>
        public string City { get; set; }
        /// <summary>
        /// 地区
        /// </summary>
        /// <returns></returns>
        public string District { get; set; }
        /// <summary>
        /// 经度
        /// </summary>
        /// <returns></returns>
        public string Lon { get; set; }
        /// <summary>
        /// 维度
        /// </summary>
        /// <returns></returns>
        public string Lat { get; set; }
        /// <summary>
        /// 位置Id
        /// </summary>
        /// <returns></returns>
        public int? LocationId { get; set; }
        /// <summary>
        /// 商圈Id
        /// </summary>
        /// <returns></returns>
        public string RegeoId { get; set; }
        /// <summary>
        /// 商圈
        /// </summary>
        /// <returns></returns>
        public string RegeoName { get; set; }
        /// <summary>
        /// 是否老板
        /// </summary>
        /// <returns></returns>
        public int? IsBoss { get; set; }
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
        /// 邮箱
        /// </summary>
        /// <returns></returns>
        public string Mailbox { get; set; }
        /// <summary>
        /// 网址
        /// </summary>
        /// <returns></returns>
        public string Website { get; set; }
        /// <summary>
        /// 楼号
        /// </summary>
        /// <returns></returns>
        public string Building { get; set; }
        /// <summary>
        /// 楼层
        /// </summary>
        /// <returns></returns>
        public int? Floor { get; set; }
        /// <summary>
        /// 房间
        /// </summary>
        /// <returns></returns>
        public int? Room { get; set; }
        /// <summary>
        /// 公司照片
        /// </summary>
        /// <returns></returns>
        public string CompanyPicture { get; set; }
        /// <summary>
        /// 查看次数
        /// </summary>
        /// <returns></returns>
        public int? SeeTimes { get; set; }
        /// <summary>
        /// 关联公司个数
        /// </summary>
        /// <returns></returns>
        public int? RelationCount { get; set; }
        /// <summary>
        /// 专业化程度
        /// </summary>
        /// <returns></returns>
        public int? Specialized { get; set; }
        /// <summary>
        /// 跟进状态
        /// </summary>
        /// <returns></returns>
        public int? FollowState { get; set; }
        /// <summary>
        /// 确认状态
        /// </summary>
        /// <returns></returns>
        public int? ConfirmState { get; set; }
        /// <summary>
        /// 注销标记
        /// </summary>
        /// <returns></returns>
        public int? DeleteMark { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        /// <returns></returns>
        public string Description { get; set; }
        /// <summary>
        /// 获取状态
        /// </summary>
        /// <returns></returns>
        public int? ObtainState { get; set; }
        /// <summary>
        /// 获取日期
        /// </summary>
        /// <returns></returns>
        public DateTime? ObtainDate { get; set; }
        /// <summary>
        /// 获取用户Id
        /// </summary>
        /// <returns></returns>
        public string ObtainUserId { get; set; }
        /// <summary>
        /// 获取用户
        /// </summary>
        /// <returns></returns>
        public string ObtainUserName { get; set; }
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
        /// 新增调用自主添加
        /// </summary>
        public override void Create()
        {
            //this.Id = Guid.NewGuid().ToString();
            this.CreateDate = DateTime.Now;
            this.CreateUserId = OperatorProvider.Provider.Current().UserId;
            this.CreateUserName = OperatorProvider.Provider.Current().UserName;

            this.ObtainDate = DateTime.Now;
            this.ObtainUserId = OperatorProvider.Provider.Current().UserId;
            this.ObtainUserName = OperatorProvider.Provider.Current().UserName;
            this.ManageState = 1;//在营
            this.ObtainState = 1;
            this.DeleteMark = 0;
            this.SeeTimes = 0;
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