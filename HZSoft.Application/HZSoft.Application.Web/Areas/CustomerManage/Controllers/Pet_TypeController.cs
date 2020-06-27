using HZSoft.Util;
using HZSoft.Util.WebControl;
using System.Web.Mvc;
using System.Linq;
using System.Collections.Generic;
using HZSoft.Application.Busines.CustomerManage;
using HZSoft.Application.Entity.CustomerManage;

namespace HZSoft.Application.Web.Areas.CustomerManage.Controllers
{
    /// <summary>
    /// 版 本 6.1
    /// 
    /// 创 建：超级管理员
    /// 日 期：2017-12-15 15:02
    /// 描 述：产品表
    /// </summary>
    public class Pet_TypeController : MvcControllerBase
    {
        private Pet_TypeBLL pos_productbll = new Pet_TypeBLL();

        #region 视图功能
        /// <summary>
        /// 列表页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Pet_TypeIndex()
        {
            return View();
        }
        /// <summary>
        /// 分类+详情
        /// </summary>
        /// <returns></returns>
        public ActionResult Pet_TypeIndex2()
        {
            return View();
        }
        /// <summary>
        /// 表单页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Pet_TypeForm()
        {
            return View();
        }
        /// <summary>
        /// 详细页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Pet_TypeDetail()
        {
            return View();
        }
        #endregion

        #region 获取数据
        /// <summary>
        /// 分类列表 "{\"ParentId\":\"0\"}"
        /// </summary>
        /// <param name="keyword">关键字查询</param>
        /// <returns>返回树形Json</returns>
        [HttpGet]
        public ActionResult GetTreeJson(string keyword,string parentId)
        {
            var data = pos_productbll.GetList().OrderBy(t => t.SortCode).ToList();
            if (!string.IsNullOrEmpty(keyword))
            {
                data = data.TreeWhere(t => t.ItemName.Contains(keyword), "ItemId");
            }
            if (!string.IsNullOrEmpty(parentId))
            {
                data = data.TreeWhere(t => t.ParentId == parentId, "ItemId");//主键ItemId
            }
            var treeList = new List<TreeEntity>();
            foreach (Pet_TypeEntity item in data)
            {
                TreeEntity tree = new TreeEntity();
                bool hasChildren = data.Count(t => t.ParentId == item.ItemId) == 0 ? false : true;
                tree.id = item.ItemId;
                tree.text = item.ItemName;
                tree.value = item.ItemCode;
                tree.parentId = item.ParentId;
                tree.isexpand = true;
                tree.complete = true;
                tree.Attribute = "isTree";
                tree.AttributeValue = item.IsTree.ToString();
                tree.hasChildren = hasChildren;
                treeList.Add(tree);
            }
            return Content(treeList.TreeToJson());
        }
        /// <summary>
        /// 明细列表
        /// </summary>
        /// <param name="typeId">分类Id</param>
        /// <param name="keyword">关键字查询</param>
        /// <returns>返回树形列表Json</returns>
        [HttpGet]
        public ActionResult GetTreeListJson(string typeId, string condition, string keyword)
        {
            var data = pos_productbll.GetList("{\"ParentId\":\"" + typeId + "\"}").OrderBy(t => t.SortCode).ToList();
            if (!string.IsNullOrEmpty(keyword))
            {
                #region 多条件查询
                switch (condition)
                {
                    case "ItemName":        //产品名
                        data = data.TreeWhere(t => t.ItemName.Contains(keyword), "ItemId");
                        break;
                    case "ItemCode":      //产品编号
                        data = data.TreeWhere(t => t.ItemCode.Contains(keyword), "ItemId");
                        break;
                    default:
                        break;
                }
                #endregion
            }
            var TreeList = new List<TreeGridEntity>();
            foreach (Pet_TypeEntity item in data)
            {
                TreeGridEntity tree = new TreeGridEntity();
                bool hasChildren = data.Count(t => t.ParentId == item.ParentId) == 0 ? false : true;
                tree.id = item.ItemId;
                tree.parentId = item.ParentId;
                tree.expanded = true;
                tree.hasChildren = hasChildren;
                tree.entityJson = item.ToJson();
                TreeList.Add(tree);
            }
            return Content(TreeList.TreeJson());
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表Json</returns>
        [HttpGet]
        public ActionResult GetPageListJson(Pagination pagination, string queryJson)
        {
            var watch = CommonHelper.TimerStart();
            var data = pos_productbll.GetPageList(pagination, queryJson);
            var jsonData = new
            {
                rows = data,
                total = pagination.total,
                page = pagination.page,
                records = pagination.records,
                costtime = CommonHelper.TimerEnd(watch)
            };
            return ToJsonResult(jsonData);
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表Json</returns>
        [HttpGet]
        public ActionResult GetListJson(string queryJson)
        {
            var data = pos_productbll.GetList(queryJson);
            return ToJsonResult(data);
        }
        /// <summary>
        /// 获取实体 
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>返回对象Json</returns>
        [HttpGet]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = pos_productbll.GetEntity(keyValue);
            return ToJsonResult(data);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult RemoveForm(string keyValue)
        {
            pos_productbll.RemoveForm(keyValue);
            return Success("删除成功。");
        }
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult SaveForm(string keyValue, Pet_TypeEntity entity)
        {
            pos_productbll.SaveForm(keyValue, entity);
            return Success("操作成功。");
        }
        #endregion

        #region 验证数据
        /// <summary>
        /// 项目值不能重复
        /// </summary>
        /// <param name="ItemValue">项目值</param>
        /// <param name="keyValue">主键</param>
        /// <param name="itemId">分类Id</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ExistItemValue(string ItemValue, string keyValue, string itemId)
        {
            bool IsOk = pos_productbll.ExistItemValue(ItemValue, keyValue, itemId);
            return Content(IsOk.ToString());
        }
        /// <summary>
        /// 项目名不能重复
        /// </summary>
        /// <param name="ItemName">项目名</param>
        /// <param name="keyValue">主键</param>
        /// <param name="itemId">分类Id</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ExistItemName(string ItemName, string keyValue, string itemId)
        {
            bool IsOk = pos_productbll.ExistItemName(ItemName, keyValue, itemId);
            return Content(IsOk.ToString());
        }
        #endregion

    }
}
