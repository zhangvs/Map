using HZSoft.Application.Entity.CustomerManage;
using System.Data.Entity.ModelConfiguration;

namespace HZSoft.Application.Mapping.CustomerManage
{
    /// <summary>
    /// 版 本
    /// 
    /// 创 建：超级管理员
    /// 日 期：2017-12-15 16:52
    /// 描 述：销售合同表
    /// </summary>
    public class Sales_Contract_ItemMap : EntityTypeConfiguration<Sales_Contract_ItemEntity>
    {
        public Sales_Contract_ItemMap()
        {
            #region 表、主键
            //表
            this.ToTable("Sales_Contract_Item");
            //主键
            this.HasKey(t => t.Id);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
