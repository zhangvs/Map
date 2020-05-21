using HZSoft.Application.Entity.CustomerManage;
using System.Data.Entity.ModelConfiguration;

namespace HZSoft.Application.Mapping.CustomerManage
{
    /// <summary>
    /// 版 本
    /// 
    /// 创 建：超级管理员
    /// 日 期：2018-06-27 14:31
    /// 描 述：公司浏览记录
    /// </summary>
    public class Ku_CompanySeeMap : EntityTypeConfiguration<Ku_CompanySeeEntity>
    {
        public Ku_CompanySeeMap()
        {
            #region 表、主键
            //表
            this.ToTable("Ku_CompanySee");
            //主键
            this.HasKey(t => t.Id);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
