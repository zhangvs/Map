using HZSoft.Application.Entity.CustomerManage;
using System.Data.Entity.ModelConfiguration;

namespace HZSoft.Application.Mapping.CustomerManage
{
    /// <summary>
    /// 版 本
    /// 
    /// 创 建：超级管理员
    /// 日 期：2017-12-23 14:41
    /// 描 述：余量统计
    /// </summary>
    public class Sale_Customer_ItemMap : EntityTypeConfiguration<Sale_Customer_ItemEntity>
    {
        public Sale_Customer_ItemMap()
        {
            #region 表、主键
            //表
            this.ToTable("Sale_Customer_Item");
            //主键
            this.HasKey(t => t.Id);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
