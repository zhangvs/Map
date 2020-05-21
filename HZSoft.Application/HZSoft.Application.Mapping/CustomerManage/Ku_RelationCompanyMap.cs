using HZSoft.Application.Entity.CustomerManage;
using System.Data.Entity.ModelConfiguration;

namespace HZSoft.Application.Mapping.CustomerManage
{
    /// <summary>
    /// 版 本
    /// 
    /// 创 建：超级管理员
    /// 日 期：2018-06-15 14:39
    /// 描 述：关联公司
    /// </summary>
    public class Ku_RelationCompanyMap : EntityTypeConfiguration<Ku_RelationCompanyEntity>
    {
        public Ku_RelationCompanyMap()
        {
            #region 表、主键
            //表
            this.ToTable("Ku_RelationCompany");
            //主键
            this.HasKey(t => t.Id);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
