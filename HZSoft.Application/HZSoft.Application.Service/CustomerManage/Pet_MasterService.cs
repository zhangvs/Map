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
    /// �� �ڣ�2020-05-23 10:50
    /// �� ��������
    /// </summary>
    public class Pet_MasterService : RepositoryFactory<Pet_MasterEntity>, Pet_MasterIService
    {
        #region ��ȡ����
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="pagination">��ҳ</param>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>���ط�ҳ�б�</returns>
        public IEnumerable<Pet_MasterEntity> GetPageList(Pagination pagination, string queryJson)
        {
            var expression = LinqExtensions.True<Pet_MasterEntity>();
            var queryParam = queryJson.ToJObject();
            string strSql = $"select * from Pet_Master where 1=1 ";
            //��������
            if (!queryParam["StartTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
            {
                DateTime startTime = queryParam["StartTime"].ToDate();
                DateTime endTime = queryParam["EndTime"].ToDate().AddDays(1);
                strSql += " and ModifyDate >= '" + startTime + "' and ModifyDate < '" + endTime + "'";
            }
            //��Ȧ
            if (!queryParam["Name"].IsEmpty())
            {
                string Name = queryParam["Name"].ToString();
                strSql += " and Name  like '%" + Name + "%' ";
            }
            //����
            if (!queryParam["District"].IsEmpty())
            {
                string District = queryParam["District"].ToString();
                District = District.Replace("[", "").Replace("]", "").Replace("\"", "'");
                strSql += " and District in(" + District + ")";
            }
            //POI�ֲ�
            if (!queryParam["TypeName"].IsEmpty())
            {
                string TypeName = queryParam["TypeName"].ToString();
                TypeName = TypeName.Replace("[", "").Replace("]", "").Replace("\"", "'");
                strSql += " and TypeName in(" + TypeName + ")";
            }
            return this.BaseRepository().FindList(strSql.ToString(), pagination);
        }
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>�����б�</returns>
        public IEnumerable<Pet_MasterEntity> GetList(string queryJson)
        {
            var queryParam = queryJson.ToJObject();
            //�������
            string strOrder = "";
            string strSql = "";
            //������΢������Ļ�
            if (!queryParam["wxLon"].IsEmpty() && !queryParam["wxLat"].IsEmpty())
            {
                string wxLon = queryParam["wxLon"].ToString();
                string wxLat = queryParam["wxLat"].ToString();
                strSql = "select top 100 id,wxlon,wxlat,name,address,petsname,dbo.f_GetDistance(" + wxLon + "," + wxLat + ",wxlon,wxlat) as description,picture FROM Pet_Master t where 1=1";
                strOrder = " order by dbo.f_GetDistance(" + wxLon + "," + wxLat + ",wxlon,wxlat) asc";
            }
            else
            {
                strSql = "select top 100 id,wxlon,wxlat,name,address,petsname from Pet_Master where 1 = 1 ";
                strOrder = " ORDER BY CreateDate desc";
            }
            //������
            if (!queryParam["SearchName"].IsEmpty())
            {
                string SearchName = queryParam["SearchName"].ToString();
                strSql += " and name = '" + SearchName + "'";
            }
            strSql += strOrder;

            return this.BaseRepository().FindList(strSql.ToString());
        }
        /// <summary>
        /// ��ȡʵ��
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns></returns>
        public Pet_MasterEntity GetEntity(int? keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }
        #endregion

        #region �ύ����
        /// <summary>
        /// ɾ������
        /// </summary>
        /// <param name="keyValue">����</param>
        public void RemoveForm(int? keyValue)
        {
            this.BaseRepository().Delete(keyValue);
        }
        /// <summary>
        /// ��������������޸ģ�
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <param name="entity">ʵ�����</param>
        /// <returns></returns>
        public void SaveForm(int? keyValue, Pet_MasterEntity entity)
        {
            if (keyValue!=null)
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
