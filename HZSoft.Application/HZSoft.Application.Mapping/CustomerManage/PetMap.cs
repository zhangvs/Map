using HZSoft.Application.Entity.CustomerManage;
using System.Data.Entity.ModelConfiguration;

namespace HZSoft.Application.Mapping.CustomerManage
{
    /// <summary>
    /// �� ��
    /// 
    /// �� ������������Ա
    /// �� �ڣ�2020-05-21 14:42
    /// �� ��������
    /// </summary>
    public class PetMap : EntityTypeConfiguration<PetEntity>
    {
        public PetMap()
        {
            #region ������
            //��
            this.ToTable("Pet");
            //����
            this.HasKey(t => t.Id);
            #endregion

            #region ���ù�ϵ
            #endregion
        }
    }
}
