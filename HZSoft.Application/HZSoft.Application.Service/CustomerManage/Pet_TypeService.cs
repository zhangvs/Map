using HZSoft.Application.Entity.BaseManage;
using HZSoft.Application.Entity.CustomerManage;
using HZSoft.Application.IService.BaseManage;
using HZSoft.Application.IService.CustomerManage;
using HZSoft.Data.Repository;
using HZSoft.Util;
using HZSoft.Util.Extension;
using HZSoft.Util.WebControl;
using System.Collections.Generic;
using System.Linq;

namespace HZSoft.Application.Service.CustomerManage
{
    /// <summary>
    /// �� �� 6.1
    /// 
    /// �� ������������Ա
    /// �� �ڣ�2017-12-18 13:59
    /// �� ������Ʒ����
    /// </summary>
    public class Pet_TypeService : RepositoryFactory<Pet_TypeEntity>, Pet_TypeIService
    {
        #region ��ȡ����
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="pagination">��ҳ</param>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>���ط�ҳ�б�</returns>
        public IEnumerable<Pet_TypeEntity> GetPageList(Pagination pagination, string queryJson)
        {
            var expression = LinqExtensions.True<Pet_TypeEntity>();
            var queryParam = queryJson.ToJObject();
            string strSql = "select * from Pet_Type where 1 = 1";

            //����id
            if (!queryParam["ItemId"].IsEmpty())
            {
                string ItemId = queryParam["ItemId"].ToString();
                strSql += " and ItemId = '" + ItemId + "'";
            }
            //������id
            if (!queryParam["ParentId"].IsEmpty())
            {
                string ParentId = queryParam["ParentId"].ToString();
                strSql += " and ParentId = '" + ParentId + "'";
            }
            else
            {
                strSql += " and ParentId <> '0'";
            }

            if (!queryParam["keyword"].IsEmpty())
            {
                string keyword = queryParam["keyword"].ToString();
                strSql += " and (ItemName like '%" + keyword + "%' or ItemCode like '%" + keyword + "%')";
            }

            return this.BaseRepository().FindList(strSql.ToString(), pagination);
        }
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>�����б�</returns>
        public IEnumerable<Pet_TypeEntity> GetList(string queryJson)
        {
            var expression = LinqExtensions.True<Pet_TypeEntity>();
            var queryParam = queryJson.ToJObject();
            string strSql = "select * from Pet_Type where 1 = 1";
            string strOrder = " ORDER BY sortcode asc";

            //����id
            if (!queryParam["ItemId"].IsEmpty())
            {
                string ItemId = queryParam["ItemId"].ToString();
                strSql += " and ItemId = '" + ItemId + "'";
            }
            //������id
            if (!queryParam["ParentId"].IsEmpty())
            {
                string ParentId = queryParam["ParentId"].ToString();
                strSql += " and ParentId = '" + ParentId + "'";
            }
            else
            {
                strSql += " and ParentId <> '0'";
            }

            if (!queryParam["keyword"].IsEmpty())
            {
                string keyword = queryParam["keyword"].ToString();
                strSql += " and (ItemName like '%" + keyword + "%' or ItemCode like '%" + keyword + "%')";
            }

            strSql += strOrder;
            return this.BaseRepository().FindList(strSql.ToString());
        }
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <returns>�����б�</returns>
        public IEnumerable<Pet_TypeEntity> GetList()
        {
            return this.BaseRepository().IQueryable().ToList();
        }
        /// <summary>
        /// ��ȡʵ��
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns></returns>
        public Pet_TypeEntity GetEntity(string keyValue)
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
        public void SaveForm(string keyValue, Pet_TypeEntity entity)
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

        #region ��֤����
        /// <summary>
        /// ��Ŀֵ�����ظ�
        /// </summary>
        /// <param name="itemValue">��Ŀֵ</param>
        /// <param name="keyValue">����</param>
        /// <param name="itemId">����Id</param>
        /// <returns></returns>
        public bool ExistItemValue(string itemValue, string keyValue, string itemId)
        {
            var expression = LinqExtensions.True<Pet_TypeEntity>();
            expression = expression.And(t => t.ItemCode == itemValue).And(t => t.ParentId == itemId);
            if (!string.IsNullOrEmpty(keyValue))
            {
                expression = expression.And(t => t.ItemId != keyValue);
            }
            return this.BaseRepository().IQueryable(expression).Count() == 0 ? true : false;
        }
        /// <summary>
        /// ��Ŀ�������ظ�
        /// </summary>
        /// <param name="itemName">��Ŀ��</param>
        /// <param name="keyValue">����</param>
        /// <param name="itemId">����Id</param>
        /// <returns></returns>
        public bool ExistItemName(string itemName, string keyValue, string itemId)
        {
            var expression = LinqExtensions.True<Pet_TypeEntity>();
            expression = expression.And(t => t.ItemName == itemName).And(t => t.ParentId == itemId);
            if (!string.IsNullOrEmpty(keyValue))
            {
                expression = expression.And(t => t.ItemId != keyValue);
            }
            return this.BaseRepository().IQueryable(expression).Count() == 0 ? true : false;
        }
        #endregion
    }
}
