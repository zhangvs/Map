using System;
using HZSoft.Application.Code;

namespace HZSoft.Application.Entity.WeChatManage
{
    /// <summary>
    /// 版 本
    /// 
    /// 创 建：超级管理员
    /// 日 期：2017-11-24 16:45
    /// 描 述：申请POS表
    /// </summary>
    public class POS_ApplyEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// 编号
        /// </summary>
        /// <returns></returns>
        public string ApplyID { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        /// <returns></returns>
        public string Telphone { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        /// <returns></returns>
        public string FullName { get; set; }
        /// <summary>
        /// 身份证号
        /// </summary>
        /// <returns></returns>
        public string IDCode { get; set; }
        /// <summary>
        /// 身份证有效期
        /// </summary>
        /// <returns></returns>
        public string IDDate { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        /// <returns></returns>
        public string IDAddress { get; set; }
        /// <summary>
        /// 身份证正面照
        /// </summary>
        /// <returns></returns>
        public string ID_Z { get; set; }
        /// <summary>
        /// 身份证反面照
        /// </summary>
        /// <returns></returns>
        public string ID_B { get; set; }
        /// <summary>
        /// 信用卡正面
        /// </summary>
        /// <returns></returns>
        public string Credit_Z { get; set; }
        /// <summary>
        /// 信用卡反面
        /// </summary>
        /// <returns></returns>
        public string Credit_B { get; set; }
        /// <summary>
        /// 信用卡号
        /// </summary>
        /// <returns></returns>
        public string CreditCode { get; set; }
        /// <summary>
        /// 信用卡开户行
        /// </summary>
        /// <returns></returns>
        public string CreditBankName { get; set; }
        /// <summary>
        /// 信用卡到期日
        /// </summary>
        /// <returns></returns>
        public string ValidThru { get; set; }
        /// <summary>
        /// 信用卡正面
        /// </summary>
        /// <returns></returns>
        public string Credit_Z1 { get; set; }
        /// <summary>
        /// 信用卡反面
        /// </summary>
        /// <returns></returns>
        public string Credit_B1 { get; set; }
        /// <summary>
        /// 信用卡号
        /// </summary>
        /// <returns></returns>
        public string CreditCode1 { get; set; }
        /// <summary>
        /// 信用卡开户行
        /// </summary>
        /// <returns></returns>
        public string CreditBankName1 { get; set; }
        /// <summary>
        /// 信用卡到期日
        /// </summary>
        /// <returns></returns>
        public string ValidThru1 { get; set; }
        /// <summary>
        /// 信用卡正面
        /// </summary>
        /// <returns></returns>
        public string Credit_Z2 { get; set; }
        /// <summary>
        /// 信用卡反面
        /// </summary>
        /// <returns></returns>
        public string Credit_B2 { get; set; }
        /// <summary>
        /// 信用卡号
        /// </summary>
        /// <returns></returns>
        public string CreditCode2 { get; set; }
        /// <summary>
        /// 信用卡开户行
        /// </summary>
        /// <returns></returns>
        public string CreditBankName2 { get; set; }
        /// <summary>
        /// 信用卡到期日
        /// </summary>
        /// <returns></returns>
        public string ValidThru2 { get; set; }
        /// <summary>
        /// 信用卡正面
        /// </summary>
        /// <returns></returns>
        public string Credit_Z3 { get; set; }
        /// <summary>
        /// 信用卡反面
        /// </summary>
        /// <returns></returns>
        public string Credit_B3 { get; set; }
        /// <summary>
        /// 信用卡号
        /// </summary>
        /// <returns></returns>
        public string CreditCode3 { get; set; }
        /// <summary>
        /// 信用卡开户行
        /// </summary>
        /// <returns></returns>
        public string CreditBankName3 { get; set; }
        /// <summary>
        /// 信用卡到期日
        /// </summary>
        /// <returns></returns>
        public string ValidThru3 { get; set; }
        /// <summary>
        /// 信用卡正面
        /// </summary>
        /// <returns></returns>
        public string Credit_Z4 { get; set; }
        /// <summary>
        /// 信用卡反面
        /// </summary>
        /// <returns></returns>
        public string Credit_B4 { get; set; }
        /// <summary>
        /// 信用卡号
        /// </summary>
        /// <returns></returns>
        public string CreditCode4 { get; set; }
        /// <summary>
        /// 信用卡开户行
        /// </summary>
        /// <returns></returns>
        public string CreditBankName4 { get; set; }
        /// <summary>
        /// 信用卡到期日
        /// </summary>
        /// <returns></returns>
        public string ValidThru4 { get; set; }

        /// <summary>
        /// 推荐人
        /// </summary>
        /// <returns></returns>
        public string RecommenderId { get; set; }
        /// <summary>
        /// 推荐人
        /// </summary>
        /// <returns></returns>
        public string Recommender { get; set; }
        /// <summary>
        /// 通过标识
        /// </summary>
        /// <returns></returns>
        public int? PassMark { get; set; }
        /// <summary>
        /// 创建时间
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
            this.ApplyID = Guid.NewGuid().ToString();
                                            }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.ApplyID = keyValue;
                                            }
        #endregion
    }
}