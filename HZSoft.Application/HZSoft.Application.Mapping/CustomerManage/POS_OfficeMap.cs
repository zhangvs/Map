using HZSoft.Application.Entity.CustomerManage;
using System.Data.Entity.ModelConfiguration;

namespace HZSoft.Application.Mapping.CustomerManage
{
    /// <summary>
    /// �� ��
    /// 
    /// �� ������������Ա
    /// �� �ڣ�2017-12-11 18:53
    /// �� ������չд��¥
    /// </summary>
    public class POS_OfficeMap : EntityTypeConfiguration<POS_OfficeEntity>
    {
        public POS_OfficeMap()
        {
            #region ������
            //��
            this.ToTable("POS_Office");
            //����
            this.HasKey(t => t.Id);
            #endregion

            #region ���ù�ϵ
            #endregion
        }
    }
}
