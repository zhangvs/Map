using System;
using System.ComponentModel.DataAnnotations.Schema;
using HZSoft.Application.Code;

namespace HZSoft.Application.Entity.CustomerManage
{
    /// <summary>
    /// 版 本
    /// 
    /// 创 建：超级管理员
    /// 日 期：2017-12-23 14:41
    /// 描 述：余量统计
    /// </summary>
    public class Sale_CustomerEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// 客户Id
        /// </summary>
        /// <returns></returns>
        [Column("CUSTOMERID")]
        public string CustomerId { get; set; }
        /// <summary>
        /// 客户公司
        /// </summary>
        /// <returns></returns>
        [Column("CUSTOMERCOMPANY")]
        public string CustomerCompany { get; set; }
        /// <summary>
        /// 总进货金额
        /// </summary>
        /// <returns></returns>
        [Column("SUMTOTALAMOUNT")]
        public decimal? SumTotalAmount { get; set; }
        /// <summary>
        /// 总进货量
        /// </summary>
        /// <returns></returns>
        [Column("SUMTOTALCOUNT")]
        public decimal? SumTotalCount { get; set; }
        /// <summary>
        /// 总剩余量
        /// </summary>
        /// <returns></returns>
        [Column("SUMYUCOUNT")]
        public decimal? SumYuCount { get; set; }
        /// <summary>
        /// 总销售量
        /// </summary>
        /// <returns></returns>
        [Column("SUMSALECOUNT")]
        public decimal? SumSaleCount { get; set; }
        /// <summary>
        /// 添加时间
        /// </summary>
        /// <returns></returns>
        [Column("CREATEDATE")]
        public DateTime? CreateDate { get; set; }
        /// <summary>
        /// 添加人Id
        /// </summary>
        /// <returns></returns>
        [Column("CREATEUSERID")]
        public string CreateUserId { get; set; }
        /// <summary>
        /// 添加人
        /// </summary>
        /// <returns></returns>
        [Column("CREATEUSERNAME")]
        public string CreateUserName { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        /// <returns></returns>
        [Column("STATUS")]
        public int? Status { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        /// <returns></returns>
        [Column("ISDEL")]
        public int? IsDel { get; set; }
        /// <summary>
        /// 备用
        /// </summary>
        /// <returns></returns>
        [Column("SPAREFIELD")]
        public string SpareField { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        /// <returns></returns>
        [Column("REMARK")]
        public string Remark { get; set; }
        /// <summary>
        /// 修改日期
        /// </summary>
        /// <returns></returns>
        [Column("MODIFYDATE")]
        public DateTime? ModifyDate { get; set; }
        /// <summary>
        /// 修改用户主键
        /// </summary>
        /// <returns></returns>
        [Column("MODIFYUSERID")]
        public string ModifyUserId { get; set; }
        /// <summary>
        /// 修改用户
        /// </summary>
        /// <returns></returns>
        [Column("MODIFYUSERNAME")]
        public string ModifyUserName { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public override void Create()
        {
            this.CreateDate = DateTime.Now;
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.CustomerId = keyValue;
            this.ModifyDate = DateTime.Now;
        }
        #endregion
    }
}