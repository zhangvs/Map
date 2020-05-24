using HZSoft.Application.Entity.CustomerManage;
using System.Data.Entity.ModelConfiguration;

namespace HZSoft.Application.Mapping.CustomerManage
{
    /// <summary>
    /// 版 本
    /// 
    /// 创 建：超级管理员
    /// 日 期：2020-05-21 14:42
    /// 描 述：宠物
    /// </summary>
    public class PetMap : EntityTypeConfiguration<PetEntity>
    {
        public PetMap()
        {
            #region 表、主键
            //表
            this.ToTable("Pet");
            //主键
            this.HasKey(t => t.Id);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
