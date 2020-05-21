using HZSoft.Application.Code;
using HZSoft.Application.Entity.WeChatManage;
using HZSoft.Application.IService.WeChatManage;
using HZSoft.Data.Repository;
using HZSoft.Util;
using HZSoft.Util.Extension;
using HZSoft.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HZSoft.Application.Service.WeChatManage
{
    /// <summary>
    /// �� �� 6.1
    /// 
    /// �� ������������Ա
    /// �� �ڣ�2017-11-24 16:45
    /// �� ��������POS��
    /// </summary>
    public class POS_ApplyService : RepositoryFactory<POS_ApplyEntity>, POS_ApplyIService
    {
        #region ��ȡ����
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="pagination">��ҳ</param>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>���ط�ҳ�б�</returns>
        public IEnumerable<POS_ApplyEntity> GetPageList(Pagination pagination, string queryJson)
        {
            var expression = LinqExtensions.True<POS_ApplyEntity>();
            var queryParam = queryJson.ToJObject();
            string strSql = "select * from POS_Apply where 1=1";
            //��������
            if (!queryParam["StartTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
            {
                DateTime startTime = queryParam["StartTime"].ToDate();
                DateTime endTime = queryParam["EndTime"].ToDate().AddDays(1);
                strSql += " and CreateTime >= '" + startTime + "' and CreateTime < '" + endTime + "'";
            }
            //�ֻ���
            if (!queryParam["Telphone"].IsEmpty())
            {
                string Telphone = queryParam["Telphone"].ToString();
                strSql += " and Telphone = '" + Telphone + "'";
            }
            //����
            if (!queryParam["FullName"].IsEmpty())
            {
                string FullName = queryParam["FullName"].ToString();
                strSql += " and FullName = '" + FullName + "'";
            }
            //�Ƽ���
            if (!queryParam["Recommender"].IsEmpty())
            {
                string Recommender = queryParam["Recommender"].ToString();
                strSql += " and Recommender = '" + Recommender + "'";
            }
            else
            {
                if (!OperatorProvider.Provider.Current().IsSystem && OperatorProvider.Provider.Current().Account!="pos")
                {
                    string dataAutor = string.Format(OperatorProvider.Provider.Current().DataAuthorize.ReadAutorize, OperatorProvider.Provider.Current().UserId);
                    dataAutor=dataAutor.Replace("select UserId from", "select RealName from");
                    strSql += " and Recommender in (" + dataAutor + ")";
                }
            }

            return this.BaseRepository().FindList(strSql.ToString(), pagination);
        }
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>�����б�</returns>
        public IEnumerable<POS_ApplyEntity> GetList(string queryJson)
        {
            return this.BaseRepository().IQueryable().ToList();
        }
        /// <summary>
        /// ��ȡʵ��
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns></returns>
        public POS_ApplyEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }
        #endregion

        #region �ύ����
        /// <summary>
        /// ɾ������
        /// </summary>
        /// <param name="keyValue">����</param>
        public void RemoveForm(string keyValue)
        {
            this.BaseRepository().Delete(keyValue);
        }
        /// <summary>
        /// ��������������޸ģ�
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <param name="entity">ʵ�����</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, POS_ApplyEntity entity)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                entity.Modify(keyValue);
                this.BaseRepository().Update(entity);
            }
            else
            {
                entity.Create();
                this.BaseRepository().Insert(entity);
            }
        }
        #endregion
    }
}
