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
    /// �� �� 6.1
    /// 
    /// �� ������������Ա
    /// �� �ڣ�2017-12-02 16:17
    /// �� ����POS_Shop
    /// </summary>
    public class POS_ShopController : MvcControllerBase
    {
        private POS_ShopBLL pos_shopbll = new POS_ShopBLL();

        #region ��ͼ����
        /// <summary>
        /// �б�ҳ��
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult POS_ShopIndex()
        {
            return View();
        }
        /// <summary>
        /// ��ҳ��
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult POS_ShopForm()
        {
            return View();
        }
        #endregion

        #region ��ȡ����
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
        /// ��ȡ�б�
        /// </summary>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>�����б�Json</returns>
        [HttpGet]
        public ActionResult GetListJson(string queryJson)
        {
            var data = pos_shopbll.GetList(queryJson);
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
            var data = pos_shopbll.GetEntity(keyValue);
            if (data.IsSee != 1)
            {
                data.IsSee = 1;
                pos_shopbll.SaveForm(keyValue, data);
            }
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
            pos_shopbll.RemoveForm(keyValue);
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
        public ActionResult SaveForm(string keyValue, POS_ShopEntity entity)
        {
            pos_shopbll.SaveForm(keyValue, entity);
            return Success("�����ɹ���");
        }
        #endregion


        #region ������������

        public FileResult GetFile()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "Resource/ExcelTemplate/";
            string fileName = "����ģ��.xlsx";
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
                ViewBag.error = "�ļ�����Ϊ��";
                return View();
            }
            else
            {
                string filename = Path.GetFileName(file.FileName);
                int filesize = file.ContentLength;//��ȡ�ϴ��ļ��Ĵ�С��λΪ�ֽ�byte
                string fileEx = System.IO.Path.GetExtension(filename);//��ȡ�ϴ��ļ�����չ��
                string NoFileName = System.IO.Path.GetFileNameWithoutExtension(filename);//��ȡ����չ�����ļ���
                int Maxsize = 4000 * 1024;//�����ϴ��ļ������ռ��СΪ4M
                string FileType = ".xls,.xlsx";//�����ϴ��ļ��������ַ���

                FileName = NoFileName + DateTime.Now.ToString("yyyyMMddhhmmss") + fileEx;
                if (!FileType.Contains(fileEx))
                {
                    ViewBag.error = "�ļ����Ͳ��ԣ�ֻ�ܵ���xls��xlsx��ʽ���ļ�";
                    return View();
                }
                if (filesize >= Maxsize)
                {
                    ViewBag.error = "�ϴ��ļ�����4M�������ϴ�";
                    return View();
                }
                string path = AppDomain.CurrentDomain.BaseDirectory + "Resource/ExcelData/";
                savePath = Path.Combine(path, FileName);
                file.SaveAs(savePath);
            }
            

            //excelתDataTable
            DataTable dtSource = ExcelHelper.ExcelImport(savePath);
            //��������TelphoneWash��
            //SqlBulkCopyByDatatable("TelphoneWash", dtSource);
            //һ���в���

            //����������ƣ�����ʱ������ع�
            ViewBag.error = pos_shopbll.BatchAddEntity(dtSource);

            System.Threading.Thread.Sleep(2000);
            return View();
            //return RedirectToAction("TelphoneWashIndex");
        }

        /// <summary>
        /// ��datatable����Ĵ��������������Ƶ����ݿ��У������õ�����������
        /// </summary>
        /// <param name="TableName">Ŀ���</param>
        /// <param name="dt">Դ����</param>
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
                        sqlbulkcopy.ColumnMappings.Add("Telphone", "Telphone");//�Զ����DataTable�����ݿ����ֶ����Ӧ
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
