using HZSoft.Application.Entity.CustomerManage;
using HZSoft.Application.IService.CustomerManage;
using HZSoft.Data.Repository;
using HZSoft.Util.WebControl;
using System.Collections.Generic;
using System.Linq;

namespace HZSoft.Application.Service.CustomerManage
{
    /// <summary>
    /// 版 本 6.1
    /// 
    /// 创 建：超级管理员
    /// 日 期：2020-05-21 14:42
    /// 描 述：宠物
    /// </summary>
    public class PetService : RepositoryFactory<PetEntity>, PetIService
    {
        private Pet_MasterService masterService = new Pet_MasterService();
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<PetEntity> GetPageList(Pagination pagination, string queryJson)
        {
            return this.BaseRepository().FindList(pagination);
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<PetEntity> GetList(string queryJson)
        {
            return this.BaseRepository().IQueryable().ToList();
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public PetEntity GetEntity(int? keyValue)
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
        public void SaveForm(int? keyValue, PetEntity entity)
        {
            IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();
            if (keyValue != null)
            {
                entity.Modify(keyValue);
                this.BaseRepository().Update(entity);
            }
            else
            {
                entity.Create();
                var masterEntity = masterService.GetEntity(entity.MasterId);
                if (masterEntity!=null)
                {
                    masterEntity.PetsCount += 1;//主人表，宠物数量+1
                    masterEntity.PetsName += ","+entity.SubClassName;//主人表，添加宠物名称
                    db.Update(masterEntity);
                    db.Commit();
                }
                this.BaseRepository().Insert(entity);
            }
        }
        #endregion
    }
}
