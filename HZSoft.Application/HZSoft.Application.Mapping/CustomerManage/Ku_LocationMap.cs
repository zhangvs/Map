using HZSoft.Application.Entity.CustomerManage;
using System.Data.Entity.ModelConfiguration;

namespace HZSoft.Application.Mapping.CustomerManage
{
    /// <summary>
    /// 版 本
    /// 
    /// 创 建：超级管理员
    /// 日 期：2018-03-26 11:54
    /// 描 述：坐标库
    /// </summary>
    public class Ku_LocationMap : EntityTypeConfiguration<Ku_LocationEntity>
    {
        public Ku_LocationMap()
        {
            #region 表、主键
            //表
            this.ToTable("Ku_Location");
            //主键
            this.HasKey(t => t.Id);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
