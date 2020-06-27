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
    /// �� �� 6.1
    /// 
    /// �� ������������Ա
    /// �� �ڣ�2017-12-15 15:02
    /// �� ������Ʒ��
    /// </summary>
    public class Pet_TypeController : MvcControllerBase
    {
        private Pet_TypeBLL pos_productbll = new Pet_TypeBLL();

        #region ��ͼ����
        /// <summary>
        /// �б�ҳ��
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Pet_TypeIndex()
        {
            return View();
        }
        /// <summary>
        /// ����+����
        /// </summary>
        /// <returns></returns>
        public ActionResult Pet_TypeIndex2()
        {
            return View();
        }
        /// <summary>
        /// ��ҳ��
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Pet_TypeForm()
        {
            return View();
        }
        /// <summary>
        /// ��ϸҳ��
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Pet_TypeDetail()
        {
            return View();
        }
        #endregion

        #region ��ȡ����
        /// <summary>
        /// �����б� "{\"ParentId\":\"0\"}"
        /// </summary>
        /// <param name="keyword">�ؼ��ֲ�ѯ</param>
        /// <returns>��������Json</returns>
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
                data = data.TreeWhere(t => t.ParentId == parentId, "ItemId");//����ItemId
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
        /// ��ϸ�б�
        /// </summary>
        /// <param name="typeId">����Id</param>
        /// <param name="keyword">�ؼ��ֲ�ѯ</param>
        /// <returns>���������б�Json</returns>
        [HttpGet]
        public ActionResult GetTreeListJson(string typeId, string condition, string keyword)
        {
            var data = pos_productbll.GetList("{\"ParentId\":\"" + typeId + "\"}").OrderBy(t => t.SortCode).ToList();
            if (!string.IsNullOrEmpty(keyword))
            {
                #region ��������ѯ
                switch (condition)
                {
                    case "ItemName":        //��Ʒ��
                        data = data.TreeWhere(t => t.ItemName.Contains(keyword), "ItemId");
                        break;
                    case "ItemCode":      //��Ʒ���
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
        /// ��ȡ�б�
        /// </summary>
        /// <param name="pagination">��ҳ����</param>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>���ط�ҳ�б�Json</returns>
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
        /// ��ȡ�б�
        /// </summary>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>�����б�Json</returns>
        [HttpGet]
        public ActionResult GetListJson(string queryJson)
        {
            var data = pos_productbll.GetList(queryJson);
            return ToJsonResult(data);
        }
        /// <summary>
        /// ��ȡʵ�� 
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns>���ض���Json</returns>
        [HttpGet]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = pos_productbll.GetEntity(keyValue);
            return ToJsonResult(data);
        }
        #endregion

        #region �ύ����
        /// <summary>
        /// ɾ������
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult RemoveForm(string keyValue)
        {
            pos_productbll.RemoveForm(keyValue);
            return Success("ɾ���ɹ���");
        }
        /// <summary>
        /// ��������������޸ģ�
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <param name="entity">ʵ�����</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult SaveForm(string keyValue, Pet_TypeEntity entity)
        {
            pos_productbll.SaveForm(keyValue, entity);
            return Success("�����ɹ���");
        }
        #endregion

        #region ��֤����
        /// <summary>
        /// ��Ŀֵ�����ظ�
        /// </summary>
        /// <param name="ItemValue">��Ŀֵ</param>
        /// <param name="keyValue">����</param>
        /// <param name="itemId">����Id</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ExistItemValue(string ItemValue, string keyValue, string itemId)
        {
            bool IsOk = pos_productbll.ExistItemValue(ItemValue, keyValue, itemId);
            return Content(IsOk.ToString());
        }
        /// <summary>
        /// ��Ŀ�������ظ�
        /// </summary>
        /// <param name="ItemName">��Ŀ��</param>
        /// <param name="keyValue">����</param>
        /// <param name="itemId">����Id</param>
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
