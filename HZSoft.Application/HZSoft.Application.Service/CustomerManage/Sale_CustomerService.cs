using HZSoft.Application.Code;
using HZSoft.Application.Entity.CustomerManage;
using HZSoft.Application.IService.CustomerManage;
using HZSoft.Data.Repository;
using HZSoft.Util;
using HZSoft.Util.Extension;
using HZSoft.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HZSoft.Application.Service.CustomerManage
{
    /// <summary>
    /// �� �� 6.1
    /// 
    /// �� ������������Ա
    /// �� �ڣ�2017-12-23 14:41
    /// �� ��������ͳ��
    /// </summary>
    public class Sale_CustomerService : RepositoryFactory, Sale_CustomerIService
    {
        #region ��ȡ����
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="pagination">��ҳ</param>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>���ط�ҳ�б�</returns>
        public IEnumerable<Sale_CustomerEntity> GetPageList(Pagination pagination, string queryJson)
        {
            var expression = LinqExtensions.True<Sale_CustomerEntity>();
            var queryParam = queryJson.ToJObject();
            string strSql = "select * from Sale_Customer where 1=1";
            //��������
            if (!queryParam["StartTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
            {
                DateTime startTime = queryParam["StartTime"].ToDate();
                DateTime endTime = queryParam["EndTime"].ToDate().AddDays(1);
                strSql += " and CreateTime >= '" + startTime + "' and CreateTime < '" + endTime + "'";
            }
            //�ͻ���˾��
            if (!queryParam["CustomerCompany"].IsEmpty())
            {
                string CustomerCompany = queryParam["CustomerCompany"].ToString();
                strSql += " and CustomerCompany = '" + CustomerCompany + "'";
            }

            //������
            if (!queryParam["CreateUserId"].IsEmpty())
            {
                string CreateUserId = queryParam["CreateUserId"].ToString();
                strSql += " and CreateUserId = '" + CreateUserId + "'";
            }
            else
            {
                if (!OperatorProvider.Provider.Current().IsSystem)
                {
                    string dataAutor = string.Format(OperatorProvider.Provider.Current().DataAuthorize.ReadAutorize, OperatorProvider.Provider.Current().UserId);
                    strSql += " and (CreateUserId in (" + dataAutor + "))";
                }
            }

            return this.BaseRepository().FindList<Sale_CustomerEntity>(strSql.ToString(), pagination);
        }
        /// <summary>
        /// ��ȡʵ��
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns></returns>
        public Sale_CustomerEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity<Sale_CustomerEntity>(keyValue);
        }
        /// <summary>
        /// ��ȡ�ӱ���ϸ��Ϣ
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns></returns>
        public IEnumerable<Sale_Customer_ItemEntity> GetDetails(string keyValue)
        {
            return this.BaseRepository().FindList<Sale_Customer_ItemEntity>("select * from Sale_Customer_Item where CustomerId='"+keyValue+ "'");        }
        #endregion

        #region �ύ����
        /// <summary>
        /// ɾ������
        /// </summary>
        /// <param name="keyValue">����</param>
        public void RemoveForm(string keyValue)
        {
            IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();
            try
            {
                db.Delete<Sale_CustomerEntity>(keyValue);
                db.Delete<Sale_Customer_ItemEntity>(t => t.CustomerId.Equals(keyValue));
                db.Commit();
            }
            catch (Exception)
            {
                db.Rollback();
                throw;
            }
        }
        /// <summary>
        /// ��������������޸ģ�
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <param name="entity">ʵ�����</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, Sale_CustomerEntity entity,List<Sale_Customer_ItemEntity> entryList)
        {
        IRepository db = this.BaseRepository().BeginTrans();
        try
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                //����
                entity.Modify(keyValue);
                db.Update(entity);
                //��ϸ
                db.Delete<Sale_Customer_ItemEntity>(t => t.CustomerId.Equals(keyValue));
                foreach (Sale_Customer_ItemEntity item in entryList)
                {
                    item.Create();
                    item.CustomerId = entity.CustomerId;
                    db.Insert(item);
                }
            }
            else
            {
                //����
                entity.Create();
                db.Insert(entity);
                //��ϸ
                foreach (Sale_Customer_ItemEntity item in entryList)
                {
                    item.Create();
                    item.CustomerId = entity.CustomerId;
                    db.Insert(item);
                }
            }
            db.Commit();
        }
        catch (Exception)
        {
            db.Rollback();
            throw;
        }
        }
        #endregion
    }
}
