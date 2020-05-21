using HZSoft.Application.Busines;
using HZSoft.Application.Entity.ReportManage;
using HZSoft.Util;
using HZSoft.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using HZSoft.Application.Busines.ReportManage;
using HZSoft.Application.Entity.AuthorizeManage;
using HZSoft.Application.Busines.AuthorizeManage;
using HZSoft.Application.Code;

namespace HZSoft.Application.Web.Areas.ReportManage.Controllers
{
    /// <summary>
    /// 版 本 6.1
    /// 
    /// 创建人：刘晓雷
    /// 日 期：2016.1.14 14:27
    /// 描 述：报表管理
    /// </summary>
    public class ReportController : MvcControllerBase
    {
        RptTempBLL rptTempBLL = new RptTempBLL();
        ModuleBLL modulebll = new ModuleBLL();

        #region 视图功能
        /// <summary>
        /// 报表管理页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 报表表单页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult ReportGuide()
        {
            return View();
        }
        /// <summary>
        /// 报表预览
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult ReportPreview()
        {
            return View();
        }
        #endregion

        #region 获取数据
        /// <summary>
        /// 获得报表列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表Json</returns>
        public ActionResult GetListJson(string queryJson)
        {
            return Content(rptTempBLL.GetList(queryJson).ToJson());
        }
        /// <summary>
        /// 获得报表实体 
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>返回对象Json</returns>
        [HttpGet]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = rptTempBLL.GetEntity(keyValue);
            return Content(data.ToJson());
        }
        /// <summary>
        /// 获得报表数据 
        /// </summary>
        /// <param name="reportId">报表主键值</param>
        /// <returns>返回对象Json</returns>
        [HttpGet]
        public ActionResult GetReportJson(string reportId)
        {
            var reportJson = rptTempBLL.GetReportData(reportId);
            return Content(reportJson);
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
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult RemoveForm(string keyValue)
        {
            rptTempBLL.RemoveForm(keyValue);
            return Success("删除成功。");
        }
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="tempJson">对象Json</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult SaveForm(string keyValue, string tempJson)
        {
            dynamic RptTempJson = tempJson.ToJson();
            RptTempEntity rptTempEntity = new RptTempEntity();
            ModuleEntity moduleEntity = new ModuleEntity();
            rptTempEntity.EnCode = RptTempJson.EnCode;
            rptTempEntity.Description = RptTempJson.Description;
            rptTempEntity.TempType = RptTempJson.TempType;
            rptTempEntity.FullName = RptTempJson.FullName;
            rptTempEntity.TempCategory = RptTempJson.TempCategory;
            StringBuilder rptJson = new StringBuilder();
            rptJson.Append("{");
            rptJson.AppendFormat("    \"title\":\"{0}\",", RptTempJson.title);//标题
            rptJson.AppendFormat("    \"sqlString\":\"{0}\",", RptTempJson.sqlString);
            rptJson.AppendFormat("    \"ParentId\":\"{0}\",", RptTempJson.ParentId);
            rptJson.AppendFormat("    \"Icon\":\"{0}\",", RptTempJson.Icon);
            rptJson.AppendFormat("    \"Description\":\"{0}\",", RptTempJson.Description);
            rptJson.AppendFormat("    \"listSqlString\":\"{0}\"", RptTempJson.listSqlString);
            rptJson.Append(" }"); rptJson.Replace("\n", "");
            rptTempEntity.ParamJson = rptJson.ToString();
            string parentId = RptTempJson.ParentId;
            if (!string.IsNullOrEmpty(parentId))
            {
                moduleEntity.Create();
                moduleEntity.ParentId = parentId;
                moduleEntity.Icon = RptTempJson.Icon;
                moduleEntity.Description = RptTempJson.Description;
                moduleEntity.IsMenu = 1;
                moduleEntity.FullName = rptTempEntity.FullName;
                moduleEntity.EnCode = rptTempEntity.EnCode;
                moduleEntity.EnabledMark = 1;
                moduleEntity.Target = "iframe";
                moduleEntity.SortCode = modulebll.GetSortCode();
            }
            rptTempBLL.SaveForm(keyValue, rptTempEntity, moduleEntity);
            return Success("操作成功。");
        }
        #endregion


        #region 地图定位展示
        /// <summary>
        /// 区县地图数据
        /// </summary>
        /// <returns></returns>
        public ActionResult dms_area()
        {
            string des = OperatorProvider.Provider.Current().Description;
            if (string.IsNullOrEmpty(des))
            {
                return Content("当前账户没设置区县范围，请联系管理员！");
            }
            string center = "118.362978,35.109642";
            if (des.IndexOf('|') > 0)
            {
                ViewBag.areaCenter = center;
            }
            else
            {
                switch (des)
                {
                    case "兰山区":
                        center = "118.312243, 35.174845";
                        break;
                    case "罗庄区":
                        center = "118.297279, 34.964343";
                        break;
                    case "河东区":
                        center = "118.517311, 35.127031";
                        break;
                    case "沂南县":
                        center = "118.417586, 35.536723";
                        break;
                    case "郯城县":
                        center = "118.324431, 34.649855";
                        break;
                    case "沂水县":
                        center = "118.609358, 35.914369";
                        break;
                    case "兰陵县":
                        center = "118.007509, 34.86262";
                        break;
                    case "费县":
                        center = "117.985838, 35.254971";
                        break;
                    case "平邑县":
                        center = "117.682448, 35.43425";
                        break;
                    case "莒南县":
                        center = "118.890079, 35.213123";
                        break;
                    case "蒙阴县":
                        center = "118.036742, 35.74744";
                        break;
                    case "临沭县":
                        center = "118.659445, 34.885484";
                        break;
                    default:
                        center = "118.338635, 35.064204";
                        break;
                }

                ViewBag.areaCenter = center;
            }
            return View();
        }
        /// <summary>
        /// 区县
        /// </summary>
        /// <returns></returns>
        public ActionResult GetAreaData(string queryJson)
        {
            var dt = rptTempBLL.GetAreaData(queryJson);
            return Content(dt.ToJson());
        }
        /// <summary>
        /// 区县地图数据
        /// </summary>
        /// <returns></returns>
        public ActionResult dms_radius()
        {
            string des = OperatorProvider.Provider.Current().Description;
            if (string.IsNullOrEmpty(des))
            {
                return Content("当前账户没设置中心点坐标，请联系管理员！");
            }
            else if (des.IndexOf(',') <= 0)
            {
                return Content("当前账户没坐标格式不正确，请联系管理员！");
            }
            else if (des.IndexOf('|') > 0)
            {
                ViewBag.roundCenter = "118.362978,35.109642"; 
            }
            else
            {
                ViewBag.roundCenter = des;
            }

            return View();
        }
        /// <summary>
        /// 区县
        /// </summary>
        /// <returns></returns>
        public ActionResult GetRadiusData(string queryJson)
        {
            var dt = rptTempBLL.GetRadiusData(queryJson);
            return Content(dt.ToJson());
        }
        #endregion
    }
}
