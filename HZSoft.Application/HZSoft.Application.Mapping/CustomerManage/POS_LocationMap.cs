using HZSoft.Application.Entity.CustomerManage;
using System.Data.Entity.ModelConfiguration;

namespace HZSoft.Application.Mapping.CustomerManage
{
    /// <summary>
    /// �� ��
    /// 
    /// �� ������������Ա
    /// �� �ڣ�2018-03-15 09:39
    /// �� ������ҵλ����Ϣ
    /// </summary>
    public class POS_LocationMap : EntityTypeConfiguration<POS_LocationEntity>
    {
        public POS_LocationMap()
        {
            #region ������
            //��
            this.ToTable("POS_Location");
            //����
            this.HasKey(t => t.Id);
            #endregion

            #region ���ù�ϵ
            #endregion
        }
    }
}
