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
    public class Sale_Customer_ItemEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// 统计子表Id
        /// </summary>
        /// <returns></returns>
        [Column("ID")]
        public string Id { get; set; }
        /// <summary>
        /// 客户Id
        /// </summary>
        /// <returns></returns>
        [Column("CUSTOMERID")]
        public string CustomerId { get; set; }
        /// <summary>
        /// 产品Id
        /// </summary>
        /// <returns></returns>
        [Column("PRODUCTID")]
        public string ProductId { get; set; }
        /// <summary>
        /// 产品编号
        /// </summary>
        /// <returns></returns>
        [Column("PRODUCTCODE")]
        public string ProductCode { get; set; }
        /// <summary>
        /// 产品名称
        /// </summary>
        /// <returns></returns>
        [Column("PRODUCTNAME")]
        public string ProductName { get; set; }
        /// <summary>
        /// 单位
        /// </summary>
        /// <returns></returns>
        [Column("PRODUCTUNIT")]
        public string ProductUnit { get; set; }
        /// <summary>
        /// 单价
        /// </summary>
        /// <returns></returns>
        [Column("UNITPRICE")]
        public decimal? UnitPrice { get; set; }
        /// <summary>
        /// 累计金额
        /// </summary>
        /// <returns></returns>
        [Column("SUMAMOUNT")]
        public decimal? SumAmount { get; set; }
        /// <summary>
        /// 累计数量
        /// </summary>
        /// <returns></returns>
        [Column("SUMCOUNT")]
        public decimal? SumCount { get; set; }
        /// <summary>
        /// 剩余数量
        /// </summary>
        /// <returns></returns>
        [Column("YUCOUNT")]
        public decimal? YuCount { get; set; }
        /// <summary>
        /// 销售数量
        /// </summary>
        /// <returns></returns>
        [Column("SALECOUNT")]
        public decimal? SaleCount { get; set; }
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
        /// 排序
        /// </summary>
        /// <returns></returns>
        [Column("SORT")]
        public int? Sort { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        /// <returns></returns>
        [Column("DESCRIPTION")]
        public string Description { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public override void Create()
        {
            this.Id = Guid.NewGuid().ToString();
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