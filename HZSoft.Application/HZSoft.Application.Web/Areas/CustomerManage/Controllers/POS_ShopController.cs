using HZSoft.Application.Entity.CustomerManage;
using HZSoft.Application.Busines.CustomerManage;
using HZSoft.Util;
using HZSoft.Util.WebControl;
using System.Web.Mvc;
using System.Data;
using System.IO;
using System.Web;
using System;
using HZSoft.Util.Offices;
using System.Configuration;
using System.Data.SqlClient;


namespace HZSoft.Application.Web.Areas.CustomerManage.Controllers
{
    /// <summary>
    /// 版 本 6.1
    /// 
    /// 创 建：超级管理员
    /// 日 期：2017-12-02 16:17
    /// 描 述：POS_Shop
    /// </summary>
    public class POS_ShopController : MvcControllerBase
    {
        private POS_ShopBLL pos_shopbll = new POS_ShopBLL();

        #region 视图功能
        /// <summary>
        /// 列表页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult POS_ShopIndex()
        {
            return View();
        }
        /// <summary>
        /// 表单页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult POS_ShopForm()
        {
            return View();
        }
        #endregion

        #region 获取数据
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
            var data = pos_shopbll.GetPageList(pagination, queryJson);
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
            var data = pos_shopbll.GetList(queryJson);
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
            var data = pos_shopbll.GetEntity(keyValue);
            if (data.IsSee != 1)
            {
                data.IsSee = 1;
                pos_shopbll.SaveForm(keyValue, data);
            }
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
            pos_shopbll.RemoveForm(keyValue);
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
        public ActionResult SaveForm(string keyValue, POS_ShopEntity entity)
        {
            pos_shopbll.SaveForm(keyValue, entity);
            return Success("操作成功。");
        }
        #endregion


        #region 批量导入数据

        public FileResult GetFile()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "Resource/ExcelTemplate/";
            string fileName = "导入模板.xlsx";
            return File(path + fileName, "application/ms-excel", fileName);
        }

        public ActionResult ShopImport()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ShopImport(HttpPostedFileBase filebase)
        {

            HttpPostedFileBase file = Request.Files["files"];
            string FileName;
            string savePath;
            if (file == null || file.ContentLength <= 0)
            {
                ViewBag.error = "文件不能为空";
                return View();
            }
            else
            {
                string filename = Path.GetFileName(file.FileName);
                int filesize = file.ContentLength;//获取上传文件的大小单位为字节byte
                string fileEx = System.IO.Path.GetExtension(filename);//获取上传文件的扩展名
                string NoFileName = System.IO.Path.GetFileNameWithoutExtension(filename);//获取无扩展名的文件名
                int Maxsize = 4000 * 1024;//定义上传文件的最大空间大小为4M
                string FileType = ".xls,.xlsx";//定义上传文件的类型字符串

                FileName = NoFileName + DateTime.Now.ToString("yyyyMMddhhmmss") + fileEx;
                if (!FileType.Contains(fileEx))
                {
                    ViewBag.error = "文件类型不对，只能导入xls和xlsx格式的文件";
                    return View();
                }
                if (filesize >= Maxsize)
                {
                    ViewBag.error = "上传文件超过4M，不能上传";
                    return View();
                }
                string path = AppDomain.CurrentDomain.BaseDirectory + "Resource/ExcelData/";
                savePath = Path.Combine(path, FileName);
                file.SaveAs(savePath);
            }
            

            //excel转DataTable
            DataTable dtSource = ExcelHelper.ExcelImport(savePath);
            //批量插入TelphoneWash表
            //SqlBulkCopyByDatatable("TelphoneWash", dtSource);
            //一行行插入

            //引用事务机制，出错时，事物回滚
            ViewBag.error = pos_shopbll.BatchAddEntity(dtSource);

            System.Threading.Thread.Sleep(2000);
            return View();
            //return RedirectToAction("TelphoneWashIndex");
        }

        /// <summary>
        /// 将datatable里面的大量数据批量复制到数据库中，而不用担心性能问题
        /// </summary>
        /// <param name="TableName">目标表</param>
        /// <param name="dt">源数据</param>
        private void SqlBulkCopyByDatatable(string TableName, DataTable dt)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["BaseDb"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlBulkCopy sqlbulkcopy = new SqlBulkCopy(connectionString, SqlBulkCopyOptions.UseInternalTransaction))
                {
                    try
                    {
                        sqlbulkcopy.DestinationTableName = TableName;
                        sqlbulkcopy.ColumnMappings.Add("Telphone", "Telphone");//自定义的DataTable和数据库表的字段相对应
                        //for (int i = 0; i < dt.Columns.Count; i++)
                        //{
                        //    sqlbulkcopy.ColumnMappings.Add(dt.Columns[i].ColumnName, dt.Columns[i].ColumnName);
                        //}
                        sqlbulkcopy.WriteToServer(dt);
                    }
                    catch (System.Exception ex)
                    {
                        throw ex;
                    }
                }
            }
        }


        #endregion
    }
}
