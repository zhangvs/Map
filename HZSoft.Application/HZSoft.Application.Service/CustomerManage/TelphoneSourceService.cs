using HZSoft.Application.Code;
using HZSoft.Application.Entity.CustomerManage;
using HZSoft.Application.IService.CustomerManage;
using HZSoft.Data.Repository;
using HZSoft.Util;
using HZSoft.Util.Extension;
using HZSoft.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;

namespace HZSoft.Application.Service.CustomerManage
{
    /// <summary>
    /// �� �� 6.1
    /// 
    /// �� ������������Ա
    /// �� �ڣ�2017-09-16 16:41
    /// �� ���������
    /// </summary>
    public class TelphoneSourceService : RepositoryFactory<TelphoneSourceEntity>, TelphoneSourceIService
    {
        #region ��ȡ����
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="pagination">��ҳ</param>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>���ط�ҳ�б�</returns>
        public IEnumerable<TelphoneSourceEntity> GetPageList(Pagination pagination, string queryJson)
        {
            var expression = LinqExtensions.True<TelphoneSourceEntity>();
            var queryParam = queryJson.ToJObject();
            string strSql = "select * from TelphoneSource where DeleteMark <> 1 and EnabledMark <> 1";
            //��������
            if (!queryParam["StartTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
            {
                DateTime startTime = queryParam["StartTime"].ToDate();
                DateTime endTime = queryParam["EndTime"].ToDate().AddDays(1);
                //expression = expression.And(t => t.ModifyDate >= startTime && t.ModifyDate <= endTime);
                strSql += " and ModifyDate >= '" + startTime + "' and ModifyDate < '" + endTime + "'";
            }
            //���ݱ��
            if (!queryParam["Telphone"].IsEmpty())
            {
                string Telphone = queryParam["Telphone"].ToString();
                //expression = expression.And(t => t.Telphone.Contains(Telphone));
                strSql += " and Telphone = '"+Telphone+"'";
            }
            //������
            if (!queryParam["SellerId"].IsEmpty())
            {
                string SellerId = queryParam["SellerId"].ToString();
                //expression = expression.And(t => t.SellerId.Contains(SellerId));
                strSql += " and SellerId = '" + SellerId+"'";
            }

            //�۳�״̬
            if (!queryParam["SellMark"].IsEmpty())
            {
                int SellMark = queryParam["SellMark"].ToInt();
                //expression = expression.And(t => t.SellMark==SellMark);
                strSql += " and SellMark = " + SellMark;
            }
            if (!OperatorProvider.Provider.Current().IsSystem)
            {
                string dataAutor = string.Format(OperatorProvider.Provider.Current().DataAuthorize.ReadAutorize, OperatorProvider.Provider.Current().UserId);
                strSql += " and OrganizeId IN( select OrganizeId from Base_User where 1=1";
                strSql += " and UserId in (" + dataAutor + ") GROUP BY OrganizeId )";
            }
            return this.BaseRepository().FindList(strSql.ToString(), pagination);
            //return this.BaseRepository().FindList(expression, pagination);
        }
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="telphone">�Զ�ƥ����ֻ���</param>
        /// <returns>�����б�</returns>
        public IEnumerable<TelphoneSourceEntity> GetList(string telphone)
        {
            string strSql = "SELECT TOP(10) Telphone FROM TelphoneSource WHERE SellMark<>1 AND DeleteMark<>1 and EnabledMark <> 1 and Telphone like '%" + telphone + "%' ";
            return this.BaseRepository().FindList(strSql.ToString());
            // this.BaseRepository().IQueryable().ToList();
        }
        /// <summary>
        /// ��ȡʵ��
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns></returns>
        public TelphoneSourceEntity GetEntity(int? keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }
        /// <summary>
        /// ��ȡʵ��
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns></returns>
        public TelphoneSourceEntity GetEntity(string telphone)
        {
            IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();
            TelphoneSourceEntity entity = db.FindEntity<TelphoneSourceEntity>(t => t.Telphone == telphone);
            return entity;
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
        public void SaveForm(int? keyValue, TelphoneSourceEntity entity)
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
        /// <summary>
        /// ��ȡ����
        /// </summary>
        /// <returns></returns>
        public int GetTelphone()
        {
            //1.�Ȳ鿴��Ա�������Ƿ��Ѿ���ȡ��(�Ƿ����10)
            var expression = LinqExtensions.True<TelphoneSourceEntity>();
            string userid = OperatorProvider.Provider.Current().UserId;
            string username= OperatorProvider.Provider.Current().UserName;
            string strSql = "SELECT COUNT(*) FROM TelphoneSource WHERE 1=1 AND SellerId ='"+ userid + "' AND   datediff(day,[ModifyDate],getdate())=0";
            DataTable dt= this.BaseRepository().FindTable(strSql.ToString());
            int ges = int.Parse(dt.Rows[0][0].ToString());
            if (ges<10)
            {
                //2.û��ȡ���������ȡ10������������ǰԱ��
                //��ȡһ�����ݿ���û�з���ĺ��룬
                string strSql1 = "SELECT * FROM TelphoneSource WHERE 1=1 AND SellerId IS NULL ";
                DataTable dts = this.BaseRepository().FindTable(strSql1.ToString());
                Random rd = new Random();
                List<int> gint=new List<int>();
                if (dts.Rows.Count<20)
                {
                    for (int i = 0; i < 20; i++)
                    {
                        //�����ȡһ����
                        int dd = rd.Next(dts.Rows.Count);
                        //�жϵ�ǰ���Ƿ��ù�
                        if (gint.Contains(dd))
                        {
                            i--;
                            continue;
                        }
                        else
                        {
                            //���ظ����޸ĵ�ǰʵ�壬���з���
                            gint.Add(dd);
                            DataRow dtr = dts.Rows[dd];
                            int keyValue = int.Parse(dtr["TelphoneID"].ToString());
                            TelphoneSourceEntity oldEntity = this.BaseRepository().FindEntity(keyValue);
                            oldEntity.SellerId = userid;
                            oldEntity.SellerName = username;
                            oldEntity.SellMark = 1;
                            oldEntity.Modify(keyValue);
                            this.BaseRepository().Update(oldEntity);
                        }

                    }
                    return 1;
                }
                else
                {
                    return 0;
                }

            }
            else
            {
                return 2;
            }
        }
        #endregion
    }
}
