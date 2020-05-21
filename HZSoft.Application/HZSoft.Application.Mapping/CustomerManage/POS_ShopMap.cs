using HZSoft.Application.Entity.CustomerManage;
using System.Data.Entity.ModelConfiguration;

namespace HZSoft.Application.Mapping.CustomerManage
{
    /// <summary>
    /// 版 本
    /// 
    /// 创 建：超级管理员
    /// 日 期：2017-12-02 16:17
    /// 描 述：POS_Shop
    /// </summary>
    public class POS_ShopMap : EntityTypeConfiguration<POS_ShopEntity>
    {
        public POS_ShopMap()
        {
            #region 表、主键
            //表
            this.ToTable("POS_Shop");
            //主键
            this.HasKey(t => t.Id);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
