using HZSoft.Application.Code;
using HZSoft.Application.Entity.CustomerManage;
using HZSoft.Application.Entity.WeChatManage;
using HZSoft.Application.IService.CustomerManage;
using HZSoft.Application.IService.WeChatManage;
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
    /// �� �ڣ�2017-12-11 18:53
    /// �� ������չд��¥
    /// </summary>
    public class POS_OfficeService : RepositoryFactory<POS_OfficeEntity>, POS_OfficeIService
    {
        #region ��ȡ����
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="pagination">��ҳ</param>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>���ط�ҳ�б�</returns>
        public IEnumerable<POS_OfficeEntity> GetPageList(Pagination pagination, string queryJson)
        {

            var expression = LinqExtensions.True<POS_OfficeEntity>();
            var queryParam = queryJson.ToJObject();
            string strSql = "select * from POS_Office where 1=1";
            //��������
            if (!queryParam["StartTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
            {
                DateTime startTime = queryParam["StartTime"].ToDate();
                DateTime endTime = queryParam["EndTime"].ToDate().AddDays(1);
                strSql += " and CreateTime >= '" + startTime + "' and CreateTime < '" + endTime + "'";
            }
            //����ID
            if (!queryParam["Id"].IsEmpty())
            {
                string Id = queryParam["Id"].ToString();
                strSql += " and Id = '" + Id + "'";
            }
            //������
            if (!queryParam["OfficeName"].IsEmpty())
            {
                string OfficeName = queryParam["OfficeName"].ToString();
                strSql += " and OfficeName = '" + OfficeName + "'";
            }
            //״̬
            if (!queryParam["State"].IsEmpty())
            {
                string State = queryParam["State"].ToString();
                strSql += " and State = '" + State + "'";
            }

            //������
            if (!queryParam["SellerId"].IsEmpty())
            {
                string SellerId = queryParam["SellerId"].ToString();
                strSql += " and SellerId = '" + SellerId + "'";
            }
            else
            {
                if (!OperatorProvider.Provider.Current().IsSystem)
                {
                    //���Ƴ���Ȩ��
                    string des = OperatorProvider.Provider.Current().Description;
                    if (!string.IsNullOrEmpty(des))
                    {
                        des = des.Replace("|", "','");
                        strSql += " and (province in ('" + des + "') or city in ('" + des + "') or district in ('" + des + "')) ";
                    }
                }
            }

            return this.BaseRepository().FindList(strSql.ToString(), pagination);
        }
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>�����б�</returns>
        public IEnumerable<POS_OfficeEntity> GetList(string queryJson)
        {
            var expression = LinqExtensions.True<POS_OfficeEntity>();
            var queryParam = queryJson.ToJObject();
            string wxLon = "";
            string wxLat = "";
            string strSql = "";
            string strOrder = "";
            if (!queryParam["wxLon"].IsEmpty()&& !queryParam["wxLat"].IsEmpty())
            {
                wxLon = queryParam["wxLon"].ToString();
                wxLat = queryParam["wxLat"].ToString();
                strSql = "SELECT TOP 50 id,wxlon,wxlat,officename,officepicture,OfficeAddress,dbo.f_GetDistance(" + wxLon + "," + wxLat + ",wxlon,wxlat) as Description FROM POS_Office where 1=1";
                strOrder= " order by dbo.f_GetDistance(" + wxLon + "," + wxLat + ",wxlon,wxlat) asc";
            }
            else
            {
                strSql = "select * from POS_Office where 1 = 1 ";
                strOrder = " ORDER BY CreateTime desc";
            }

            //������
            if (!queryParam["SearchName"].IsEmpty())
            {
                string OfficeName = queryParam["SearchName"].ToString();
                strSql += " and OfficeName = '" + OfficeName + "'";
            }
            
            //������
            if (!OperatorProvider.Provider.Current().IsSystem)
            {
                //���Ƴ���Ȩ��
                string des = OperatorProvider.Provider.Current().Description;
                if (!string.IsNullOrEmpty(des))
                {
                    des = des.Replace("|", "','");
                    strSql += " and (province in ('" + des + "') or city in ('" + des + "') or district in ('" + des + "')) ";
                }
            }

            strSql += strOrder;

            return this.BaseRepository().FindList(strSql.ToString());
        }
        /// <summary>
        /// ��ȡʵ��
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns></returns>
        public POS_OfficeEntity GetEntity(string keyValue)
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
        public void SaveForm(string keyValue, POS_OfficeEntity entity)
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
