using System;
using HZSoft.Application.Code;

namespace HZSoft.Application.Entity.CustomerManage
{
    /// <summary>
    /// �� ��
    /// 
    /// �� ������������Ա
    /// �� �ڣ�2020-05-21 14:42
    /// �� ��������
    /// </summary>
    public class PetEntity : BaseEntity
    {
        #region ʵ���Ա
        /// <summary>
        /// Id
        /// </summary>
        /// <returns></returns>
        public int? Id { get; set; }
        /// <summary>
        /// ��������
        /// </summary>
        /// <returns></returns>
        public string Name { get; set; }
        /// <summary>
        /// ����Id
        /// </summary>
        /// <returns></returns>
        public string MasterId { get; set; }
        /// <summary>
        /// ��������
        /// </summary>
        /// <returns></returns>
        public string MasterName { get; set; }
        /// <summary>
        /// Ʒ��id
        /// </summary>
        /// <returns></returns>
        public string ClassId { get; set; }
        /// <summary>
        /// Ʒ��
        /// </summary>
        /// <returns></returns>
        public string ClassName { get; set; }
        /// <summary>
        /// ����Ʒ��id
        /// </summary>
        /// <returns></returns>
        public string SubClassId { get; set; }
        /// <summary>
        /// ����Ʒ��
        /// </summary>
        /// <returns></returns>
        public string SubClassName { get; set; }
        /// <summary>
        /// �Ա�
        /// </summary>
        /// <returns></returns>
        public int? Gender { get; set; }
        /// <summary>
        /// ͷ��
        /// </summary>
        /// <returns></returns>
        public string Photo { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        public DateTime? Birthday { get; set; }
        /// <summary>
        /// ����ʱ��
        /// </summary>
        /// <returns></returns>
        public DateTime? HomeDate { get; set; }
        /// <summary>
        /// ����״̬
        /// </summary>
        /// <returns></returns>
        public int? Sterilized { get; set; }
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
            //this.Id = Guid.NewGuid().ToString();
            this.CreateDate = DateTime.Now;
            this.CreateUserId = OperatorProvider.Provider.Current().UserId;
            this.CreateUserName = OperatorProvider.Provider.Current().UserName;
        }
        /// <summary>
        /// �༭����
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(int? keyValue)
        {
            this.Id = keyValue;
            this.ModifyDate = DateTime.Now;
            this.ModifyUserId = OperatorProvider.Provider.Current().UserId;
            this.ModifyUserName = OperatorProvider.Provider.Current().UserName;
        }
        #endregion
    }
}