using HZSoft.Application.Entity.CustomerManage;
using System.Data.Entity.ModelConfiguration;

namespace HZSoft.Application.Mapping.CustomerManage
{
    /// <summary>
    /// �� ��
    /// 
    /// �� ������������Ա
    /// �� �ڣ�2020-05-24 15:17
    /// �� ��������Ʒ��
    /// </summary>
    public class Pet_TypeMap : EntityTypeConfiguration<Pet_TypeEntity>
    {
        public Pet_TypeMap()
        {
            #region ������
            //��
            this.ToTable("Pet_Type");
            //����
            this.HasKey(t => t.ItemId);
            #endregion

            #region ���ù�ϵ
            #endregion
        }
    }
}
