using HZSoft.Application.Busines.BaseManage;
using HZSoft.Application.Busines.WeChatManage;
using HZSoft.Application.Code;
using HZSoft.Application.Entity.BaseManage;
using HZSoft.Application.Entity.WeChatManage;
using HZSoft.Application.Web.Utility;
using HZSoft.Util;
using HZSoft.Util.Extension;
using HZSoft.Util.WebControl;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace HZSoft.Application.Web.Areas.WeChatManage.Controllers
{

    /// <summary>
    /// 实名认证
    /// </summary>
    public class CertificationController : BaseWxUserController
    {
        WeChat_UsersBLL wechatUserBll = new WeChat_UsersBLL();
        UserBLL userBLL = new UserBLL();
        POS_ApplyBLL posApplyBll = new POS_ApplyBLL();
        #region 视图功能

        /// <summary>
        /// 实名认证页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index()
        {
            //1.授权登录
            string returnUrl = Request.Url.ToString();
            string url = WeixinAuth(returnUrl);
            if (!string.IsNullOrEmpty(url))
                return Redirect(url);
            WeChat_UsersEntity entity = wechatUserBll.GetEntity(CurrentWxUser.OpenId);
            if (entity != null)
            {
                ViewBag.UserName = entity.UserName;
                ViewBag.UserId = entity.UserId;
            }
            else
            {
                ViewBag.UserName = "";
                ViewBag.UserId = "";
            }
            return View();
        }

        /// <summary>
        /// 添加水印
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddWater()
        {
            string Message = "";
            if (Request.Files.Count > 0)
            {
                HttpPostedFile file = System.Web.HttpContext.Current.Request.Files[0];
                if (file.ContentLength > 0)
                {
                    string fileExt = file.FileName.Substring(file.FileName.LastIndexOf('.'));//后缀
                    try
                    {
                        string fileGuid = Guid.NewGuid().ToString();
                        string uploadDate = DateTime.Now.ToString("yyyyMMdd");
                        string dir = string.Format("/Resource/DocumentFile/Certification/{0}/", uploadDate);
                        if (Directory.Exists(Server.MapPath(dir)) == false)//如果不存在就创建file文件夹
                        {
                            Directory.CreateDirectory(Server.MapPath(dir));
                        }
                        //Directory.CreateDirectory(Path.GetDirectoryName(dir));
                        string newfileName = Guid.NewGuid().ToString();
                        //原图
                        string fullDir1 = dir + newfileName + fileExt;
                        string imgFilePath = Request.MapPath(fullDir1);
                        file.SaveAs(imgFilePath);
                        //水印
                        string imgShui = dir + newfileName + "_Water" + fileExt;
                        string imgWaterPath = Request.MapPath(imgShui);

                        //ImgWater.zzsTextWater(imgYaPath, "仅用于开卡使用", imgWaterPath, 0.3f, 0.9f, 150);
                        using (FileStream fromFile = new FileStream(imgFilePath, FileMode.Open))
                        {
                            ImageHelper.ZoomAuto(fromFile, imgWaterPath, 1130, 1130, "仅用于开卡使用", "", true);
                        }
                        

                        //删除原图节省空间
                        System.IO.File.Delete(imgFilePath);

                        return Content(new JsonMessage { Success = true, Code = "0", Message = imgShui }.ToString());

                    }
                    catch (Exception ex)
                    {
                        Message = HttpUtility.HtmlEncode(ex.Message);
                        return Content(new JsonMessage { Success = false, Code = "-1", Message = Message }.ToString());
                    }
                }
                Message = "请选择要上传的文件！";
                return Content(new JsonMessage { Success = false, Code = "-1", Message = Message }.ToString());
            }
            Message = "请选择要上传的文件！";
            return Content(new JsonMessage { Success = false, Code = "-1", Message = Message }.ToString());
        }

        /// <summary>
        /// 提交实名认证
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Audit(POS_ApplyEntity entity)
        {
            //推荐人
            if (string.IsNullOrEmpty(entity.RecommenderId) && !string.IsNullOrEmpty(entity.Recommender))
            {
                UserEntity user = userBLL.GetEntityByName(entity.Recommender);
                if (user != null)
                {
                    entity.RecommenderId = user.UserId;
                    //更新微信用户表中的姓名和ID
                    
                    if (!string.IsNullOrEmpty(CurrentWxUser.OpenId))
                    {
                        WeChat_UsersEntity wechatUser = wechatUserBll.GetEntity(CurrentWxUser.OpenId);
                        wechatUser.UserId = user.UserId;
                        wechatUser.UserName = entity.Recommender;
                        wechatUserBll.SaveForm(CurrentWxUser.OpenId, wechatUser);
                    }
                }
            }
            entity.PassMark = 0;
            entity.CreateTime = DateTime.Now;
            //插入实名认证表
            posApplyBll.SaveForm("", entity);


            //通知指定的微信客服
            //#region 获取access_token
            //string apiurl = "https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid=wx24e47efa56c2e554&secret=1f8de99c6304d13e5a65efa418638ee4";
            //WebRequest req = WebRequest.Create(@apiurl);
            //req.Method = "POST";
            //WebResponse response = req.GetResponse();
            //Stream stream = response.GetResponseStream();
            //Encoding encode = Encoding.UTF8;
            //StreamReader reader = new StreamReader(stream, encode);
            //string detail = reader.ReadToEnd();
            //var jd = JsonConvert.DeserializeObject<WXApi>(detail);
            //string token = (String)jd.access_token;
            //#endregion

            //string url = "https://api.weixin.qq.com/cgi-bin/message/custom/send?access_token=" + token;

            //string touser = ConfigurationManager.AppSettings["touser"];
            //string message = "{\"touser\":\"" + touser + "\"," +
            //    "\"msgtype\":\"text\"," +
            //    "\"text\": " +
            //    "{\"content\":\"有客户提交了实名认证资料" +
            //    "\n手机号：" + entity.mobileNumber +
            //    "\n客户名称：" + entity.custName +
            //    "\n身份证：" + entity.custCertCode +
            //    "\n地址：" + entity.custCertAddress +
            //    " \"} }";
            //string str = GetResponseData(message, @url);
            return Content("true");

        }




        // 百度云中开通对应服务应用的 API Key 建议开通应用的时候多选服务
        private static string clientId = "MrwmwTA0ySdsuSUHcLqYg9Ti";
        // 百度云中开通对应服务应用的 Secret Key
        private static string clientSecret = "EeDVlPwBeOPxeGnrYyGrVhdNpbI7SUOp";

        /// <summary>
        /// 文字识别
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult IDFront(string smrz_front)
        {
            string idcardStr = Idcard(Request.MapPath(smrz_front));

            var client = new Baidu.Aip.Ocr.Ocr(clientId, clientSecret);
            var image = System.IO.File.ReadAllBytes(Request.MapPath(smrz_front));

            // 身份证正面识别
            Dictionary<string, object> myDictionary = new Dictionary<string, object>();
            myDictionary.Add("detect_direction", "true");//是否检测图像朝向
            myDictionary.Add("accuracy", "high");//精准度，精度越高，速度越慢
            var result = client.IdCardFront(image, myDictionary);

            return Content(result.ToString());

        }

        /// <summary>
        /// 百度证件识别接口
        /// </summary>
        /// <param name="imgPath"></param>
        /// <returns></returns>
        public static string Idcard(string imgPath)
        {
            var client = new Baidu.Aip.Ocr.Ocr(clientId, clientSecret);
            var image = System.IO.File.ReadAllBytes(imgPath);

            // 身份证正面识别
            Dictionary<string, object> myDictionary = new Dictionary<string, object>();
            myDictionary.Add("detect_direction", "true");//是否检测图像朝向
            myDictionary.Add("accuracy", "high");//精准度，精度越高，速度越慢
            var result = client.IdCardFront(image, myDictionary);
            return result.ToString();
        }

        /// <summary>
        /// 文字识别
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult IDBack(string smrz_back)
        {
            var client = new Baidu.Aip.Ocr.Ocr(clientId, clientSecret);
            var image = System.IO.File.ReadAllBytes(Request.MapPath(smrz_back));
            // 身份证背面识别
            Dictionary<string, object> myDictionary = new Dictionary<string, object>();
            myDictionary.Add("detect_direction", "true");//是否检测图像朝向
            myDictionary.Add("accuracy", "high");//精准度，精度越高，速度越慢
            var resultBei = client.IdCardBack(image, myDictionary);
            return Content(resultBei.ToString());

        }
        /// <summary>
        /// 文字识别
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CreditFront(string credit_front)
        {
            var client = new Baidu.Aip.Ocr.Ocr(clientId, clientSecret);
            var image = System.IO.File.ReadAllBytes(Request.MapPath(credit_front));
            // 信用卡正面
            Dictionary<string, object> myDictionary = new Dictionary<string, object>();
            myDictionary.Add("detect_direction", "true");//是否检测图像朝向
            myDictionary.Add("accuracy", "high");//精准度，精度越高，速度越慢
            var resultBei = client.BankCard(image, myDictionary);
            return Content(resultBei.ToString());

        }



        /// <summary>
        /// 返回JSon数据
        /// </summary>
        /// <param name="JSONData">要处理的JSON数据</param>
        /// <param name="Url">要提交的URL</param>
        /// <returns>返回的JSON处理字符串</returns>
        public static string GetResponseData(string JSONData, string Url)
        {
            string strResult = "";
            if (JSONData != "")
            {
                byte[] bytes = Encoding.UTF8.GetBytes(JSONData);
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
                request.Method = "POST";
                request.ContentLength = bytes.Length;
                request.ContentType = "json";
                Stream reqstream = request.GetRequestStream();
                reqstream.Write(bytes, 0, bytes.Length);
                //声明一个HttpWebRequest请求
                request.Timeout = 90000;
                //设置连接超时时间
                request.Headers.Set("Pragma", "no-cache");
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream streamReceive = response.GetResponseStream();
                Encoding encoding = Encoding.UTF8;
                StreamReader streamReader = new StreamReader(streamReceive, encoding);
                strResult = streamReader.ReadToEnd();
                streamReceive.Dispose();
                streamReader.Dispose();
            }
            return strResult;
        }
        /// <summary>
        /// 
        /// </summary>
        public class WXApi
        {
            /// <summary>
            /// 
            /// </summary>
            public string access_token { set; get; }
        }

        #endregion

    }
}
