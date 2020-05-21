using System;
using HZSoft.Application.Code;

namespace HZSoft.Application.Entity.CustomerManage
{
    /// <summary>
    /// �� ��
    /// 
    /// �� ������������Ա
    /// �� �ڣ�2017-12-12 10:37
    /// �� ������չĿ��
    /// </summary>
    public class POS_OfficeCompanyEntity : BaseEntity
    {
        #region ʵ���Ա
        /// <summary>
        /// Ψһ��ʶ
        /// </summary>
        /// <returns></returns>
        public string Id { get; set; }
        /// <summary>
        /// д��¥Id
        /// </summary>
        /// <returns></returns>
        public string OfficeId { get; set; }
        /// <summary>
        /// д��¥
        /// </summary>
        /// <returns></returns>
        public string OfficeName { get; set; }
        /// <summary>
        /// ��˾��
        /// </summary>
        /// <returns></returns>
        public string CompanyName { get; set; }
        /// <summary>
        /// ��˾��ַ
        /// </summary>
        /// <returns></returns>
        public string CompanyAddress { get; set; }
        /// <summary>
        /// ���
        /// </summary>
        /// <returns></returns>
        public string Area { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        public string Employee { get; set; }
        /// <summary>
        /// ��˾��Ƭ
        /// </summary>
        /// <returns></returns>
        public string CompanyPicture { get; set; }
        /// <summary>
        /// ¥��
        /// </summary>
        /// <returns></returns>
        public string Floor { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        public string Room { get; set; }
        /// <summary>
        /// ��ϵ��
        /// </summary>
        /// <returns></returns>
        public string Contacts { get; set; }
        /// <summary>
        /// ��ϵ�绰
        /// </summary>
        /// <returns></returns>
        public string Telphone { get; set; }
        /// <summary>
        /// ������ϵ��
        /// </summary>
        /// <returns></returns>
        public string ContactsSpare { get; set; }
        /// <summary>
        /// ������ϵ��ʽ
        /// </summary>
        /// <returns></returns>
        public string TelphoneSpare { get; set; }

        /// <summary>
        /// ע���ʱ�
        /// </summary>
        /// <returns></returns>
        public string Capital { get; set; }
        /// <summary>
        /// ��������
        /// </summary>
        /// <returns></returns>
        public DateTime? BuildTime { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        public string Mailbox { get; set; }
        /// <summary>
        /// ��վ
        /// </summary>
        /// <returns></returns>
        public string Website { get; set; }
        /// <summary>
        /// ��Ӫ��Χ
        /// </summary>
        /// <returns></returns>
        public string Scope { get; set; }

        /// <summary>
        /// ״̬
        /// </summary>
        /// <returns></returns>
        public int? IsSee { get; set; }
        /// <summary>
        /// �鿴״̬
        /// </summary>
        /// <returns></returns>
        public int? State { get; set; }
        /// <summary>
        /// �ϴ���
        /// </summary>
        /// <returns></returns>
        public string SellerId { get; set; }
        
        /// <summary>
        /// �ϴ���
        /// </summary>
        /// <returns></returns>
        public string SellerName { get; set; }
        /// <summary>
        /// �Ƿ����ϰ�
        /// </summary>
        /// <returns></returns>
        public int? IsBoss { get; set; }
        /// <summary>
        /// ΢��Id
        /// </summary>
        /// <returns></returns>
        public string OpenId { get; set; }
        /// <summary>
        /// ΢���ǳ�
        /// </summary>
        /// <returns></returns>
        public string NickName { get; set; }
        /// <summary>
        /// ��ע
        /// </summary>
        /// <returns></returns>
        public string Description { get; set; }
        /// <summary>
        /// ��������
        /// </summary>
        /// <returns></returns>
        public DateTime? CreateTime { get; set; }
        #endregion

        #region ��չ����
        /// <summary>
        /// ��������
        /// </summary>
        public override void Create()
        {
            this.Id = Guid.NewGuid().ToString();
            CreateTime = DateTime.Now;
        }
        /// <summary>
        /// �༭����
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.Id = keyValue;
                                            }
        #endregion
    }
}