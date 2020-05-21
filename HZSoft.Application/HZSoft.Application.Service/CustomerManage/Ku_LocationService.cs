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
using System.Text.RegularExpressions;

namespace HZSoft.Application.Service.CustomerManage
{
    /// <summary>
    /// 版 本 6.1
    /// 
    /// 创 建：超级管理员
    /// 日 期：2018-03-26 11:54
    /// 描 述：坐标库
    /// </summary>
    public class Ku_LocationService : RepositoryFactory<Ku_LocationEntity>, Ku_LocationIService
    {
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<Ku_LocationEntity> GetPageList(Pagination pagination, string queryJson)
        {
            var expression = LinqExtensions.True<Ku_LocationEntity>();
            var queryParam = queryJson.ToJObject();
            string strSql = $"select * from Ku_Location where 1=1 ";
            //单据日期
            if (!queryParam["StartTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
            {
                DateTime startTime = queryParam["StartTime"].ToDate();
                DateTime endTime = queryParam["EndTime"].ToDate().AddDays(1);
                strSql += " and ModifyDate >= '" + startTime + "' and ModifyDate < '" + endTime + "'";
            }
            //商圈
            if (!queryParam["RegeoName"].IsEmpty())
            {
                string RegeoName = queryParam["RegeoName"].ToString();
                strSql += " and RegeoName  like '%" + RegeoName + "%' ";
            }
            //区域
            if (!queryParam["District"].IsEmpty())
            {
                string District = queryParam["District"].ToString();
                District = District.Replace("[", "").Replace("]", "").Replace("\"", "'");
                strSql += " and District in(" + District + ")";
            }
            //POI分布
            if (!queryParam["TypeName"].IsEmpty())
            {
                string TypeName = queryParam["TypeName"].ToString();
                TypeName = TypeName.Replace("[", "").Replace("]", "").Replace("\"", "'");
                strSql += " and TypeName in(" + TypeName + ")";
            }
            return this.BaseRepository().FindList(strSql.ToString(), pagination);
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<Ku_LocationEntity> GetList(string queryJson)
        {
            var queryParam = queryJson.ToJObject();
            //限坐标表
            string strOrder = "";
            string strSql = "";
            string des = OperatorProvider.Provider.Current().Description;
            //string des = "兰山区";
            string locationSql = LocationHelper.GetLocationSql(des);
            //假如有微信坐标的话
            if (!queryParam["wxLon"].IsEmpty() && !queryParam["wxLat"].IsEmpty())
            {
                string wxLon = queryParam["wxLon"].ToString();
                string wxLat = queryParam["wxLat"].ToString();
                strSql = "select top 100 id,wxlon,wxlat,regeoname,address,count,dbo.f_GetDistance(" + wxLon + "," + wxLat + ",wxlon,wxlat) as description,picture FROM " + locationSql + " t where 1=1";
                strOrder = " order by dbo.f_GetDistance(" + wxLon + "," + wxLat + ",wxlon,wxlat) asc";
            }
            else
            {
                strSql = "select top 100 id,wxlon,wxlat,regeoname,address,count from " + locationSql + " where 1 = 1 ";
                strOrder = " ORDER BY CreateDate desc";
            }
            //店铺名
            if (!queryParam["SearchName"].IsEmpty())
            {
                string OfficeName = queryParam["SearchName"].ToString();
                strSql += " and regeoname = '" + OfficeName + "'";
            }
            strSql += strOrder;

            return this.BaseRepository().FindList(strSql.ToString());
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public Ku_LocationEntity GetEntity(int? keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void RemoveForm(int? keyValue)
        {
            this.BaseRepository().Delete(keyValue);
        }
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public void SaveForm(int? keyValue, Ku_LocationEntity entity)
        {
            IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();
            if (keyValue !=null)
            {
                var location_Data = db.FindEntity<Ku_LocationEntity>(t => t.Id == keyValue);//公司重复判断                                                                                           
                if (location_Data != null)
                {
                    if (entity.RegeoName!= location_Data.RegeoName)
                    {
                        //var companyList = db.FindList<Ku_CompanyEntity>(t => t.RegeoId == location_Data.RegeoId);
                        //foreach (var item in companyList)
                        //{
                        //    item.RegeoName = entity.RegeoName;
                        //}
                        db.ExecuteBySql("UPDATE dbo.Ku_Company SET RegeoName = '"+ entity.RegeoName + "' WHERE RegeoId = '"+ location_Data.RegeoId + "'");
                        db.Commit();
                    }
                }


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



        #region 根据地址通过高德地图api转成坐标，返回添加的坐标信息
        /// <summary>
        /// 根据地址通过高德地图api转成坐标，返回添加的坐标信息
        /// </summary>
        /// <param name="address">地址</param>
        public Ku_LocationEntity AddressToLocation(string address)
        {
            IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();
            try
            {
                int isRegeo = 0;
                Ku_LocationEntity locationData = null;
                //根据地址转换，获取坐标
                GeocodesItem geo = GDAPI.AddressToGeo(address);
                //逆地理接口，通过坐标推出商圈
                Regeocode regeo = GDAPI.GetRegeoByGeo(geo.location);
                //1.先pois,distance=0的  2.再aoi=0的，
                if (regeo != null)
                {
                    List<PoisList> pois = regeo.pois;
                    List<Aois> aois = regeo.aois;

                    //1.先pois,distance=0的
                    if (pois != null)
                    {
                        PoisList poi = pois[0];
                        if (poi.distance == "0")
                        {
                            isRegeo = 1;
                            locationData = GetLocationByPOI(db, locationData, geo, regeo, poi, isRegeo);
                        }
                    }
                    //2.再aoi,distance=0的
                    if (isRegeo == 0)
                    {
                        if (aois != null)
                        {
                            Aois aoi = aois[0];//aoi根据距离排序的
                            if (aoi.distance == "0")
                            {
                                isRegeo = 2;
                                locationData = GetLocationByAOI(db, locationData, geo, regeo, aoi, isRegeo);
                            }
                        }
                    }

                    if (isRegeo == 0)
                    {
                        //3.根据formatted_address再pois，
                        GeocodesItem geo2 = GDAPI.AddressToGeo(geo.formatted_address);
                        //如果二次转换为区县，市，未知，且与第一次转换的坐标一致，则放弃转换
                        if (geo2 != null && geo2.level != "区县" && geo2.level != "市" && geo2.level != "未知" && geo2.location != geo.location)
                        {
                            //逆地理接口，通过坐标推出商圈
                            Regeocode regeo2 = GDAPI.GetRegeoByGeo(geo2.location);
                            if (regeo2 != null)
                            {
                                List<PoisList> pois2 = regeo2.pois;
                                List<Aois> aois2 = regeo2.aois;
                                if (pois2 != null)
                                {
                                    PoisList poi2 = pois2[0];
                                    if (poi2.distance == "0")
                                    {
                                        isRegeo = 3;
                                        locationData = GetLocationByPOI(db, locationData, geo2, regeo2, poi2, isRegeo);
                                    }
                                }
                                //4.再aois
                                if (isRegeo == 0)
                                {
                                    if (aois2 != null)
                                    {
                                        Aois aoi2 = aois2[0];
                                        if (aoi2.distance == "0")
                                        {
                                            isRegeo = 4;
                                            locationData = GetLocationByAOI(db, locationData, geo2, regeo2, aoi2, isRegeo);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                //0.无逆地理geo 
                if (isRegeo == 0)
                {
                    locationData = GetLocationByGeo(db, locationData, geo, 0);
                }
                return locationData;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region 添加位置信息表的5种情况

        //通过poi获得位置信息表(1,3)
        public Ku_LocationEntity GetLocationByPOI(IRepository db, Ku_LocationEntity locationData, GeocodesItem geo, Regeocode regeo, PoisList item, int isRegeo)
        {
            locationData = db.FindList<Ku_LocationEntity>(t => t.RegeoId == item.id).FirstOrDefault();
            if (locationData != null)
            {
                //有此坐标，个数+1
                locationData.Count += 1;
                locationData.Modify(locationData.Id);
                db.Update<Ku_LocationEntity>(locationData);
            }
            else
            {
                AddressComponent addressComponent = regeo.addressComponent;
                string formatted_address = regeo.formatted_address;

                double wxLon = 0, wxLat = 0, bdLon = 0, bdLat = 0;
                GDAPI.getCoordinate(item.location, out wxLat, out wxLon, out bdLat, out bdLon);

                string typeCode, typeName;
                GetTypeCodeByName(item.type, out typeCode, out typeName);
                //无此坐标,插入pos_location表
                locationData = new Ku_LocationEntity()
                {
                    Address = item.address,
                    Province = addressComponent.province,
                    City = addressComponent.city,
                    CityCode = addressComponent.citycode,
                    District = addressComponent.district,
                    AdCode = addressComponent.adcode,
                    Township = addressComponent.township,
                    Location = item.location,
                    wxLon = Convert.ToDecimal(wxLon),
                    wxLat = Convert.ToDecimal(wxLat),
                    bdLon = Convert.ToDecimal(bdLon),
                    bdLat = Convert.ToDecimal(bdLat),
                    _Level = geo.level,
                    IsRegeo = isRegeo,
                    RegeoId = item.id,
                    RegeoName = item.name,
                    TypeCode = typeCode,
                    TypeName = typeName,
                    Count = 1
                };
                locationData.Create();
                db.Insert<Ku_LocationEntity>(locationData);
            }
            return locationData;
        }

        //通过aoi获得位置信息表(2,4)
        public Ku_LocationEntity GetLocationByAOI(IRepository db, Ku_LocationEntity locationData, GeocodesItem geo, Regeocode regeo, Aois aoi, int isRegeo)
        {
            locationData = db.FindList<Ku_LocationEntity>(t => t.RegeoId == aoi.id).FirstOrDefault();
            if (locationData != null)
            {
                //有此坐标，个数+1
                locationData.Count += 1;
                locationData.Modify(locationData.Id);
                db.Update<Ku_LocationEntity>(locationData);
            }
            else
            {
                AddressComponent addressComponent = regeo.addressComponent;

                double wxLon = 0, wxLat = 0, bdLon = 0, bdLat = 0;
                GDAPI.getCoordinate(aoi.location, out wxLat, out wxLon, out bdLat, out bdLon);

                string typeCode, typeName;
                GetTypeNameByCode(aoi.type, out typeCode, out typeName);
                //无此坐标,插入pos_location表
                locationData = new Ku_LocationEntity()
                {
                    Address = regeo.formatted_address,
                    Province = addressComponent.province,
                    City = addressComponent.city,
                    CityCode = addressComponent.citycode,
                    District = addressComponent.district,
                    AdCode = addressComponent.adcode,
                    Township = addressComponent.township,
                    Location = aoi.location,
                    wxLon = Convert.ToDecimal(wxLon),
                    wxLat = Convert.ToDecimal(wxLat),
                    bdLon = Convert.ToDecimal(bdLon),
                    bdLat = Convert.ToDecimal(bdLat),
                    _Level = geo.level,
                    IsRegeo = isRegeo,
                    RegeoId = aoi.id,
                    RegeoName = aoi.name,
                    Area = aoi.area,//存在字母转换失败decimal“山东省临沂市平邑县地方镇九间棚”
                    TypeCode = typeCode,
                    TypeName = typeName,
                    Count = 1
                };
                locationData.Create();
                db.Update<Ku_LocationEntity>(locationData);

            }
            return locationData;
        }

        //通过geo获得位置信息表(5)
        public Ku_LocationEntity GetLocationByGeo(IRepository db, Ku_LocationEntity locationData, GeocodesItem geo, int isRegeo)
        {
            //如果aoi取消
            locationData = db.FindList<Ku_LocationEntity>(t => t.Location == geo.location).FirstOrDefault();
            if (locationData != null)
            {
                locationData.Count += 1;
                locationData.Modify(locationData.Id);
                db.Update<Ku_LocationEntity>(locationData);
            }
            else
            {
                //添加POI
                double wxLon = 0, wxLat = 0, bdLon = 0, bdLat = 0;
                GDAPI.getCoordinate(geo.location, out wxLat, out wxLon, out bdLat, out bdLon);
                //无此坐标,插入pos_location表
                locationData = new Ku_LocationEntity()
                {
                    RegeoName = geo.formatted_address.Replace(geo.province, "").Replace(geo.city, "").Replace(geo.district, ""),
                    Address = geo.formatted_address,
                    Province = geo.province,
                    City = geo.city,
                    CityCode = geo.citycode,
                    District = geo.district,
                    AdCode = geo.adcode,
                    Township = geo.township,
                    Location = geo.location,
                    wxLon = Convert.ToDecimal(wxLon),
                    wxLat = Convert.ToDecimal(wxLat),
                    bdLon = Convert.ToDecimal(bdLon),
                    bdLat = Convert.ToDecimal(bdLat),
                    _Level = geo.level,
                    IsRegeo = 0,
                    Count = 1
                };
                locationData.Create();
                db.Insert<Ku_LocationEntity>(locationData);
            }

            return locationData;
        }
        #endregion

        #region 高德POI编码转换
        //根据code获取子类名称
        public void GetTypeNameByCode(string typeCodes, out string typeCode, out string typeName)
        {
            typeName = ""; typeCode = typeCodes;
            IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();
            {
                //只匹配第一个
                string[] codes = typeCode.Split('|');
                string typeCode1 = codes[0];
                var poiTypeData = db.FindList<POS_PoiTypeCode>(t => t.TypeCode == typeCode1).FirstOrDefault();
                if (poiTypeData != null)
                {
                    typeName = poiTypeData.SubCategory;
                    typeCode = typeCode1;
                }
            }

        }

        //根据子类获取类型code
        public void GetTypeCodeByName(string typeNames, out string typeCode, out string typeName)
        {
            typeCode = ""; typeName = typeNames;
            IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();
            {
                string[] names = typeNames.Split('|');
                string typeName1 = names[0];
                if (typeName1.LastIndexOf(";") > 0)
                {
                    int sub = typeName1.LastIndexOf(";");
                    typeName1 = typeName1.Substring(sub + 1, typeName1.Length - sub - 1);
                }
                var poiTypeData = db.FindList<POS_PoiTypeCode>(t => t.SubCategory == typeName1).FirstOrDefault();
                if (poiTypeData != null)
                {
                    typeCode = poiTypeData.TypeCode;
                    typeName = typeName1;
                }
            }
        }
        #endregion

    }
}
