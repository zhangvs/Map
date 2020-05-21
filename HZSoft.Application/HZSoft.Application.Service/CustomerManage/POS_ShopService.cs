using HZSoft.Application.Code;
using HZSoft.Application.Entity.CustomerManage;
using HZSoft.Application.Entity.WeChatManage;
using HZSoft.Application.IService.CustomerManage;
using HZSoft.Application.IService.WeChatManage;
using HZSoft.Data.Repository;
using HZSoft.Util;
using HZSoft.Util.Extension;
using HZSoft.Util.WebControl;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace HZSoft.Application.Service.CustomerManage
{
    /// <summary>
    /// �� �� 6.1
    /// 
    /// �� ������������Ա
    /// �� �ڣ�2017-12-02 16:17
    /// �� ����POS_Shop
    /// </summary>
    public class POS_ShopService : RepositoryFactory<POS_ShopEntity>, POS_ShopIService
    {
        #region ��ȡ����
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="pagination">��ҳ</param>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>���ط�ҳ�б�</returns>
        public IEnumerable<POS_ShopEntity> GetPageList(Pagination pagination, string queryJson)
        {
            var expression = LinqExtensions.True<POS_ShopEntity>();
            var queryParam = queryJson.ToJObject();
            string strSql = "select * from POS_Shop where 1=1";
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
            //��ϵ��
            if (!queryParam["Contacts"].IsEmpty())
            {
                string Contacts = queryParam["Contacts"].ToString();
                strSql += " and Contacts = '" + Contacts + "'";
            }
            //����ID
            if (!queryParam["Id"].IsEmpty())
            {
                string Id = queryParam["Id"].ToString();
                strSql += " and Id = '" + Id + "'";
            }
            //������
            if (!queryParam["ShopName"].IsEmpty())
            {
                string ShopName = queryParam["ShopName"].ToString();
                strSql += " and ShopName = '" + ShopName + "'";
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
                    //���ƽ�ɫȨ��
                    string strSqlSeller = "";
                    string dataAutor = string.Format(OperatorProvider.Provider.Current().DataAuthorize.ReadAutorize, OperatorProvider.Provider.Current().UserId);
                    strSqlSeller = " SellerId in (" + dataAutor + ") ";

                    //���Ƴ���Ȩ��
                    string des = OperatorProvider.Provider.Current().Description;
                    string strSqlArea = "";
                    if (!string.IsNullOrEmpty(des))
                    {
                        des = des.Replace("|", "','");
                        strSqlArea = " or (province in ('" + des + "') or city in ('" + des + "') or district in ('" + des + "')  and SellerId is null) ";
                    }
                    else
                    {
                        strSqlArea = " or SellerId is null ";
                    }
                   //���˽�ɫ��Χ�ڵģ�������or����Ȩ�޷�Χ�ڵģ��ϴ��˲�Ϊ�յģ������˱༭֮��ľͲ��������ڣ�
                    strSql += " and ("+ strSqlSeller + strSqlArea + ")";
                }
            }




            return this.BaseRepository().FindList(strSql.ToString(), pagination);
        }
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>�����б�</returns>
        public IEnumerable<POS_ShopEntity> GetList(string queryJson)
        {
            var expression = LinqExtensions.True<POS_ShopEntity>();
            var queryParam = queryJson.ToJObject();
            string wxLon = "";
            string wxLat = "";
            string strSql = "";
            string strOrder = "";
            if (!queryParam["wxLon"].IsEmpty() && !queryParam["wxLat"].IsEmpty())
            {
                wxLon = queryParam["wxLon"].ToString();
                wxLat = queryParam["wxLat"].ToString();
                strSql = "SELECT TOP 50 id,shopname,ShopAddress,shoppicture,contacts,telphone,wxLon,wxLat,dbo.f_GetDistance(" + wxLon + "," + wxLat + ",wxlon,wxlat) as Description FROM POS_Shop where 1=1 ";
                strOrder = " order by dbo.f_GetDistance(" + wxLon + "," + wxLat + ",wxlon,wxlat) asc";
            }
            else
            {
                strSql = "select * from POS_Shop where 1 = 1 ";
                strOrder = " ORDER BY CreateTime desc";
            }

            //������
            if (!queryParam["SearchName"].IsEmpty())
            {
                string ShopName = queryParam["SearchName"].ToString();
                strSql += " and ShopName = '" + ShopName + "'";
            }
            if (!OperatorProvider.Provider.Current().IsSystem)
            {
                //���ƽ�ɫȨ��
                string strSqlSeller = "";
                string dataAutor = string.Format(OperatorProvider.Provider.Current().DataAuthorize.ReadAutorize, OperatorProvider.Provider.Current().UserId);
                strSqlSeller = " SellerId in (" + dataAutor + ") ";

                //���Ƴ���Ȩ��
                string des = OperatorProvider.Provider.Current().Description;
                string strSqlArea = "";
                if (!string.IsNullOrEmpty(des))
                {
                    des = des.Replace("|", "','");
                    strSqlArea = " or (province in ('" + des + "') or city in ('" + des + "') or district in ('" + des + "')  and SellerId is null) ";
                }
                else
                {
                    strSqlArea = " or SellerId is null ";
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
        public POS_ShopEntity GetEntity(string keyValue)
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
        public void SaveForm(string keyValue, POS_ShopEntity entity)
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


        /// <summary>
        /// ������������
        /// </summary>
        /// <param name="dtSource">ʵ�����</param>
        /// <returns></returns>
        public string BatchAddEntity(DataTable dtSource)
        {
            for (int i = 0; i < dtSource.Rows.Count; i++)
            {
                string CustomerName = dtSource.Rows[i][0].ToString();//����
                string CustomerCapital = dtSource.Rows[i][1].ToString();//ע���ʽ�
                DateTime? CustomerBuildTime = null;
                if (!string.IsNullOrEmpty(dtSource.Rows[i][2].ToString()))
                {
                    CustomerBuildTime = Convert.ToDateTime(dtSource.Rows[i][2].ToString());//��������
                }                
                string CustomerWebsite = dtSource.Rows[i][3].ToString();//��վ
                string CustomerMailbox = dtSource.Rows[i][4].ToString();//����
                string CustomerScope = dtSource.Rows[i][5].ToString();//��Ӫ��Χ
                string CustomerAddress = dtSource.Rows[i][6].ToString();//��ַ
                string CustomerContacts = dtSource.Rows[i][7].ToString();//����������
                string CustomerTelphone = dtSource.Rows[i][8].ToString();//��ϵ�绰
                string CustomerContactsSpare = dtSource.Rows[i][9].ToString();//������ϵ��
                string CustomerTelphoneSpare = dtSource.Rows[i][10].ToString();//������ϵ��ʽ
                string CustomerDescription = dtSource.Rows[i][11].ToString();//��ע
                try
                {
                    IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();
                    if (!string.IsNullOrEmpty(CustomerName))
                    {
                        #region �ظ������ж�
                        var shop_Data = db.FindEntity<POS_ShopEntity>(t => t.ShopName == CustomerName);
                        if (shop_Data != null)
                        {
                            return CustomerName + "�����ظ����룡";
                        }
                        var company_Data = db.FindEntity<POS_OfficeCompanyEntity>(t => t.CompanyName == CustomerName);
                        if (company_Data != null)
                        {
                            return CustomerName + "��˾�ظ����룡";
                        } 
                        #endregion

                        #region ���ݵ�ַת������ȡ����
                        RestApi restApi = new RestApi();
                        string url = @"http://restapi.amap.com/v3/geocode/geo?address=" + CustomerAddress + "&key=cf3dd05a8192fd1839628b39e589c89e";//output=XML&
                        string responseJson = HttpClientHelper.Get(url);
                        restApi = JsonConvert.DeserializeObject<RestApi>(responseJson.Replace("[]", "\"\""));

                        List<GeocodesItem> geo = restApi.geocodes;
                        string wxLonStr = "";//118.358518
                        string wxLatStr = "";//35.049133
                        string bdLatStr = "";
                        string bdLonStr = "";
                        string Province = "";
                        string CityCode = "";
                        string City = "";
                        string District = "";

                        if (geo != null)
                        {
                            string location = geo[0].location;
                            string district = geo[0].district;
                            if (!string.IsNullOrEmpty(location) && !string.IsNullOrEmpty(district))
                            {
                                string[] jw = location.Split(',');
                                if (jw.Length == 2)
                                {
                                    wxLonStr = jw[0];
                                    wxLatStr = jw[1];
                                    double wxLon = double.Parse(wxLonStr);
                                    double wxLat = double.Parse(wxLatStr);

                                    //΢��ת���ٶ�����
                                    double bdLat, bdLon;
                                    MapConverter.GCJ02ToBD09(wxLat, wxLon, out bdLat, out bdLon);
                                    bdLonStr = bdLon.ToString();
                                    bdLatStr = bdLat.ToString();
                                }
                            }
                            Province = geo[0].province;
                            CityCode = geo[0].citycode;
                            City = geo[0].city;
                            District = district;
                        }
                        #endregion
                        
                        #region �Ȳ鿴��ǰ���꣬�Ƿ���д��¥����һ��
                        var office_Data = db.FindEntity<POS_OfficeEntity>(t => t.wxLat == wxLatStr && t.wxLon == wxLonStr);
                        if (office_Data != null)
                        {
                            #region ���һ��,���д��¥��˾��Ϣ
                            POS_OfficeCompanyEntity officeCompanyEntity = new POS_OfficeCompanyEntity()
                            {
                                OfficeId = office_Data.Id,
                                OfficeName = office_Data.OfficeName,
                                CompanyName = CustomerName,
                                CompanyAddress = CustomerAddress,
                                Capital = CustomerCapital,//ע���ʽ�
                                BuildTime = CustomerBuildTime,//��������
                                Website = CustomerWebsite,//��վ
                                Mailbox = CustomerMailbox,//����
                                Scope = CustomerScope,//��Ӫ��Χ
                                Contacts = CustomerContacts,//����������
                                Telphone = CustomerTelphone,//��ϵ�绰
                                ContactsSpare = CustomerContactsSpare,//������ϵ��
                                TelphoneSpare = CustomerTelphoneSpare,//������ϵ��ʽ
                                Description = CustomerDescription,//��ע
                                IsSee = 0,
                                State = 0
                            };
                            officeCompanyEntity.Create();
                            db.Insert<POS_OfficeCompanyEntity>(officeCompanyEntity);
                            #endregion
                        }
                        else
                        {
                            #region �ж�һ����������Ƿ��Ѿ����ڵ�����Ϣ
                            var zb_Data = db.FindEntity<POS_ShopEntity>(t => t.wxLat == wxLatStr && t.wxLon == wxLonStr);
                            //����Ѿ����ڵ�����Ϣ����ӹ�˾��Ϣ
                            if (zb_Data != null)
                            {
                                #region �ж��Ƿ���ӹ�д��¥��Ϣ
                                var shopOffice = db.FindEntity<POS_OfficeEntity>(t => t.wxLat == wxLatStr && t.wxLon == wxLonStr);
                                if (shopOffice != null)
                                {
                                    #region ����д��¥��ֱ�ӽ���ǰ���ݲ����д��¥��Ӧ��д��¥��˾����ת�Ƶ�ǰ������Ϣ����˾��
                                    POS_OfficeCompanyEntity officeCompanyEntity = new POS_OfficeCompanyEntity()
                                    {
                                        OfficeId = shopOffice.Id,
                                        OfficeName = shopOffice.OfficeName,
                                        CompanyName = CustomerName,
                                        CompanyAddress = CustomerAddress,
                                        Capital = CustomerCapital,//ע���ʽ�
                                        BuildTime = CustomerBuildTime,//��������
                                        Website = CustomerWebsite,//��վ
                                        Mailbox = CustomerMailbox,//����
                                        Scope = CustomerScope,//��Ӫ��Χ
                                        Contacts = CustomerContacts,//����������
                                        Telphone = CustomerTelphone,//��ϵ�绰
                                        ContactsSpare = CustomerContactsSpare,//������ϵ��
                                        TelphoneSpare = CustomerTelphoneSpare,//������ϵ��ʽ
                                        Description = CustomerDescription,//��ע
                                        IsSee = 0,
                                        State = 0
                                    };
                                    officeCompanyEntity.Create();
                                    db.Insert<POS_OfficeCompanyEntity>(officeCompanyEntity);

                                    //����ǰ�ĵ�����Ϣ��ת���ɹ�˾��Ϣ
                                    POS_OfficeCompanyEntity companyEntity = new POS_OfficeCompanyEntity()
                                    {
                                        OfficeId = shopOffice.Id,
                                        OfficeName = shopOffice.OfficeName,
                                        CompanyName = zb_Data.ShopName,
                                        CompanyAddress = zb_Data.ShopAddress,
                                        Capital = CustomerCapital,//ע���ʽ�
                                        BuildTime = CustomerBuildTime,//��������
                                        Website = CustomerWebsite,//��վ
                                        Mailbox = CustomerMailbox,//����
                                        Scope = zb_Data.Scope,//��Ӫ��Χ
                                        Contacts = zb_Data.Contacts,//����������
                                        Telphone = zb_Data.Telphone,//��ϵ�绰
                                        ContactsSpare = CustomerContactsSpare,//������ϵ��
                                        TelphoneSpare = CustomerTelphoneSpare,//������ϵ��ʽ
                                        Description = CustomerDescription,//��ע
                                        IsSee = 0,
                                        State = 0
                                    };
                                    companyEntity.Create();
                                    db.Insert<POS_OfficeCompanyEntity>(companyEntity);
                                    #endregion
                                }
                                else
                                {
                                    #region �����δ���д��¥����һ�������д��¥��Ϣ������ӹ�˾��Ϣ������������Ϣת�Ƴɹ�˾��Ϣ
                                    RestPoi restAPoi = new RestPoi();
                                    //http://restapi.amap.com/v3/place/text?&keywords=ɽ��ʡ�������ٹ�·100��&city=����&output=xml&offset=1&page=1&types=120000&key=cf3dd05a8192fd1839628b39e589c89e
                                    string urlPoi = @"http://restapi.amap.com/v3/place/text?keywords=" + CustomerAddress + "&city=" + City + "&offset=1&page=1&types=120000&key=cf3dd05a8192fd1839628b39e589c89e";//output=XML&
                                    string responseJsonPoi = HttpClientHelper.Get(urlPoi);
                                    restAPoi = JsonConvert.DeserializeObject<RestPoi>(responseJsonPoi.Replace("[]", "\"\"")); 
                                    List<Pois> pois = restAPoi.pois;
                                    string OfficeName = "";
                                    if (pois.Count != 0)
                                    {
                                        OfficeName = pois[0].name;//���ݸߵ½ӿ�ͨ����ַ��ȡд��¥����
                                    }

                                    POS_OfficeEntity officeEntity = new POS_OfficeEntity()
                                    {
                                        wxLon = wxLonStr,
                                        wxLat = wxLatStr,
                                        bdLon = bdLonStr,
                                        bdLat = bdLatStr,
                                        Province = Province,
                                        CityCode = CityCode,
                                        City = City,
                                        District = District,
                                        OfficeName = OfficeName,
                                        OfficeAddress = CustomerAddress,
                                        IsSee = 0,
                                        State = 0
                                    };
                                    officeEntity.Create();
                                    db.Insert<POS_OfficeEntity>(officeEntity);


                                    //����ӹ�˾��Ϣ
                                    POS_OfficeCompanyEntity officeCompanyEntity = new POS_OfficeCompanyEntity()
                                    {
                                        OfficeId = officeEntity.Id,
                                        OfficeName = officeEntity.OfficeName,
                                        CompanyName = CustomerName,
                                        CompanyAddress = CustomerAddress,
                                        Capital = CustomerCapital,//ע���ʽ�
                                        BuildTime = CustomerBuildTime,//��������
                                        Website = CustomerWebsite,//��վ
                                        Mailbox = CustomerMailbox,//����
                                        Scope = CustomerScope,//��Ӫ��Χ
                                        Contacts = CustomerContacts,//����������
                                        Telphone = CustomerTelphone,//��ϵ�绰
                                        ContactsSpare = CustomerContactsSpare,//������ϵ��
                                        TelphoneSpare = CustomerTelphoneSpare,//������ϵ��ʽ
                                        Description = CustomerDescription,//��ע
                                        IsSee = 0,
                                        State = 0
                                    };
                                    officeCompanyEntity.Create();
                                    db.Insert<POS_OfficeCompanyEntity>(officeCompanyEntity);

                                    //����ǰ�ĵ�����Ϣ��ת���ɹ�˾��Ϣ
                                    POS_OfficeCompanyEntity companyEntity = new POS_OfficeCompanyEntity()
                                    {
                                        OfficeId = officeEntity.Id,
                                        OfficeName = officeEntity.OfficeName,
                                        CompanyName = zb_Data.ShopName,
                                        CompanyAddress = zb_Data.ShopAddress,
                                        Capital = CustomerCapital,//ע���ʽ�
                                        BuildTime = CustomerBuildTime,//��������
                                        Website = CustomerWebsite,//��վ
                                        Mailbox = CustomerMailbox,//����
                                        Scope = zb_Data.Scope,//��Ӫ��Χ
                                        Contacts = zb_Data.Contacts,//����������
                                        Telphone = zb_Data.Telphone,//��ϵ�绰
                                        ContactsSpare = CustomerContactsSpare,//������ϵ��
                                        TelphoneSpare = CustomerTelphoneSpare,//������ϵ��ʽ
                                        Description = CustomerDescription,//��ע
                                        IsSee = 0,
                                        State = 0
                                    };
                                    companyEntity.Create();
                                    db.Insert<POS_OfficeCompanyEntity>(companyEntity);
                                    #endregion
                                }
                                //ɾ����ǰ������Ϣ����Ϊ�Ѿ�ת�Ƴ�д��¥��˾��Ϣ
                                db.Delete<POS_ShopEntity>(zb_Data.Id);
                                #endregion
                            }
                            else
                            {
                                #region �����ڣ�ֱ����ӵ�����Ϣ
                                POS_ShopEntity entity = new POS_ShopEntity();
                                entity.wxLon = wxLonStr;
                                entity.wxLat = wxLatStr;
                                entity.bdLon = bdLonStr;
                                entity.bdLat = bdLatStr;
                                entity.Province = Province;
                                entity.CityCode = CityCode;
                                entity.City = City;
                                entity.District = District;
                                entity.ShopName = CustomerName;//��������
                                entity.ShopAddress = CustomerAddress;//��ַ
                                entity.Capital = CustomerCapital;//ע���ʽ�
                                entity.BuildTime = CustomerBuildTime;//��������
                                entity.Website = CustomerWebsite;//��վ
                                entity.Mailbox = CustomerMailbox;//����
                                entity.Scope = CustomerScope;//��Ӫ��Χ
                                entity.Contacts = CustomerContacts;//����������
                                entity.Telphone = CustomerTelphone;//��ϵ�绰
                                entity.ContactsSpare = CustomerContactsSpare;//������ϵ��
                                entity.TelphoneSpare = CustomerTelphoneSpare;//������ϵ��ʽ
                                entity.Description = CustomerDescription;//��ע
                                entity.IsSee = 0;
                                entity.State = 0;
                                //entity.SellerId = OperatorProvider.Provider.Current().UserId;
                                //entity.SellerName = OperatorProvider.Provider.Current().UserName;

                                entity.Create();
                                db.Insert<POS_ShopEntity>(entity);
                                #endregion
                            }
                            #endregion
                        } 
                        #endregion

                    }
                    db.Commit();
                }
                catch (Exception ex)
                {
                    int j = i + 2;
                    return "ִ�е���"+ j+"�С�"+ CustomerName + ex.Message;
                }
            }
            return "����ɹ�";

        }
    }
}
