using HZSoft.Application.Entity.CustomerManage;
using System.Data.Entity.ModelConfiguration;

namespace HZSoft.Application.Mapping.CustomerManage
{
    /// <summary>
    /// 版 本
    /// 
    /// 创 建：超级管理员
    /// 日 期：2018-04-24 10:20
    /// 描 述：公司更新个数
    /// </summary>
    public class Ku_CompanyCountMap : EntityTypeConfiguration<Ku_CompanyCountEntity>
    {
        public Ku_CompanyCountMap()
        {
            #region 表、主键
            //表
            this.ToTable("Ku_CompanyCount");
            //主键
            this.HasKey(t => t.Id);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
