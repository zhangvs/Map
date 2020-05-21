using HZSoft.Application.Entity.BaseManage;
using HZSoft.Application.Entity.CustomerManage;
using HZSoft.Application.IService.CustomerManage;
using HZSoft.Application.IService.SystemManage;
using HZSoft.Application.Service.SystemManage;
using HZSoft.Application.Service.BaseManage;
using HZSoft.Data.Repository;
using HZSoft.Util;
using HZSoft.Util.Extension;
using HZSoft.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Linq;
using HZSoft.Application.Code;

namespace HZSoft.Application.Service.CustomerManage
{
    /// <summary>
    /// 版 本 6.1
    /// 
    /// 创 建：超级管理员
    /// 日 期：2017-12-15 16:52
    /// 描 述：销售合同表
    /// </summary>
    public class Sales_ContractService : RepositoryFactory, Sales_ContractIService
    {
        private ICodeRuleService coderuleService = new CodeRuleService();
        POS_ProductService prooductService = new POS_ProductService();
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<Sales_ContractEntity> GetPageList(Pagination pagination, string queryJson)
        {
            var expression = LinqExtensions.True<Sales_ContractEntity>();
            var queryParam = queryJson.ToJObject();
            string strSql = "select * from Sales_Contract where 1=1";
            //单据日期
            if (!queryParam["StartTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
            {
                DateTime startTime = queryParam["StartTime"].ToDate();
                DateTime endTime = queryParam["EndTime"].ToDate().AddDays(1);
                strSql += " and CreateTime >= '" + startTime + "' and CreateTime < '" + endTime + "'";
            }
            //客户名
            if (!queryParam["CustomerName"].IsEmpty())
            {
                string CustomerName = queryParam["CustomerName"].ToString();
                strSql += " and CustomerName = '" + CustomerName + "'";
            }
            //客户公司名
            if (!queryParam["CustomerCompany"].IsEmpty())
            {
                string CustomerCompany = queryParam["CustomerCompany"].ToString();
                strSql += " and CustomerCompany = '" + CustomerCompany + "'";
            }
            //状态
            if (!queryParam["State"].IsEmpty())
            {
                string State = queryParam["State"].ToString();
                strSql += " and State = '" + State + "'";
            }

            //销售人
            if (!queryParam["UserId"].IsEmpty())
            {
                string UserId = queryParam["UserId"].ToString();
                strSql += " and UserId = '" + UserId + "'";
            }
            else
            {
                if (!OperatorProvider.Provider.Current().IsSystem)
                {
                    string dataAutor = string.Format(OperatorProvider.Provider.Current().DataAuthorize.ReadAutorize, OperatorProvider.Provider.Current().UserId);
                    strSql += " and (UserId in (" + dataAutor + "))";
                }
            }

            return this.BaseRepository().FindList<Sales_ContractEntity>(strSql.ToString(), pagination);
        }
        // <summary>
        // 获取列表
        // </summary>
        // <param name = "pagination" > 分页 </ param >
        // < param name="queryJson">查询参数</param>
        // <returns>返回分页列表</returns>
        public IEnumerable<Sales_ContractEntity> GetList(string queryJson)
        {
            var expression = LinqExtensions.True<Sales_ContractEntity>();
            var queryParam = queryJson.ToJObject();

            string strSql = "select * from Sales_Contract where 1 = 1 ";
            string strOrder = " ORDER BY CreateDate desc";

            //店铺名
            if (!queryParam["SearchName"].IsEmpty())
            {
                string SearchName = queryParam["SearchName"].ToString();
                strSql += " and CustomerName = '" + SearchName + "'";
            }
            //员工Id
            if (!OperatorProvider.Provider.Current().IsSystem)
            {
                string dataAutor = string.Format(OperatorProvider.Provider.Current().DataAuthorize.ReadAutorize, OperatorProvider.Provider.Current().UserId);
                strSql += " and (UserId in (" + dataAutor + ") or UserId is null)";
            }
            strSql += strOrder;

            return this.BaseRepository().FindList<Sales_ContractEntity>(strSql.ToString());
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public Sales_ContractEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity<Sales_ContractEntity>(keyValue);
        }
        /// <summary>
        /// 获取子表详细信息
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public IEnumerable<Sales_Contract_ItemEntity> GetDetails(string keyValue)
        {
            return this.BaseRepository().FindList<Sales_Contract_ItemEntity>("select * from Sales_Contract_Item where ContractId='" + keyValue + "'");
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void RemoveForm(string keyValue)
        {
            IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();
            try
            {
                db.Delete<Sales_ContractEntity>(keyValue);
                db.Delete<Sales_Contract_ItemEntity>(t => t.ContractId.Equals(keyValue));
                db.Commit();
            }
            catch (Exception)
            {
                db.Rollback();
                throw;
            }
        }
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <param name="entryList">明细对象</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, Sales_ContractEntity entity, List<Sales_Contract_ItemEntity> entryList)
        {
            IRepository db = this.BaseRepository().BeginTrans();
            try
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    //主表
                    entity.Modify(keyValue);
                    db.Update(entity);

                    //明细
                    db.Delete<Sales_Contract_ItemEntity>(t => t.ContractId.Equals(keyValue));

                    foreach (Sales_Contract_ItemEntity item in entryList)
                    {
                        item.Create();
                        item.ContractId = entity.Id;
                        db.Insert(item);
                        //出库才减库存
                        if (entity.Status == 1)
                        {
                            MinusWareGoods(db, item);
                        }
                    }
                    
                }
                else
                {
                    //主表
                    entity.Create();
                    db.Insert(entity);

                    //出库再操作，初始化余量主表
                    if (entity.Status == 1)
                    {
                        Sale_CustomerEntity saleCustomer = db.FindEntity<Sale_CustomerEntity>(t => t.CustomerId.Equals(entity.CustomerId));

                        if (saleCustomer != null)
                        {
                            saleCustomer.SumTotalAmount = saleCustomer.SumTotalAmount + entity.TotalAmount;
                            saleCustomer.SumTotalCount = saleCustomer.SumTotalCount + entity.TotalCount;
                            saleCustomer.ModifyUserId = entity.UserId;
                            saleCustomer.ModifyUserName = entity.UserName;
                            saleCustomer.Modify(saleCustomer.CustomerId);
                            db.Update(saleCustomer);
                        }
                        else
                        {
                            Sale_CustomerEntity saleCustomerEntity = new Sale_CustomerEntity()
                            {
                                CustomerId = entity.CustomerId,
                                CustomerCompany = entity.CustomerCompany,
                                SumTotalAmount = entity.TotalAmount,
                                SumTotalCount = entity.TotalCount,
                                CreateUserId = entity.UserId,
                                CreateUserName = entity.UserName,
                            };
                            saleCustomerEntity.Create();
                            db.Insert(saleCustomerEntity);
                        }
                    }




                    //明细
                    int sort = 0;
                    foreach (Sales_Contract_ItemEntity item in entryList)
                    {
                        item.Sort=sort++;
                        item.Create();
                        item.ContractId = entity.Id;
                        db.Insert(item);
                        //出库才减库存
                        if (entity.Status==1)
                        {
                            //出库才减库存
                            MinusWareGoods(db, item);

                            #region 余量信息
                            //出库才初始化余量子表
                            Sale_Customer_ItemEntity saleCustomerItem = db.FindEntity<Sale_Customer_ItemEntity>(t => t.CustomerId.Equals(entity.CustomerId) && t.ProductId.Equals(item.ProductId));
                            //商品已经进过一次货，在上次进货基础上累加总进货量
                            if (saleCustomerItem!=null)
                            {
                                saleCustomerItem.UnitPrice = item.UnitPrice;
                                saleCustomerItem.SumAmount = saleCustomerItem.SumAmount + item.Amount;
                                saleCustomerItem.SumCount = saleCustomerItem.SumCount + item.Count;
                                saleCustomerItem.Modify(saleCustomerItem.Id);
                                db.Update(saleCustomerItem);
                            }
                            else
                            {
                                //第一次进货，新增此商品
                                Sale_Customer_ItemEntity saleCustomerItemEntity = new Sale_Customer_ItemEntity()
                                {
                                    CustomerId = entity.CustomerId,
                                    ProductId = item.ProductId,
                                    ProductCode = item.ProductCode,
                                    ProductName = item.ProductName,
                                    UnitPrice = item.UnitPrice,
                                    SumAmount = item.Amount,
                                    SumCount = item.Count,
                                    Sort = item.Sort,
                                };
                                saleCustomerItemEntity.Create();
                                db.Insert(saleCustomerItemEntity);
                            }

                            #endregion
                        }

                    }
                    //占用单据号
                    coderuleService.UseRuleSeed(SystemInfo.CurrentModuleId, db);
                }
                db.Commit();
            }
            catch (Exception ex)
            {
                db.Rollback();
                throw;
            }
        }

