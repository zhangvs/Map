using HZSoft.Application.Entity.CustomerManage;
using System.Data.Entity.ModelConfiguration;

namespace HZSoft.Application.Mapping.CustomerManage
{
    /// <summary>
    /// 版 本
    /// 
    /// 创 建：超级管理员
    /// 日 期：2020-05-23 10:50
    /// 描 述：主人
    /// </summary>
    public class Pet_MasterMap : EntityTypeConfiguration<Pet_MasterEntity>
    {
        public Pet_MasterMap()
        {
            #region 表、主键
            //表
            this.ToTable("Pet_Master");
            //主键
            this.HasKey(t => t.Id);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
