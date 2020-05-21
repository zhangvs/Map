using System;
using HZSoft.Application.Code;

namespace HZSoft.Application.Entity.CustomerManage
{
    /// <summary>
    /// �� ��
    /// 
    /// �� ������������Ա
    /// �� �ڣ�2017-12-11 18:53
    /// �� ������չд��¥
    /// </summary>
    public class POS_OfficeEntity : BaseEntity
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
        /// ��ַ
        /// </summary>
        /// <returns></returns>
        public string OfficeAddress { get; set; }
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
        /// д��¥��
        /// </summary>
        /// <returns></returns>
        public string OfficeName { get; set; }
        /// <summary>
        /// д��¥��Ƭ
        /// </summary>
        /// <returns></returns>
        public string OfficePicture { get; set; }
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
        /// �ϴ���Id
        /// </summary>
        /// <returns></returns>
        public string SellerId { get; set; }
        /// <summary>
        /// �ϴ�������
        /// </summary>
        /// <returns></returns>
        public string SellerName { get; set; }
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