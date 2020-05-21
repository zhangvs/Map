using HZSoft.Application.Entity.CustomerManage;
using System.Data.Entity.ModelConfiguration;

namespace HZSoft.Application.Mapping.CustomerManage
{
    /// <summary>
    /// 版 本
    /// 
    /// 创 建：超级管理员
    /// 日 期：2017-12-12 10:37
    /// 描 述：拓展目标
    /// </summary>
    public class POS_OfficeCompanyMap : EntityTypeConfiguration<POS_OfficeCompanyEntity>
    {
        public POS_OfficeCompanyMap()
        {
            #region 表、主键
            //表
            this.ToTable("POS_OfficeCompany");
            //主键
            this.HasKey(t => t.Id);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
