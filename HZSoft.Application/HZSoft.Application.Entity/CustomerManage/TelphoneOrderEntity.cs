using System;
using HZSoft.Application.Code;

namespace HZSoft.Application.Entity.CustomerManage
{
    /// <summary>
    /// �� ��
    /// 
    /// �� ������������Ա
    /// �� �ڣ�2017-09-22 15:43
    /// �� �������붩��
    /// </summary>
    public class TelphoneOrderEntity : BaseEntity
    {
        #region ʵ���Ա
        /// <summary>
        /// ID
        /// </summary>
        /// <returns></returns>
        public string ID { get; set; }
        /// <summary>
        /// �������
        /// </summary>
        /// <returns></returns>
        public string OrderCode { get; set; }
        /// <summary>
        /// �۳�����
        /// </summary>
        /// <returns></returns>
        public string Telphone { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        public string SellerId { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        public string SellerName { get; set; }
        /// <summary>
        /// �۳����
        /// </summary>
        /// <returns></returns>
        public decimal? Amount { get; set; }
        /// <summary>
        /// ���ͷ�ʽ
        /// </summary>
        /// <returns></returns>
        public string Express { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        public string Numbers { get; set; }
        /// <summary>
        /// Ԥ����
        /// </summary>
        /// <returns></returns>
        public decimal? Paid { get; set; }
        /// <summary>
        /// Ԥ��������
        /// </summary>
        /// <returns></returns>
        public string PaidDate { get; set; }
        /// <summary>
        /// β��/ȫ��
        /// </summary>
        /// <returns></returns>
        public decimal? ToPay { get; set; }
        /// <summary>
        /// β��/ȫ������
        /// </summary>
        /// <returns></returns>
        public string ToPayDate { get; set; }
        /// <summary>
        /// ���������
        /// </summary>
        /// <returns></returns>
        public decimal? ToPayCharge { get; set; }
        /// <summary>
        /// ֧����ʽ
        /// </summary>
        /// <returns></returns>
        public string PayMode { get; set; }
        /// <summary>
        /// �˺�
        /// </summary>
        /// <returns></returns>
        public string Account { get; set; }
        /// <summary>
        /// ת��˾�˻�
        /// </summary>
        /// <returns></returns>
        public int? Transferred { get; set; }
        /// <summary>
        /// �ջ���
        /// </summary>
        /// <returns></returns>
        public string Consignee { get; set; }
        /// <summary>
        /// ��ϵ��ʽ
        /// </summary>
        /// <returns></returns>
        public string Contact { get; set; }
        /// <summary>
        /// ��ַ
        /// </summary>
        /// <returns></returns>
        public string Address { get; set; }
        /// <summary>
        /// �Ƿ�ǩ��
        /// </summary>
        /// <returns></returns>
        public int? Sign { get; set; }
        /// <summary>
        /// �Ƿ񼤻�
        /// </summary>
        /// <returns></returns>
        public int? ActivationMark { get; set; }
        /// <summary>
        /// ɾ�����
        /// </summary>
        /// <returns></returns>
        public int? DeleteMark { get; set; }
        /// <summary>
        /// ��Ч��־
        /// </summary>
        /// <returns></returns>
        public int? EnabledMark { get; set; }
        /// <summary>
        /// ��ע
        /// </summary>
        /// <returns></returns>
        public string Description { get; set; }
        /// <summary>
        /// ��������
        /// </summary>
        /// <returns></returns>
        public DateTime? CreateDate { get; set; }
        /// <summary>
        /// �����û�����
        /// </summary>
        /// <returns></returns>
        public string CreateUserId { get; set; }
        /// <summary>
        /// �����û�
        /// </summary>
        /// <returns></returns>
        public string CreateUserName { get; set; }
        /// <summary>
        /// �޸�����
        /// </summary>
        /// <returns></returns>
        public DateTime? ModifyDate { get; set; }
        /// <summary>
        /// �޸��û�����
        /// </summary>
        /// <returns></returns>
        public string ModifyUserId { get; set; }
        /// <summary>
        /// �޸��û�
        /// </summary>
        /// <returns></returns>
        public string ModifyUserName { get; set; }
        #endregion

        #region ��չ����
        /// <summary>
        /// ��������
        /// </summary>
        public override void Create()
        {
            this.ID = Guid.NewGuid().ToString();
            this.CreateDate = DateTime.Now;
            this.CreateUserId = OperatorProvider.Provider.Current().UserId;
            this.CreateUserName = OperatorProvider.Provider.Current().UserName;
        }
        /// <summary>
        /// �༭����
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.ID = keyValue;
            this.ModifyDate = DateTime.Now;
            this.ModifyUserId = OperatorProvider.Provider.Current().UserId;
            this.ModifyUserName = OperatorProvider.Provider.Current().UserName;
        }
        #endregion
    }
}