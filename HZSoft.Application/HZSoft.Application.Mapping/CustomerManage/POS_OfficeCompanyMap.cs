using HZSoft.Application.Entity.CustomerManage;
using System.Data.Entity.ModelConfiguration;

namespace HZSoft.Application.Mapping.CustomerManage
{
    /// <summary>
    /// �� ��
    /// 
    /// �� ������������Ա
    /// �� �ڣ�2017-12-12 10:37
    /// �� ������չĿ��
    /// </summary>
    public class POS_OfficeCompanyMap : EntityTypeConfiguration<POS_OfficeCompanyEntity>
    {
        public POS_OfficeCompanyMap()
        {
            #region ������
            //��
            this.ToTable("POS_OfficeCompany");
            //����
            this.HasKey(t => t.Id);
            #endregion

            #region ���ù�ϵ
            #endregion
        }
    }
}
