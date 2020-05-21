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
    /// �� �ڣ�2017-12-12 10:37
    /// �� ������չĿ��
    /// </summary>
    public class POS_OfficeCompanyService : RepositoryFactory<POS_OfficeCompanyEntity>, POS_OfficeCompanyIService
    {
        #region ��ȡ����
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="pagination">��ҳ</param>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>���ط�ҳ�б�</returns>
        public IEnumerable<POS_OfficeCompanyEntity> GetPageList(Pagination pagination, string queryJson)
        {

            var expression = LinqExtensions.True<POS_OfficeCompanyEntity>();
            var queryParam = queryJson.ToJObject();

            //string strSql = "select * from POS_OfficeCompany where 1=1";
            string strSql = "SELECT company.id,companyname,companypicture,officeid,company.officename,Contacts,Floor,Room,Telphone,company.IsSee,company.State,company.CreateTime,company.NickName,company.SellerName"
                     + " FROM POS_OfficeCompany company "
                     + " LEFT JOIN POS_Office office  ON office.Id = company.OfficeId  where 1=1";

            //��������
            if (!queryParam["StartTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
            {
                DateTime startTime = queryParam["StartTime"].ToDate();
                DateTime endTime = queryParam["EndTime"].ToDate().AddDays(1);
                strSql += " and CreateTime >= '" + startTime + "' and CreateTime < '" + endTime + "'";
            }
            //д��¥ID
            if (!queryParam["OfficeId"].IsEmpty())
            {
                string OfficeId = queryParam["OfficeId"].ToString();
                strSql += " and OfficeId = '" + OfficeId + "'";
            }
            //д��¥��
            if (!queryParam["OfficeName"].IsEmpty())
            {
                string OfficeName = queryParam["OfficeName"].ToString();
                strSql += " and OfficeName = '" + OfficeName + "'";
            }
            //��˾��
            if (!queryParam["CompanyName"].IsEmpty())
            {
                string CompanyName = queryParam["CompanyName"].ToString();
                strSql += " and CompanyName = '" + CompanyName + "'";
            }
            //�ֻ���
            if (!queryParam["Telphone"].IsEmpty())
            {
                string Telphone = queryParam["Telphone"].ToString();
                strSql += " and Telphone = '" + Telphone + "'";
            }
            //��ϵ��
            if (!queryParam["Contacts"].IsEmpty())
            {
                string Contacts = queryParam["Contacts"].ToString();
                strSql += " and Contacts = '" + Contacts + "'";
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
                strSql += " and company.SellerId = '" + SellerId + "'";
            }
            else
            {
                if (!OperatorProvider.Provider.Current().IsSystem)
                {
                    //���ƽ�ɫȨ��
                    string strSqlSeller = "";
                    string dataAutor = string.Format(OperatorProvider.Provider.Current().DataAuthorize.ReadAutorize, OperatorProvider.Provider.Current().UserId);
                    strSqlSeller = " company.SellerId in (" + dataAutor + ") ";

                    //���Ƴ���Ȩ��
                    string des = OperatorProvider.Provider.Current().Description;
                    string strSqlArea = "";
                    if (!string.IsNullOrEmpty(des))
                    {
                        des = des.Replace("|", "','");
                        strSqlArea = " or (province in ('" + des + "') or city in ('" + des + "') or district in ('" + des + "')  and company.SellerId is null) ";
                    }
                    else
                    {
                        strSqlArea = " or company.SellerId is null ";
                    }
                    //���˽�ɫ��Χ�ڵģ�������or����Ȩ�޷�Χ�ڵģ��ϴ��˲�Ϊ�յģ������˱༭֮��ľͲ��������ڣ�
                    strSql += " and (" + strSqlSeller + strSqlArea + ")";
                }
            }


            return this.BaseRepository().FindList(strSql.ToString(), pagination);
        }
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>�����б�</returns>
        public IEnumerable<POS_OfficeCompanyEntity> GetList(string queryJson)
        {
            var expression = LinqExtensions.True<POS_OfficeEntity>();
            var queryParam = queryJson.ToJObject();
            string wxLon = "";
            string wxLat = "";
            string strSql = "";
            string strOrder = "";
            if (!queryParam["wxLon"].IsEmpty() && !queryParam["wxLat"].IsEmpty())
            {
                wxLon = queryParam["wxLon"].ToString();
                wxLat = queryParam["wxLat"].ToString();

                strSql = "SELECT TOP 50 company.id,companyname,companypicture,officeid,company.officename,Contacts,Floor,Room,Telphone,dbo.f_GetDistance(" + wxLon + "," + wxLat
                         + ",wxlon,wxlat) as Description FROM POS_OfficeCompany company"
                         + " LEFT JOIN POS_Office office  ON office.Id = company.OfficeId  where 1=1";
                strOrder = " order by dbo.f_GetDistance(" + wxLon + "," + wxLat + ",wxlon,wxlat) asc,convert(int,floor) desc";
            }
            else
            {
                strSql = "select * from POS_OfficeCompany where 1 = 1 ";
                strOrder = " ORDER BY CreateTime desc";
            }
            //��˾��
            if (!queryParam["SearchName"].IsEmpty())
            {
                string CompanyName = queryParam["SearchName"].ToString();
                strSql += " and CompanyName = '" + CompanyName + "'";
            }
            //д��¥ID
            if (!queryParam["OfficeId"].IsEmpty())
            {
                string OfficeId = queryParam["OfficeId"].ToString();
                strSql += " and OfficeId = '" + OfficeId + "'";
            }
            
            if (!OperatorProvider.Provider.Current().IsSystem)
            {
                //���ƽ�ɫȨ��
                string strSqlSeller = "";
                string dataAutor = string.Format(OperatorProvider.Provider.Current().DataAuthorize.ReadAutorize, OperatorProvider.Provider.Current().UserId);
                strSqlSeller = " company.SellerId in (" + dataAutor + ") ";

                //���Ƴ���Ȩ��
                string des = OperatorProvider.Provider.Current().Description;
                string strSqlArea = "";
                if (!string.IsNullOrEmpty(des))
                {
                    des = des.Replace("|", "','");
                    strSqlArea = " or (province in ('" + des + "') or city in ('" + des + "') or district in ('" + des + "')  and company.SellerId is null) ";
                }
                else
                {
                    strSqlArea = " or company.SellerId is null ";
                }
                //���˽�ɫ��Χ�ڵģ�������or����Ȩ�޷�Χ�ڵģ��ϴ��˲�Ϊ�յģ������˱༭֮��ľͲ��������ڣ�
                strSql += " and (" + strSqlSeller + strSqlArea + ")";
            }

            strSql += strOrder;

            return this.BaseRepository().FindList(strSql.ToString());
        }
        /// <summary>
        /// ��ȡʵ��
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns></returns>
        public POS_OfficeCompanyEntity GetEntity(string keyValue)
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
        public void SaveForm(string keyValue, POS_OfficeCompanyEntity entity)
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
