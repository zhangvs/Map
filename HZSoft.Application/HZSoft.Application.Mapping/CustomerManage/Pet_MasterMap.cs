using HZSoft.Application.Entity.CustomerManage;
using System.Data.Entity.ModelConfiguration;

namespace HZSoft.Application.Mapping.CustomerManage
{
    /// <summary>
    /// �� ��
    /// 
    /// �� ������������Ա
    /// �� �ڣ�2020-05-23 10:50
    /// �� ��������
    /// </summary>
    public class Pet_MasterMap : EntityTypeConfiguration<Pet_MasterEntity>
    {
        public Pet_MasterMap()
        {
            #region ������
            //��
            this.ToTable("Pet_Master");
            //����
            this.HasKey(t => t.Id);
            #endregion

            #region ���ù�ϵ
            #endregion
        }
    }
}
