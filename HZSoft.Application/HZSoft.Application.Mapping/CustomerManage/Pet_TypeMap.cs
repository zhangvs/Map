using HZSoft.Application.Entity.CustomerManage;
using System.Data.Entity.ModelConfiguration;

namespace HZSoft.Application.Mapping.CustomerManage
{
    /// <summary>
    /// 版 本
    /// 
    /// 创 建：超级管理员
    /// 日 期：2020-05-24 15:17
    /// 描 述：宠物品类
    /// </summary>
    public class Pet_TypeMap : EntityTypeConfiguration<Pet_TypeEntity>
    {
        public Pet_TypeMap()
        {
            #region 表、主键
            //表
            this.ToTable("Pet_Type");
            //主键
            this.HasKey(t => t.ItemId);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
