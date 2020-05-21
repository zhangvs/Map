using HZSoft.Application.Entity.WeChatManage;
using System.Data.Entity.ModelConfiguration;

namespace HZSoft.Application.Mapping.WeChatManage
{
    /// <summary>
    /// 版 本
    /// 
    /// 创 建：超级管理员
    /// 日 期：2017-11-24 16:45
    /// 描 述：申请POS表
    /// </summary>
    public class POS_ApplyMap : EntityTypeConfiguration<POS_ApplyEntity>
    {
        public POS_ApplyMap()
        {
            #region 表、主键
            //表
            this.ToTable("POS_Apply");
            //主键
            this.HasKey(t => t.ApplyID);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
