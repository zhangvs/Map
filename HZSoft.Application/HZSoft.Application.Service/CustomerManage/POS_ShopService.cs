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
    /// 版 本 6.1
    /// 
    /// 创 建：超级管理员
    /// 日 期：2017-12-02 16:17
    /// 描 述：POS_Shop
    /// </summary>
    public class POS_ShopService : RepositoryFactory<POS_ShopEntity>, POS_ShopIService
    {
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<POS_ShopEntity> GetPageList(Pagination pagination, string queryJson)
        {
            var expression = LinqExtensions.True<POS_ShopEntity>();
            var queryParam = queryJson.ToJObject();
            string strSql = "select * from POS_Shop where 1=1";
            //单据日期
            if (!queryParam["StartTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
            {
                DateTime startTime = queryParam["StartTime"].ToDate();
                DateTime endTime = queryParam["EndTime"].ToDate().AddDays(1);
                strSql += " and CreateTime >= '" + startTime + "' and CreateTime < '" + endTime + "'";
            }
            //手机号
            if (!queryParam["Telphone"].IsEmpty())
            {
                string Telphone = queryParam["Telphone"].ToString();
                strSql += " and Telphone = '" + Telphone + "'";
            }
            //联系人
            if (!queryParam["Contacts"].IsEmpty())
            {
                string Contacts = queryParam["Contacts"].ToString();
                strSql += " and Contacts = '" + Contacts + "'";
            }
            //店铺ID
            if (!queryParam["Id"].IsEmpty())
            {
                string Id = queryParam["Id"].ToString();
                strSql += " and Id = '" + Id + "'";
            }
            //店铺名
            if (!queryParam["ShopName"].IsEmpty())
            {
                string ShopName = queryParam["ShopName"].ToString();
                strSql += " and ShopName = '" + ShopName + "'";
            }
            //状态
            if (!queryParam["State"].IsEmpty())
            {
                string State = queryParam["State"].ToString();
                strSql += " and State = '" + State + "'";
            }

            //销售人
            if (!queryParam["SellerId"].IsEmpty())
            {
                string SellerId = queryParam["SellerId"].ToString();
                strSql += " and SellerId = '" + SellerId + "'";
            }
            else
            {
                if (!OperatorProvider.Provider.Current().IsSystem)
                {
                    //限制角色权限
                    string strSqlSeller = "";
                    string dataAutor = string.Format(OperatorProvider.Provider.Current().DataAuthorize.ReadAutorize, OperatorProvider.Provider.Current().UserId);
                    strSqlSeller = " SellerId in (" + dataAutor + ") ";

                    //限制城市权限
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
                   //除了角色范围内的，还包括or城市权限范围内的（上传人不为空的，被别人编辑之后的就不包含在内）
                    strSql += " and ("+ strSqlSeller + strSqlArea + ")";
                }
            }




            return this.BaseRepository().FindList(strSql.ToString(), pagination);
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
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

            //店铺名
            if (!queryParam["SearchName"].IsEmpty())
            {
                string ShopName = queryParam["SearchName"].ToString();
                strSql += " and ShopName = '" + ShopName + "'";
            }
            if (!OperatorProvider.Provider.Current().IsSystem)
            {
                //限制角色权限
                string strSqlSeller = "";
                string dataAutor = string.Format(OperatorProvider.Provider.Current().DataAuthorize.ReadAutorize, OperatorProvider.Provider.Current().UserId);
                strSqlSeller = " SellerId in (" + dataAutor + ") ";

                //限制城市权限
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
                //除了角色范围内的，还包括or城市权限范围内的（上传人不为空的，被别人编辑之后的就不包含在内）
                strSql += " and (" + strSqlSeller + strSqlArea + ")";
            }

            strSql += strOrder;

            return this.BaseRepository().FindList(strSql.ToString());
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public POS_ShopEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void RemoveForm(string keyValue)
        {
            this.BaseRepository().Delete(keyValue);
        }
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
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
        /// 批量（新增）
        /// </summary>
        /// <param name="dtSource">实体对象</param>
        /// <returns></returns>
        public string BatchAddEntity(DataTable dtSource)
        {
            for (int i = 0; i < dtSource.Rows.Count; i++)
            {
                string CustomerName = dtSource.Rows[i][0].ToString();//名称
                string CustomerCapital = dtSource.Rows[i][1].ToString();//注册资金
                DateTime? CustomerBuildTime = null;
                if (!string.IsNullOrEmpty(dtSource.Rows[i][2].ToString()))
                {
                    CustomerBuildTime = Convert.ToDateTime(dtSource.Rows[i][2].ToString());//成立日期
                }                
                string CustomerWebsite = dtSource.Rows[i][3].ToString();//网站
                string CustomerMailbox = dtSource.Rows[i][4].ToString();//邮箱
                string CustomerScope = dtSource.Rows[i][5].ToString();//经营范围
                string CustomerAddress = dtSource.Rows[i][6].ToString();//地址
                string CustomerContacts = dtSource.Rows[i][7].ToString();//法定代表人
                string CustomerTelphone = dtSource.Rows[i][8].ToString();//联系电话
                string CustomerContactsSpare = dtSource.Rows[i][9].ToString();//备用联系人
                string CustomerTelphoneSpare = dtSource.Rows[i][10].ToString();//备用联系方式
                string CustomerDescription = dtSource.Rows[i][11].ToString();//备注
                try
                {
                    IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();
                    if (!string.IsNullOrEmpty(CustomerName))
                    {
                        #region 重复导入判断
                        var shop_Data = db.FindEntity<POS_ShopEntity>(t => t.ShopName == CustomerName);
                        if (shop_Data != null)
                        {
                            return CustomerName + "店铺重复导入！";
                        }
                        var company_Data = db.FindEntity<POS_OfficeCompanyEntity>(t => t.CompanyName == CustomerName);
                        if (company_Data != null)
                        {
                            return CustomerName + "公司重复导入！";
                        } 
                        #endregion

                        #region 根据地址转换，获取坐标
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

                                    //微信转换百度坐标
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
                        
                        #region 先查看当前坐标，是否与写字楼坐标一致
                        var office_Data = db.FindEntity<POS_OfficeEntity>(t => t.wxLat == wxLatStr && t.wxLon == wxLonStr);
                        if (office_Data != null)
                        {
                            #region 如果一致,添加写字楼公司信息
                            POS_OfficeCompanyEntity officeCompanyEntity = new POS_OfficeCompanyEntity()
                            {
                                OfficeId = office_Data.Id,
                                OfficeName = office_Data.OfficeName,
                                CompanyName = CustomerName,
                                CompanyAddress = CustomerAddress,
                                Capital = CustomerCapital,//注册资金
                                BuildTime = CustomerBuildTime,//成立日期
                                Website = CustomerWebsite,//网站
                                Mailbox = CustomerMailbox,//邮箱
                                Scope = CustomerScope,//经营范围
                                Contacts = CustomerContacts,//法定代表人
                                Telphone = CustomerTelphone,//联系电话
                                ContactsSpare = CustomerContactsSpare,//备用联系人
                                TelphoneSpare = CustomerTelphoneSpare,//备用联系方式
                                Description = CustomerDescription,//备注
                                IsSee = 0,
                                State = 0
                            };
                            officeCompanyEntity.Create();
                            db.Insert<POS_OfficeCompanyEntity>(officeCompanyEntity);
                            #endregion
                        }
                        else
                        {
                            #region 判断一个坐标点上是否已经存在店铺信息
                            var zb_Data = db.FindEntity<POS_ShopEntity>(t => t.wxLat == wxLatStr && t.wxLon == wxLonStr);
                            //如果已经存在店铺信息，添加公司信息
                            if (zb_Data != null)
                            {
                                #region 判断是否添加过写字楼信息
                                var shopOffice = db.FindEntity<POS_OfficeEntity>(t => t.wxLat == wxLatStr && t.wxLon == wxLonStr);
                                if (shopOffice != null)
                                {
                                    #region 存在写字楼，直接将当前数据插入该写字楼对应的写字楼公司表，并转移当前店铺信息到公司表
                                    POS_OfficeCompanyEntity officeCompanyEntity = new POS_OfficeCompanyEntity()
                                    {
                                        OfficeId = shopOffice.Id,
                                        OfficeName = shopOffice.OfficeName,
                                        CompanyName = CustomerName,
                                        CompanyAddress = CustomerAddress,
                                        Capital = CustomerCapital,//注册资金
                                        BuildTime = CustomerBuildTime,//成立日期
                                        Website = CustomerWebsite,//网站
                                        Mailbox = CustomerMailbox,//邮箱
                                        Scope = CustomerScope,//经营范围
                                        Contacts = CustomerContacts,//法定代表人
                                        Telphone = CustomerTelphone,//联系电话
                                        ContactsSpare = CustomerContactsSpare,//备用联系人
                                        TelphoneSpare = CustomerTelphoneSpare,//备用联系方式
                                        Description = CustomerDescription,//备注
                                        IsSee = 0,
                                        State = 0
                                    };
                                    officeCompanyEntity.Create();
                                    db.Insert<POS_OfficeCompanyEntity>(officeCompanyEntity);

                                    //处理当前的店铺信息，转换成公司信息
                                    POS_OfficeCompanyEntity companyEntity = new POS_OfficeCompanyEntity()
                                    {
                                        OfficeId = shopOffice.Id,
                                        OfficeName = shopOffice.OfficeName,
                                        CompanyName = zb_Data.ShopName,
                                        CompanyAddress = zb_Data.ShopAddress,
                                        Capital = CustomerCapital,//注册资金
                                        BuildTime = CustomerBuildTime,//成立日期
                                        Website = CustomerWebsite,//网站
                                        Mailbox = CustomerMailbox,//邮箱
                                        Scope = zb_Data.Scope,//经营范围
                                        Contacts = zb_Data.Contacts,//法定代表人
                                        Telphone = zb_Data.Telphone,//联系电话
                                        ContactsSpare = CustomerContactsSpare,//备用联系人
                                        TelphoneSpare = CustomerTelphoneSpare,//备用联系方式
                                        Description = CustomerDescription,//备注
                                        IsSee = 0,
                                        State = 0
                                    };
                                    companyEntity.Create();
                                    db.Insert<POS_OfficeCompanyEntity>(companyEntity);
                                    #endregion
                                }
                                else
                                {
                                    #region 如果尚未添加写字楼，第一次先添加写字楼信息，再添加公司信息，将当店铺信息转移成公司信息
                                    RestPoi restAPoi = new RestPoi();
                                    //http://restapi.amap.com/v3/place/text?&keywords=山东省临沂市临工路100号&city=临沂&output=xml&offset=1&page=1&types=120000&key=cf3dd05a8192fd1839628b39e589c89e
                                    string urlPoi = @"http://restapi.amap.com/v3/place/text?keywords=" + CustomerAddress + "&city=" + City + "&offset=1&page=1&types=120000&key=cf3dd05a8192fd1839628b39e589c89e";//output=XML&
                                    string responseJsonPoi = HttpClientHelper.Get(urlPoi);
                                    restAPoi = JsonConvert.DeserializeObject<RestPoi>(responseJsonPoi.Replace("[]", "\"\"")); 
                                    List<Pois> pois = restAPoi.pois;
                                    string OfficeName = "";
                                    if (pois.Count != 0)
                                    {
                                        OfficeName = pois[0].name;//根据高德接口通过地址获取写字楼名称
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


                                    //再添加公司信息
                                    POS_OfficeCompanyEntity officeCompanyEntity = new POS_OfficeCompanyEntity()
                                    {
                                        OfficeId = officeEntity.Id,
                                        OfficeName = officeEntity.OfficeName,
                                        CompanyName = CustomerName,
                                        CompanyAddress = CustomerAddress,
                                        Capital = CustomerCapital,//注册资金
                                        BuildTime = CustomerBuildTime,//成立日期
                                        Website = CustomerWebsite,//网站
                                        Mailbox = CustomerMailbox,//邮箱
                                        Scope = CustomerScope,//经营范围
                                        Contacts = CustomerContacts,//法定代表人
                                        Telphone = CustomerTelphone,//联系电话
                                        ContactsSpare = CustomerContactsSpare,//备用联系人
                                        TelphoneSpare = CustomerTelphoneSpare,//备用联系方式
                                        Description = CustomerDescription,//备注
                                        IsSee = 0,
                                        State = 0
                                    };
                                    officeCompanyEntity.Create();
                                    db.Insert<POS_OfficeCompanyEntity>(officeCompanyEntity);

                                    //处理当前的店铺信息，转换成公司信息
                                    POS_OfficeCompanyEntity companyEntity = new POS_OfficeCompanyEntity()
                                    {
                                        OfficeId = officeEntity.Id,
                                        OfficeName = officeEntity.OfficeName,
                                        CompanyName = zb_Data.ShopName,
                                        CompanyAddress = zb_Data.ShopAddress,
                                        Capital = CustomerCapital,//注册资金
                                        BuildTime = CustomerBuildTime,//成立日期
                                        Website = CustomerWebsite,//网站
                                        Mailbox = CustomerMailbox,//邮箱
                                        Scope = zb_Data.Scope,//经营范围
                                        Contacts = zb_Data.Contacts,//法定代表人
                                        Telphone = zb_Data.Telphone,//联系电话
                                        ContactsSpare = CustomerContactsSpare,//备用联系人
                                        TelphoneSpare = CustomerTelphoneSpare,//备用联系方式
                                        Description = CustomerDescription,//备注
                                        IsSee = 0,
                                        State = 0
                                    };
                                    companyEntity.Create();
                                    db.Insert<POS_OfficeCompanyEntity>(companyEntity);
                                    #endregion
                                }
                                //删除当前店铺信息，因为已经转移成写字楼公司信息
                                db.Delete<POS_ShopEntity>(zb_Data.Id);
                                #endregion
                            }
                            else
                            {
                                #region 不存在，直接添加店铺信息
                                POS_ShopEntity entity = new POS_ShopEntity();
                                entity.wxLon = wxLonStr;
                                entity.wxLat = wxLatStr;
                                entity.bdLon = bdLonStr;
                                entity.bdLat = bdLatStr;
                                entity.Province = Province;
                                entity.CityCode = CityCode;
                                entity.City = City;
                                entity.District = District;
                                entity.ShopName = CustomerName;//店铺名称
                                entity.ShopAddress = CustomerAddress;//地址
                                entity.Capital = CustomerCapital;//注册资金
                                entity.BuildTime = CustomerBuildTime;//成立日期
                                entity.Website = CustomerWebsite;//网站
                                entity.Mailbox = CustomerMailbox;//邮箱
                                entity.Scope = CustomerScope;//经营范围
                                entity.Contacts = CustomerContacts;//法定代表人
                                entity.Telphone = CustomerTelphone;//联系电话
                                entity.ContactsSpare = CustomerContactsSpare;//备用联系人
                                entity.TelphoneSpare = CustomerTelphoneSpare;//备用联系方式
                                entity.Description = CustomerDescription;//备注
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
                    return "执行到第"+ j+"行。"+ CustomerName + ex.Message;
                }
            }
            return "导入成功";

        }
    }
}
