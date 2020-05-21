using System;
using HZSoft.Application.Code;

namespace HZSoft.Application.Entity.CustomerManage
{
    /// <summary>
    /// 版 本
    /// 
    /// 创 建：超级管理员
    /// 日 期：2018-03-26 11:54
    /// 描 述：坐标库
    /// </summary>
    public class Ku_LocationEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// 主键
        /// </summary>
        /// <returns></returns>
        public int? Id { get; set; }
        /// <summary>
        /// 匹配级别
        /// </summary>
        /// <returns></returns>
        public string _Level { get; set; }
        /// <summary>
        /// 是否逆地理
        /// </summary>
        /// <returns></returns>
        public int? IsRegeo { get; set; }
        /// <summary>
        /// 热点圈Id
        /// </summary>
        /// <returns></returns>
        public string RegeoId { get; set; }
        /// <summary>
        /// 热点圈
        /// </summary>
        /// <returns></returns>
        public string RegeoName { get; set; }
        /// <summary>
        /// 个数
        /// </summary>
        /// <returns></returns>
        public int? Count { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        /// <returns></returns>
        public string Address { get; set; }
        /// <summary>
        /// 重量
        /// </summary>
        /// <returns></returns>
        public string Poiweight { get; set; }
        /// <summary>
        /// 面积
        /// </summary>
        /// <returns></returns>
        public string Area { get; set; }
        /// <summary>
        /// POI类型
        /// </summary>
        /// <returns></returns>
        public string TypeCode { get; set; }
        /// <summary>
        /// POI类型名称
        /// </summary>
        /// <returns></returns>
        public string TypeName { get; set; }
        /// <summary>
        /// 省份
        /// </summary>
        /// <returns></returns>
        public string Province { get; set; }
        /// <summary>
        /// 城市
        /// </summary>
        /// <returns></returns>
        public string City { get; set; }
        /// <summary>
        /// 区号
        /// </summary>
        /// <returns></returns>
        public string CityCode { get; set; }
        /// <summary>
        /// 区县
        /// </summary>
        /// <returns></returns>
        public string District { get; set; }
        /// <summary>
        /// 区域编码
        /// </summary>
        /// <returns></returns>
        public string AdCode { get; set; }
        /// <summary>
        /// 乡镇
        /// </summary>
        /// <returns></returns>
        public string Township { get; set; }
        /// <summary>
        /// POI坐标
        /// </summary>
        /// <returns></returns>
        public string Location { get; set; }
        /// <summary>
        /// 高德经度
        /// </summary>
        /// <returns></returns>
        public decimal? wxLon { get; set; }
        /// <summary>
        /// 高德纬度
        /// </summary>
        /// <returns></returns>
        public decimal? wxLat { get; set; }
        /// <summary>
        /// 百度经度
        /// </summary>
        /// <returns></returns>
        public decimal? bdLon { get; set; }
        /// <summary>
        /// 百度经度
        /// </summary>
        /// <returns></returns>
        public decimal? bdLat { get; set; }
        /// <summary>
        /// 店铺照片
        /// </summary>
        /// <returns></returns>
        public string Picture { get; set; }
        /// <summary>
        /// 删除标记
        /// </summary>
        /// <returns></returns>
        public int? DeleteMark { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        /// <returns></returns>
        public string Description { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>
        /// <returns></returns>
        public DateTime? CreateDate { get; set; }
        /// <summary>
        /// 创建用户主键
        /// </summary>
        /// <returns></returns>
        public string CreateUserId { get; set; }
        /// <summary>
        /// 创建用户
        /// </summary>
        /// <returns></returns>
        public string CreateUserName { get; set; }
        /// <summary>
        /// 修改日期
        /// </summary>
        /// <returns></returns>
        public DateTime? ModifyDate { get; set; }
        /// <summary>
        /// 修改用户主键
        /// </summary>
        /// <returns></returns>
        public string ModifyUserId { get; set; }
        /// <summary>
        /// 修改用户
        /// </summary>
        /// <returns></returns>
        public string ModifyUserName { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public override void Create()
        {
            //this.Id = Guid.NewGuid().ToString();
            this.CreateDate = DateTime.Now;
            this.CreateUserId = OperatorProvider.Provider.Current().UserId;
            this.CreateUserName = OperatorProvider.Provider.Current().UserName;
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(int? keyValue)
        {
            this.Id = keyValue;
            this.ModifyDate = DateTime.Now;
            this.ModifyUserId = OperatorProvider.Provider.Current().UserId;
            this.ModifyUserName = OperatorProvider.Provider.Current().UserName;
        }
        #endregion
    }
}