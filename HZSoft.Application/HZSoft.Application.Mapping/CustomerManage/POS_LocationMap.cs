using HZSoft.Application.Entity.CustomerManage;
using System.Data.Entity.ModelConfiguration;

namespace HZSoft.Application.Mapping.CustomerManage
{
    /// <summary>
    /// 版 本
    /// 
    /// 创 建：超级管理员
    /// 日 期：2018-03-15 09:39
    /// 描 述：企业位置信息
    /// </summary>
    public class POS_LocationMap : EntityTypeConfiguration<POS_LocationEntity>
    {
        public POS_LocationMap()
        {
            #region 表、主键
            //表
            this.ToTable("POS_Location");
            //主键
            this.HasKey(t => t.Id);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
