using HZSoft.Application.Entity.BaseManage;
using System.Data.Entity.ModelConfiguration;

namespace HZSoft.Application.Mapping.BaseManage
{
    /// <summary>
    /// 版 本
    /// 
    /// 创 建：超级管理员
    /// 日 期：2017-12-15 15:02
    /// 描 述：产品表
    /// </summary>
    public class POS_ProductMap : EntityTypeConfiguration<POS_ProductEntity>
    {
        public POS_ProductMap()
        {
            #region 表、主键
            //表
            this.ToTable("POS_Product");
            //主键
            this.HasKey(t => t.Id);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
