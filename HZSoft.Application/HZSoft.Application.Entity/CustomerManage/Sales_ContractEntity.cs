using System;
using System.ComponentModel.DataAnnotations.Schema;
using HZSoft.Application.Code;

namespace HZSoft.Application.Entity.CustomerManage
{
    /// <summary>
    /// 版 本
    /// 
    /// 创 建：超级管理员
    /// 日 期：2017-12-15 16:52
    /// 描 述：销售合同表
    /// </summary>
    public class Sales_ContractEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// 合同Id
        /// </summary>
        /// <returns></returns>
        [Column("ID")]
        public string Id { get; set; }
        /// <summary>
        /// 合同编号
        /// </summary>
        /// <returns></returns>
        [Column("CONTRACTNUM")]
        public string ContractNum { get; set; }
        /// <summary>
        /// 客户Id
        /// </summary>
        /// <returns></returns>
        [Column("CUSTOMERID")]
        public string CustomerId { get; set; }
        /// <summary>
        /// 客户名称
        /// </summary>
        /// <returns></returns>
        [Column("CUSTOMERNAME")]
        public string CustomerName { get; set; }
        /// <summary>
        /// 客户公司
        /// </summary>
        /// <returns></returns>
        [Column("CUSTOMERCOMPANY")]
        public string CustomerCompany { get; set; }
        /// <summary>
        /// 合同日期
        /// </summary>
        /// <returns></returns>
        [Column("CONTRACTDATE")]
        public DateTime? ContractDate { get; set; }
        /// <summary>
        /// 总数量
        /// </summary>
        /// <returns></returns>
        [Column("TOTALCOUNT")]
        public decimal? TotalCount { get; set; }
        /// <summary>
        /// 总金额
        /// </summary>
        /// <returns></returns>
        [Column("TotalAmount")]
        public decimal? TotalAmount { get; set; }
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
        [Column("USERID")]
        public string UserId { get; set; }
        /// <summary>
        /// 添加人
        /// </summary>
        /// <returns></returns>
        [Column("USERNAME")]
        public string UserName { get; set; }
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
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public override void Create()
        {
            this.Id = Guid.NewGuid().ToString();
            this.CreateDate = DateTime.Now;
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