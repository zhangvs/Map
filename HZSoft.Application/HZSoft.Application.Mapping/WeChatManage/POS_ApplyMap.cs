using HZSoft.Application.Entity.WeChatManage;
using System.Data.Entity.ModelConfiguration;

namespace HZSoft.Application.Mapping.WeChatManage
{
    /// <summary>
    /// �� ��
    /// 
    /// �� ������������Ա
    /// �� �ڣ�2017-11-24 16:45
    /// �� ��������POS��
    /// </summary>
    public class POS_ApplyMap : EntityTypeConfiguration<POS_ApplyEntity>
    {
        public POS_ApplyMap()
        {
            #region ������
            //��
            this.ToTable("POS_Apply");
            //����
            this.HasKey(t => t.ApplyID);
            #endregion

            #region ���ù�ϵ
            #endregion
        }
    }
}
