using HZSoft.Application.Entity.CustomerManage;
using System.Data.Entity.ModelConfiguration;

namespace HZSoft.Application.Mapping.CustomerManage
{
    /// <summary>
    /// 版 本
    /// 
    /// 创 建：超级管理员
    /// 日 期：2018-03-23 16:49
    /// 描 述：公司联系人库
    /// </summary>
    public class Ku_CompanyContactMap : EntityTypeConfiguration<Ku_CompanyContactEntity>
    {
        public Ku_CompanyContactMap()
        {
            #region 表、主键
            //表
            this.ToTable("Ku_CompanyContact");
            //主键
            this.HasKey(t => t.Id);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
