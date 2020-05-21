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
using System.Linq;
using System.Text.RegularExpressions;

namespace HZSoft.Application.Service.CustomerManage
{
    /// <summary>
    /// 版 本 6.1
    /// 
    /// 创 建：超级管理员
    /// 日 期：2018-03-23 16:28
    /// 描 述：公司库
    /// </summary>
    public class Ku_CompanyService : RepositoryFactory<Ku_CompanyEntity>, Ku_CompanyIService
    {
        private Ku_LocationService locationService = new Ku_LocationService();
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<Ku_CompanyEntity> GetPageList(Pagination pagination, string queryJson)
        {
            var expression = LinqExtensions.True<Ku_CompanyEntity>();
            var queryParam = queryJson.ToJObject();
            string des = OperatorProvider.Provider.Current().Description;
            string locationSql = LocationHelper.GetLocationSql(des);
            string strSql = $"select c.* from Ku_Company c LEFT JOIN {locationSql} l ON l.Id=c.LocationId where ManageState=1 AND LocationId>0 AND c.District!='' ";//在营公海显示被获取公司 ObtainState=0 
            //坐标位置商圈ID存在的话，不以输入的商圈文字搜索
            if (!queryParam["LocationId"].IsEmpty())
            {
                string LocationId = queryParam["LocationId"].ToString();
                strSql += " and LocationId = " + LocationId;
            }
            else if (!queryParam["RegeoName"].IsEmpty())
            {
                string RegeoName = queryParam["RegeoName"].ToString();
                strSql += " and c.RegeoName  like '%" + RegeoName + "%' ";
            }
            //位置ID
            if (!queryParam["LocationIds"].IsEmpty())
            {
                string LocationIds = queryParam["LocationIds"].ToString();
                strSql += " and LocationId in (" + LocationIds + ")";
            }            
            //单据日期
            if (!queryParam["StartTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
            {
                DateTime startTime = queryParam["StartTime"].ToDate();
                DateTime endTime = queryParam["EndTime"].ToDate().AddDays(1);
                strSql += " and BuildTime >= '" + startTime + "' and BuildTime < '" + endTime + "'";
            }
            //公司名
            if (!queryParam["CompanyName"].IsEmpty())
            {
                string CompanyName = queryParam["CompanyName"].ToString();
                strSql += " and c.CompanyName  like '%" + CompanyName + "%' ";
            }
            //经营范围
            if (!queryParam["Scope"].IsEmpty())
            {
                string Scope = queryParam["Scope"].ToString();
                strSql += " and c.Scope  like '%" + Scope + "%' ";
            }
            //行业
            if (!queryParam["Sector"].IsEmpty())
            {
                string Sector = queryParam["Sector"].ToString();
                Sector = Sector.Replace("[", "").Replace("]", "").Replace("\"", "'");
                strSql += " and c.Sector  in(" + Sector + ")";
            }
            //区域
            if (!queryParam["District"].IsEmpty())
            {
                string District = queryParam["District"].ToString();
                District = District.Replace("[", "").Replace("]", "").Replace("\"", "'");
                strSql += " and c.District in(" + District + ")";
            }
            //POI分布
            if (!queryParam["TypeName"].IsEmpty())
            {
                string TypeName = queryParam["TypeName"].ToString();
                TypeName = TypeName.Replace("[", "").Replace("]", "").Replace("\"", "'");
                strSql += " and l.TypeName in(" + TypeName + ")";
            }
            //销售人
            if (!queryParam["ObtainUserId"].IsEmpty())
            {
                string ObtainUserId = queryParam["ObtainUserId"].ToString();
                strSql += " and c.ObtainUserId = '" + ObtainUserId + "'";
            }
            //状态
            if (!queryParam["FollowState"].IsEmpty())
            {
                string FollowState = queryParam["FollowState"].ToString();
                strSql += " and c.FollowState = '" + FollowState + "'";
            }
            //排序楼层，房间
            if (pagination.sidx == "Floor")
            {
                pagination.sidx = "Floor " + pagination.sord + ",Room " + pagination.sord;
            }
            return this.BaseRepository().FindList(strSql.ToString(), pagination);
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<Ku_CompanyEntity> MyGetPageList(Pagination pagination, string queryJson)
        {
            var expression = LinqExtensions.True<Ku_CompanyEntity>();
            var queryParam = queryJson.ToJObject();
            string des = OperatorProvider.Provider.Current().Description;
            string locationSql = LocationHelper.GetLocationSql(des);
            string strSql = $"select c.* from Ku_Company c LEFT JOIN {locationSql} l ON l.Id=c.LocationId where ObtainState=1 and ManageState=1 AND LocationId>0 AND c.District!='' ";//ObtainState=1获取的
            //坐标位置商圈ID存在的话，不以输入的商圈文字搜索
            if (!queryParam["LocationId"].IsEmpty())
            {
                string LocationId = queryParam["LocationId"].ToString();
                strSql += " and LocationId = " + LocationId;
            }
            else if (!queryParam["RegeoName"].IsEmpty())
            {
                string RegeoName = queryParam["RegeoName"].ToString();
                strSql += " and c.RegeoName  like '%" + RegeoName + "%' ";
            }
            //位置ID
            if (!queryParam["LocationIds"].IsEmpty())
            {
                string LocationIds = queryParam["LocationIds"].ToString();
                strSql += " and LocationId in (" + LocationIds + ")";
            }
            //成立日期
            if (!queryParam["StartTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
            {
                DateTime startTime = queryParam["StartTime"].ToDate();
                DateTime endTime = queryParam["EndTime"].ToDate().AddDays(1);
                strSql += " and BuildTime >= '" + startTime + "' and BuildTime < '" + endTime + "'";
            }
            //公司名
            if (!queryParam["CompanyName"].IsEmpty())
            {
                string CompanyName = queryParam["CompanyName"].ToString();
                strSql += " and c.CompanyName  like '%" + CompanyName + "%' ";
            }
            //经营范围
            if (!queryParam["Scope"].IsEmpty())
            {
                string Scope = queryParam["Scope"].ToString();
                strSql += " and c.Scope  like '%" + Scope + "%' ";
            }
            //行业
            if (!queryParam["Sector"].IsEmpty())
            {
                string Sector = queryParam["Sector"].ToString();
                Sector = Sector.Replace("[", "").Replace("]", "").Replace("\"", "'");
                strSql += " and c.Sector  in(" + Sector + ")";
            }
            //区域
            if (!queryParam["District"].IsEmpty())
            {
                string District = queryParam["District"].ToString();
                District = District.Replace("[", "").Replace("]", "").Replace("\"", "'");
                strSql += " and c.District in(" + District + ")";
            }
            //POI分布
            if (!queryParam["TypeName"].IsEmpty())
            {
                string TypeName = queryParam["TypeName"].ToString();
                TypeName = TypeName.Replace("[", "").Replace("]", "").Replace("\"", "'");
                strSql += " and l.TypeName in(" + TypeName + ")";
            }
            //销售人
            if (!queryParam["ObtainUserId"].IsEmpty())
            {
                string ObtainUserId = queryParam["ObtainUserId"].ToString();
                strSql += " and c.ObtainUserId = '" + ObtainUserId + "'";
            }
            else
            {
                if (!OperatorProvider.Provider.Current().IsSystem)
                {
                    string dataAutor = string.Format(OperatorProvider.Provider.Current().DataAuthorize.ReadAutorize, OperatorProvider.Provider.Current().UserId);
                    strSql += " and (c.ObtainUserId in (" + dataAutor + "))";
                }
            }

            //销售人
            if (!queryParam["ObtainUserName"].IsEmpty())
            {
                string ObtainUserName = queryParam["ObtainUserName"].ToString();
                strSql += " and c.ObtainUserName = '" + ObtainUserName + "'";
            }
            //获取日期
            if (!queryParam["ObtainStartTime"].IsEmpty() && !queryParam["ObtainEndTime"].IsEmpty())
            {
                DateTime startTime = queryParam["ObtainStartTime"].ToDate();
                DateTime endTime = queryParam["ObtainEndTime"].ToDate().AddDays(1);
                strSql += " and ObtainDate >= '" + startTime + "' and ObtainDate < '" + endTime + "'";
            }
            //获取日期
            if (!queryParam["ModifyStartTime"].IsEmpty() && !queryParam["ModifyEndTime"].IsEmpty())
            {
                DateTime startTime = queryParam["ModifyStartTime"].ToDate();
                DateTime endTime = queryParam["ModifyEndTime"].ToDate().AddDays(1);
                strSql += " and c.ModifyDate >= '" + startTime + "' and c.ModifyDate < '" + endTime + "'";
            }
            //指标日期一天内的获取详情
            if (!queryParam["ObtainDate"].IsEmpty())
            {
                DateTime startTime = queryParam["ObtainDate"].ToDate();
                DateTime endTime = queryParam["ObtainDate"].ToDate().AddDays(1);
                strSql += " and ObtainDate >= '" + startTime + "' and ObtainDate < '" + endTime + "'";
            }
            //指标日期一天内的获取详情
            if (!queryParam["ModifyDate"].IsEmpty())
            {
                DateTime startTime = queryParam["ModifyDate"].ToDate();
                DateTime endTime = queryParam["ModifyDate"].ToDate().AddDays(1);
                strSql += " and c.ModifyDate >= '" + startTime + "' and c.ModifyDate < '" + endTime + "'";
            }

            //状态
            if (!queryParam["FollowState"].IsEmpty())
            {
                string FollowState = queryParam["FollowState"].ToString();
                strSql += " and c.FollowState = '" + FollowState + "'";
            }
            //排序楼层，房间
            if (pagination.sidx == "Floor")
            {
                pagination.sidx = "Floor " + pagination.sord + ",Room " + pagination.sord;
            }
            //单据日期
            if (!queryParam["syear"].IsEmpty())
            {
                string syear = queryParam["syear"].ToString();
                strSql += " and year(c.ModifyDate)=" + syear;
            }
            //单据日期
            if (!queryParam["smonth"].IsEmpty())
            {
                string smonth = queryParam["smonth"].ToString();
                strSql += " and month(c.ModifyDate)=" + smonth;
            }

            return this.BaseRepository().FindList(strSql.ToString(), pagination);
        }
        ///// <summary>
        ///// 获取列表
        ///// </summary>
        ///// <param name="queryJson">查询参数</param>
        ///// <returns>返回列表</returns>
        //public IEnumerable<Ku_CompanyEntity> GetList(string queryJson)
        //{
        //    return this.BaseRepository().IQueryable().ToList();
        //}
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<Ku_CompanyEntity> GetList(string queryJson)
        {
            var expression = LinqExtensions.True<Ku_CompanyEntity>();
            var queryParam = queryJson.ToJObject();
            string strSql = "";
            string strOrder = "";

            strSql = "select * from Ku_Company where ManageState=1 ";
            strOrder = " ORDER BY floor desc,room desc,CreateDate desc";
            //写字楼ID
            if (!queryParam["LocationId"].IsEmpty())
            {
                string locationid = queryParam["LocationId"].ToString();
                strSql += " and locationid = '" + locationid + "'";
            }
            //公司名
            if (!queryParam["SearchName"].IsEmpty())
            {
                string CompanyName = queryParam["SearchName"].ToString();
                strSql += " and (CompanyName  like '%" + CompanyName + "%'  or CompanyAddress  like '%" + CompanyName + "%') ";
            }

            strSql += strOrder;

            return this.BaseRepository().FindList(strSql.ToString());
        }

        /// <summary>
        /// 个人私池
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Ku_CompanyEntity> GetListByUserId(string searchName, string state)
        {
            string strSql = @"select * from Ku_Company where ManageState=1 and ObtainUserId = '" + OperatorProvider.Provider.Current().UserId + "'";

            //公司名
            if (!searchName.IsEmpty())
            {
                strSql += " and (CompanyName  like '%" + searchName + "%'  or CompanyAddress  like '%" + searchName + "%') ";
            }
            //状态
            if (!state.IsEmpty())
            {
                strSql += " and FollowState = '" + state + "'";
            }
            strSql += " ORDER BY ObtainDate desc";

            return this.BaseRepository().FindList(strSql.ToString());
        }


        /// <summary>
        /// 个人中心页面-状态个数
        /// </summary>
        /// <returns>返回列表</returns>
        public DataTable GetFollowState()
        {
            string userId = OperatorProvider.Provider.Current().UserId;
            string strSql = $"SELECT count(1) FROM Ku_Company b WHERE ObtainUserId='{userId}' and FollowState=3 UNION all "
+ $"SELECT count(1) FROM Ku_Company b WHERE ObtainUserId = '{userId}' and FollowState = 2 UNION all "
+ $"SELECT count(1) FROM Ku_Company b WHERE ObtainUserId = '{userId}' and FollowState = 4 UNION all "
+ $"SELECT count(1) FROM Ku_Company b WHERE ObtainUserId = '{userId}' and FollowState = 0";
            return this.BaseRepository().FindTable(strSql);
        }

        /// <summary>
        /// 获取列表自动补全公司名称
        /// </summary>
        /// <param name="CompanyName">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<Ku_CompanyEntity> GetListByName(string CompanyName)
        {
            string des = OperatorProvider.Provider.Current().Description;
            string locationSql = LocationHelper.GetLocationSql(des);
            string strSql = $"select top 5 c.Id,c.CompanyName from Ku_Company c LEFT JOIN {locationSql} l ON l.Id=c.LocationId where ManageState=1 AND LocationId>0 AND c.District!='' ";

            //公司名
            if (!CompanyName.IsEmpty())
            {
                strSql += " and c.CompanyName like '%" + CompanyName + "%'";
            }

            return this.BaseRepository().FindList(strSql.ToString());
        }

        /// <summary>
        /// 获取列表自动补全公司名称(私池)
        /// </summary>
        /// <param name="CompanyName">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<Ku_CompanyEntity> GetListByNameSi(string CompanyName)
        {
            string des = OperatorProvider.Provider.Current().Description;
            string locationSql = LocationHelper.GetLocationSql(des);
            string strSql = $"select top 5 c.CompanyName from Ku_Company c LEFT JOIN {locationSql} l ON l.Id=c.LocationId where ObtainState=1 and ManageState=1 AND LocationId>0 AND c.District!='' ";

            //公司名
            if (!CompanyName.IsEmpty())
            {
                strSql += " and c.CompanyName like '%" + CompanyName + "%'";
            }
            if (!OperatorProvider.Provider.Current().IsSystem)
            {
                string dataAutor = string.Format(OperatorProvider.Provider.Current().DataAuthorize.ReadAutorize, OperatorProvider.Provider.Current().UserId);
                strSql += " and (c.ObtainUserId in (" + dataAutor + "))";
            }
            return this.BaseRepository().FindList(strSql.ToString());
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public Ku_CompanyEntity GetEntity(int? keyValue)
        {
            IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();
            var entity = db.FindEntity<Ku_CompanyEntity>(t => t.Id == keyValue);

            //查看次数+1
            if (entity.SeeTimes == null)
            {
                entity.SeeTimes = 1;
            }
            else
            {
                entity.SeeTimes = entity.SeeTimes + 1;
            }
            this.BaseRepository().Update(entity);

            //同一个人浏览过同一个公司的话，只修改浏览时间，否则添加新的浏览记录
            string userid = OperatorProvider.Provider.Current().UserId;
            var oldSeeEntity = db.FindEntity<Ku_CompanySeeEntity>(t => t.CompanyId == entity.Id && t.SeeUserId == userid);
            if (oldSeeEntity != null)
            {
                oldSeeEntity.SeeDate = DateTime.Now;
                db.Update(oldSeeEntity);
            }
            else
            {
                //创建一条浏览记录
                Ku_CompanySeeEntity seeEntity = new Ku_CompanySeeEntity
                {
                    CompanyId = entity.Id,
                    CompanyName = entity.CompanyName
                };
                seeEntity.Create();
                db.Insert(seeEntity);
            }
            db.Commit();

            return entity;
        }
        #endregion

        #region 验证数据
        /// <summary>
        /// 客户名称不能重复
        /// </summary>
        /// <param name="CompanyName">名称</param>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public bool ExistFullName(string CompanyName, string keyValue)
        {
            var expression = LinqExtensions.True<Ku_CompanyEntity>();
            expression = expression.And(t => t.CompanyName == CompanyName);
            if (!string.IsNullOrEmpty(keyValue))
            {
                expression = expression.And(t => t.Id != int.Parse(keyValue));
            }
            return this.BaseRepository().IQueryable(expression).Count() == 0 ? true : false;
        }
        #endregion

        #region 提交数据


        /// <summary>
        /// 领取公司到个人私池
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public void ObtainForm(int? keyValue)
        {
            if (keyValue != null)
            {
                IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();
                var entity = db.FindEntity<Ku_CompanyEntity>(t => t.Id == keyValue);
                entity.ObtainUserId = OperatorProvider.Provider.Current().UserId;
                entity.ObtainUserName = OperatorProvider.Provider.Current().UserName;
                entity.ObtainDate = DateTime.Now;
                entity.ObtainState = 1;
                this.BaseRepository().Update(entity);
            }
        }

        /// <summary>
        /// 批量领取
        /// </summary>
        /// <param name="dtSource">实体对象</param>
        /// <returns></returns>
        public string BatchObtain(DataTable dtSource)
        {
            IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();
            try
            {
                string resultStr = "";
                for (int i = 0; i < dtSource.Rows.Count; i++)
                {
                    string companyName = dtSource.Rows[i][0].ToString();
                    if (!string.IsNullOrEmpty(companyName))
                    {
                        var entity = db.FindEntity<Ku_CompanyEntity>(t => t.CompanyName == companyName);
                        if (entity == null)
                        {
                            resultStr += companyName + "：未发现此公司\n";
                        }
                        else if (entity.ObtainState==1)
                        {
                            resultStr += companyName + "：已被领取\n";
                        }
                        else
                        {
                            entity.ObtainUserId = OperatorProvider.Provider.Current().UserId;
                            entity.ObtainUserName = OperatorProvider.Provider.Current().UserName;
                            entity.ObtainDate = DateTime.Now;
                            entity.ObtainState = 1;
                            this.BaseRepository().Update(entity);
                        }
                    }
                }
                db.Commit();
                return "操作完毕：\n"+ resultStr;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }


        /// <summary>
        /// 分配
        /// </summary>
        /// <param name="keyValue">客户ids</param>
        /// <param name="ObtainUserId">员工id</param>
        /// <param name="ObtainUserName">员工名称</param>
        public void SaveAssign(int? keyValue, string ObtainUserId, string ObtainUserName)
        {
            if (keyValue != null)
            {
                IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();
                var entity = db.FindEntity<Ku_CompanyEntity>(t => t.Id == keyValue);
                entity.ObtainUserId = ObtainUserId;
                entity.ObtainUserName = ObtainUserName;
                entity.ObtainDate = DateTime.Now;
                entity.ObtainState = 1;
                entity.DeleteMark = 0;//垃圾站还原需改成未删除
                entity.Modify(keyValue);
                this.BaseRepository().Update(entity);
            }

        }

        /// <summary>
        /// 放入公海
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public void ThrowForm(int? keyValue)
        {
            if (keyValue != null)
            {
                IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();
                var entity = db.FindEntity<Ku_CompanyEntity>(t => t.Id == keyValue);
                entity.ObtainUserId = "";
                entity.ObtainUserName = "";//置空获取人
                entity.ObtainState = 0;//未分配
                entity.Modify(keyValue);
                this.BaseRepository().Update(entity);
            }
        }

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
        public void SaveForm(int? keyValue, Ku_CompanyEntity entity)
        {
            IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();
            if (keyValue != null)
            {
                Ku_CompanyEntity oldEntity = db.FindEntity<Ku_CompanyEntity>(t => t.Id == keyValue);
                //真实地址修正注册地址
                if (oldEntity.CompanyAddress != entity.RealAddress && !string.IsNullOrEmpty(entity.RealAddress) && oldEntity.RealAddress != entity.RealAddress)
                {
                    //重新运算地址
                    Ku_LocationEntity locationData = locationService.AddressToLocation(entity.RealAddress);
                    entity = GetCompany(entity, locationData);

                    //减去原坐标数
                    Ku_LocationEntity oldLoactionEntity = db.FindEntity<Ku_LocationEntity>(t => t.Id == oldEntity.LocationId);
                    oldLoactionEntity.Count = oldLoactionEntity.Count - 1;
                    db.Update<Ku_LocationEntity>(oldLoactionEntity);
                }
                entity.Modify(keyValue);
                this.BaseRepository().Update(entity);
            }
            else
            {
                var company_Data = db.FindEntity<Ku_CompanyEntity>(t => t.CompanyName == entity.CompanyName);//公司重复判断                                                                                           
                if (company_Data != null)
                {
                    throw new Exception(entity.CompanyName + " 重复！");
                }
                //地址为空判断                                                                                           
                if (string.IsNullOrEmpty(entity.CompanyAddress))
                {
                    throw new Exception(entity.CompanyName + " 地址为空！");
                }
                //如果存在坐标id，直接从当前坐标添加公司，如果不存在坐标点，则需要从地址转换坐标
                if (entity.LocationId==null)
                {
                    //1.根据地址获得坐标信息
                    Ku_LocationEntity locationData = locationService.AddressToLocation(entity.CompanyAddress);
                    entity = GetCompany(entity, locationData);
                }
                else
                {
                    //现有坐标
                    Ku_LocationEntity locationData = db.FindEntity<Ku_LocationEntity>(t => t.Id == entity.LocationId);
                    locationData.Count = locationData.Count + 1;
                    db.Update(locationData);

                    entity = GetCompany(entity, locationData);

                }
                entity.Create();
                this.BaseRepository().Insert(entity);
            }
        }

        #endregion

        #region 补全公司数据
        /// <summary>
        /// 补全公司数据中的1.楼层房间，2.坐标关联信息
        /// </summary>
        /// <param name="entity">公司实体</param>
        /// <param name="locationData">坐标点实体</param>
        /// <returns></returns>
        public Ku_CompanyEntity GetCompany(Ku_CompanyEntity entity, Ku_LocationEntity locationData)
        {
            //通过地址获取楼层和房间
            if (entity.Floor ==0 && entity.Room ==0 && !string.IsNullOrEmpty(entity.RealAddress))
            {
                string building;
                int floor, room;
                getFloorRoom(entity.RealAddress, out building, out floor, out room);
                entity.Building = building;
                entity.Floor = floor;
                entity.Room = room;
            }

            //添加客户信息表,将插入的位置id和商圈名称，返回给公司
            entity.Province = locationData.Province;
            entity.City = locationData.City;
            entity.District = locationData.District;
            entity.LocationId = locationData.Id;
            entity.RegeoId = locationData.RegeoId;
            entity.RegeoName = locationData.RegeoName;
            return entity;
        }
        #endregion


        #region 通过地址匹配楼层房间号
        /// <summary>
        /// 通过地址匹配楼层房间号
        /// </summary>
        /// <param name="address">地址</param>
        /// <param name="building">楼号</param>
        /// <param name="floor">楼层</param>
        /// <param name="room">房间</param>
        public void getFloorRoom(string address, out string building, out int floor, out int room)
        {
            //通过地址匹配地址中存在的楼层，房间
            building = "";
            floor = 0;
            room = 0;
            Regex r = new Regex(@"\d*号楼"); // 定义一个Regex对象实例
            address = address.Replace('１', '1').Replace('２', '1').Replace('３', '1').Replace('４', '1').Replace('５', '1').Replace('６', '1').Replace('７', '1').Replace('８', '1').Replace('９', '1');
            Match m = r.Match(address); // 在字符串中匹配
            if (m.Success)
            {
                building = m.Value.Replace("号楼", "");
            }

            Regex rr = new Regex(@"\d*室$|\d*$"); // 定义一个Regex对象实例
            Match mm = rr.Match(address); // 在字符串中匹配
            if (mm.Success)
            {
                string fr = mm.Value.Replace("室", "").Replace("号", "");
                if (fr.Length == 3)
                {
                    floor = Convert.ToInt32(fr.Substring(0, 1));
                    room = Convert.ToInt32(fr.Substring(1, 2));
                }
                else if (fr.Length == 4)
                {
                    floor = Convert.ToInt32(fr.Substring(0, 2));
                    room = Convert.ToInt32(fr.Substring(2, 2));
                }
                else if (fr.Length == 5)
                {
                    floor = Convert.ToInt32(fr.Substring(0, 2));
                    room = Convert.ToInt32(fr.Substring(2, 3));
                }
            }

        }
        #endregion


    }
}
