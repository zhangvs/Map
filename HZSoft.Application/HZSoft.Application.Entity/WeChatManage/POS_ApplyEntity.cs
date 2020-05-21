using System;
using HZSoft.Application.Code;

namespace HZSoft.Application.Entity.WeChatManage
{
    /// <summary>
    /// �� ��
    /// 
    /// �� ������������Ա
    /// �� �ڣ�2017-11-24 16:45
    /// �� ��������POS��
    /// </summary>
    public class POS_ApplyEntity : BaseEntity
    {
        #region ʵ���Ա
        /// <summary>
        /// ���
        /// </summary>
        /// <returns></returns>
        public string ApplyID { get; set; }
        /// <summary>
        /// �ֻ���
        /// </summary>
        /// <returns></returns>
        public string Telphone { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        public string FullName { get; set; }
        /// <summary>
        /// ���֤��
        /// </summary>
        /// <returns></returns>
        public string IDCode { get; set; }
        /// <summary>
        /// ���֤��Ч��
        /// </summary>
        /// <returns></returns>
        public string IDDate { get; set; }
        /// <summary>
        /// ��ַ
        /// </summary>
        /// <returns></returns>
        public string IDAddress { get; set; }
        /// <summary>
        /// ���֤������
        /// </summary>
        /// <returns></returns>
        public string ID_Z { get; set; }
        /// <summary>
        /// ���֤������
        /// </summary>
        /// <returns></returns>
        public string ID_B { get; set; }
        /// <summary>
        /// ���ÿ�����
        /// </summary>
        /// <returns></returns>
        public string Credit_Z { get; set; }
        /// <summary>
        /// ���ÿ�����
        /// </summary>
        /// <returns></returns>
        public string Credit_B { get; set; }
        /// <summary>
        /// ���ÿ���
        /// </summary>
        /// <returns></returns>
        public string CreditCode { get; set; }
        /// <summary>
        /// ���ÿ�������
        /// </summary>
        /// <returns></returns>
        public string CreditBankName { get; set; }
        /// <summary>
        /// ���ÿ�������
        /// </summary>
        /// <returns></returns>
        public string ValidThru { get; set; }
        /// <summary>
        /// ���ÿ�����
        /// </summary>
        /// <returns></returns>
        public string Credit_Z1 { get; set; }
        /// <summary>
        /// ���ÿ�����
        /// </summary>
        /// <returns></returns>
        public string Credit_B1 { get; set; }
        /// <summary>
        /// ���ÿ���
        /// </summary>
        /// <returns></returns>
        public string CreditCode1 { get; set; }
        /// <summary>
        /// ���ÿ�������
        /// </summary>
        /// <returns></returns>
        public string CreditBankName1 { get; set; }
        /// <summary>
        /// ���ÿ�������
        /// </summary>
        /// <returns></returns>
        public string ValidThru1 { get; set; }
        /// <summary>
        /// ���ÿ�����
        /// </summary>
        /// <returns></returns>
        public string Credit_Z2 { get; set; }
        /// <summary>
        /// ���ÿ�����
        /// </summary>
        /// <returns></returns>
        public string Credit_B2 { get; set; }
        /// <summary>
        /// ���ÿ���
        /// </summary>
        /// <returns></returns>
        public string CreditCode2 { get; set; }
        /// <summary>
        /// ���ÿ�������
        /// </summary>
        /// <returns></returns>
        public string CreditBankName2 { get; set; }
        /// <summary>
        /// ���ÿ�������
        /// </summary>
        /// <returns></returns>
        public string ValidThru2 { get; set; }
        /// <summary>
        /// ���ÿ�����
        /// </summary>
        /// <returns></returns>
        public string Credit_Z3 { get; set; }
        /// <summary>
        /// ���ÿ�����
        /// </summary>
        /// <returns></returns>
        public string Credit_B3 { get; set; }
        /// <summary>
        /// ���ÿ���
        /// </summary>
        /// <returns></returns>
        public string CreditCode3 { get; set; }
        /// <summary>
        /// ���ÿ�������
        /// </summary>
        /// <returns></returns>
        public string CreditBankName3 { get; set; }
        /// <summary>
        /// ���ÿ�������
        /// </summary>
        /// <returns></returns>
        public string ValidThru3 { get; set; }
        /// <summary>
        /// ���ÿ�����
        /// </summary>
        /// <returns></returns>
        public string Credit_Z4 { get; set; }
        /// <summary>
        /// ���ÿ�����
        /// </summary>
        /// <returns></returns>
        public string Credit_B4 { get; set; }
        /// <summary>
        /// ���ÿ���
        /// </summary>
        /// <returns></returns>
        public string CreditCode4 { get; set; }
        /// <summary>
        /// ���ÿ�������
        /// </summary>
        /// <returns></returns>
        public string CreditBankName4 { get; set; }
        /// <summary>
        /// ���ÿ�������
        /// </summary>
        /// <returns></returns>
        public string ValidThru4 { get; set; }

        /// <summary>
        /// �Ƽ���
        /// </summary>
        /// <returns></returns>
        public string RecommenderId { get; set; }
        /// <summary>
        /// �Ƽ���
        /// </summary>
        /// <returns></returns>
        public string Recommender { get; set; }
        /// <summary>
        /// ͨ����ʶ
        /// </summary>
        /// <returns></returns>
        public int? PassMark { get; set; }
        /// <summary>
        /// ����ʱ��
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
            this.ApplyID = Guid.NewGuid().ToString();
                                            }
        /// <summary>
        /// �༭����
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.ApplyID = keyValue;
                                            }
        #endregion
    }
}