        private static readonly object _locker = new object(); // 互斥锁

        /// <summary>
        /// 入库存操作
        /// </summary>
        /// <param name="db"></param>
        /// <param name="item"></param>
        private static void AddWareGoods(IRepository db, Sales_Contract_ItemEntity item)
        {
            lock (_locker)
            {
                POS_ProductEntity op = db.FindEntity<POS_ProductEntity>(t => t.Id.Equals(item.ProductId));
                if (op != null)
                {
                    op.Stock += item.Count;
                    db.Update(op);
                }
                else
                {
                    throw new Exception(string.Format("系统中不存在：{0}，请先维护该产品。", item.ProductName));
                }
            }
        }

        /// <summary>
        /// 出库存操作
        /// </summary>
        /// <param name="db"></param>
        /// <param name="item"></param>
        private static void MinusWareGoods(IRepository db, Sales_Contract_ItemEntity item)
        {
            lock (_locker)
            {
                POS_ProductEntity op = db.FindEntity<POS_ProductEntity>(t => t.Id.Equals(item.ProductId));
                if (op != null)
                {
                    op.Stock -= item.Count;
                    if (op.Stock >= 0)
                    {
                        db.Update(op);
                    }
                    else
                    {
                        throw new Exception(string.Format("仓库存货库存不足，存货信息:{0}, 存货数量：{1}， 出库数量：{2}", item.ProductName, op.Stock, item.Count));
                    }
                }
                else
                {
                    throw new Exception(string.Format("仓库存货库存不足，存货信息:{0}, 存货数量：{1}， 出库数量：{2}", item.ProductName, op.Stock, item.Count));
                }
            }
        }
        #endregion
    }
}
