using HZSoft.Application.Entity.CustomerManage;
using System.Data.Entity.ModelConfiguration;

namespace HZSoft.Application.Mapping.CustomerManage
{
    /// <summary>
    /// 版 本
    /// 
    /// 创 建：超级管理员
    /// 日 期：2017-12-11 18:53
    /// 描 述：拓展写字楼
    /// </summary>
    public class POS_OfficeMap : EntityTypeConfiguration<POS_OfficeEntity>
    {
        public POS_OfficeMap()
        {
            #region 表、主键
            //表
            this.ToTable("POS_Office");
            //主键
            this.HasKey(t => t.Id);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
