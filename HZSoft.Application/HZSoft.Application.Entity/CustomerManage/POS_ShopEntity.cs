using System;
using HZSoft.Application.Code;

namespace HZSoft.Application.Entity.CustomerManage
{
    /// <summary>
    /// �� ��
    /// 
    /// �� ������������Ա
    /// �� �ڣ�2017-12-02 16:17
    /// �� ����POS_Shop
    /// </summary>
    public class POS_ShopEntity : BaseEntity
    {
        #region ʵ���Ա
        /// <summary>
        /// Ψһ��ʶ
        /// </summary>
        /// <returns></returns>
        public string Id { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        public string wxLon { get; set; }
        /// <summary>
        /// γ��
        /// </summary>
        /// <returns></returns>
        public string wxLat { get; set; }
        /// <summary>
        /// �ٶȾ���
        /// </summary>
        /// <returns></returns>
        public string bdLon { get; set; }
        /// <summary>
        /// �ٶ�γ��
        /// </summary>
        /// <returns></returns>
        public string bdLat { get; set; }
        /// <summary>
        /// ������
        /// </summary>
        /// <returns></returns>
        public string ShopName { get; set; }
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
        /// ���̵�ַ
        /// </summary>
        /// <returns></returns>
        public string ShopAddress { get; set; }
        /// <summary>
        /// ʡ��
        /// </summary>
        /// <returns></returns>
        public string Province { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        public string CityCode { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        public string City { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        public string District { get; set; }
        /// <summary>
        /// ������Ƭ
        /// </summary>
        /// <returns></returns>
        public string ShopPicture { get; set; }
        /// <summary>
        /// ��ϵ��
        /// </summary>
        /// <returns></returns>
        public string Contacts { get; set; }
        /// <summary>
        /// ��ϵ��ʽ
        /// </summary>
        /// <returns></returns>
        public string Telphone { get; set; }
        /// <summary>
        /// �Ƿ����ϰ�
        /// </summary>
        /// <returns></returns>
        public int? IsBoss { get; set; }
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
        /// ״̬
        /// </summary>
        /// <returns></returns>
        public int? State { get; set; }
        /// <summary>
        /// �ϴ���id
        /// </summary>
        /// <returns></returns>
        public string SellerId { get; set; }
        /// <summary>
        /// �ϴ�������
        /// </summary>
        /// <returns></returns>
        public string SellerName { get; set; }
        /// <summary>
        /// �ϴ���΢��ID
        /// </summary>
        /// <returns></returns>
        public string OpenId { get; set; }
        /// <summary>
        /// �ϴ���΢���ǳ�
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