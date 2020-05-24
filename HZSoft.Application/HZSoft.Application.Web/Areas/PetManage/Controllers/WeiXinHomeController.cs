using HZSoft.Application.Entity.WeChatManage;
using HZSoft.Application.Web.Utility;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Net;
using HZSoft.Application.Code;
using HZSoft.Application.Web.Areas.WeChatManage.Controllers;

namespace HZSoft.Application.Web.Areas.PetManage.Controllers
{

    /// <summary>
    /// 主界面
    /// </summary>
    [HandlerWXLoginAttribute(LoginMode.Enforce)]
    public class WeiXinHomeController : BaseWxUserController
    {
        /// <summary>
        /// 设置开机画面
        /// </summary>
        /// <param name="urlstr"></param>
        /// <returns></returns>
        public ActionResult Index(string urlstr)
        {
            //默认调转到附近的热点界面
            if (string.IsNullOrEmpty(urlstr))
            {
                urlstr = Url.Action("MasterMap", "Master");
            }
            //2.定位
            var jssdkUiPackage = Senparc.Weixin.MP.Helpers.JSSDKHelper.GetJsSdkUiPackage(WeixinConfig.AppID, WeixinConfig.AppSecret, Request.Url.AbsoluteUri.Split('#')[0]);
            ViewBag.JSSDKUiPackage = jssdkUiPackage;
            ViewBag.urlstr = urlstr;
            return View();
        }
        /// <summary>
        /// 坐标缓存cookie,5分钟
        /// </summary>
        /// <param name="currlong">经度</param>
        /// <param name="currlat">纬度</param>
        public void getCurrLongLat(string currlong, string currlat)
        {
            if (Request.Cookies["currlong"] == null)
            {
                HttpCookie cookie1 = new HttpCookie("currlong", currlong);
                cookie1.Expires = DateTime.Now.AddMinutes(5);
                Response.Cookies.Add(cookie1);
            }
            if (Request.Cookies["currlat"] == null)
            {
                HttpCookie cookie2 = new HttpCookie("currlat", currlat);
                cookie2.Expires = DateTime.Now.AddMinutes(5);
                Response.Cookies.Add(cookie2);
            }
        }
        
        /// <summary>
        /// 无权登录定位系统
        /// </summary>
        /// <returns></returns>
        public ActionResult Error()
        {
            return View();
        }

        public ActionResult GetWxPic(string media_id, string callback, string folder)
        {
            WeixinTokenBase tokenBase = new WeixinTokenBase();

            //判断是否保存微信token
            if (Session[WebSiteConfig.WXTOKEN_SESSION_NAME_BASE] != null)
            {
                tokenBase = Session[WebSiteConfig.WXTOKEN_SESSION_NAME_BASE] as WeixinTokenBase;
            }
            else
            {
                string returnUrl = Url.Action(callback);
                return RedirectToAction("Index", "WeiXinHome", new { urlstr = returnUrl });
            }
            string fileGuid = Guid.NewGuid().ToString();

            string uploadDate = DateTime.Now.ToString("yyyyMMdd");
            string dir = String.Format("/Resource/Pet/{0}/{1}/", folder, uploadDate);
            if (Directory.Exists(Server.MapPath(dir)) == false)//如果不存在就创建file文件夹
            {
                Directory.CreateDirectory(Server.MapPath(dir));
            }

            string newfileName = Guid.NewGuid().ToString();
            //原图
            string fullDir1 = dir + newfileName + ".jpg";
            string imgFilePath = Request.MapPath(fullDir1);


            HttpWebResponse myResponse = null;
            try
            {
                //token.access_token = "5_MZsxQs1ztodyRCDw0wvDQddW9OpzYxLZyNpzloD-jsUFRN3MDhXWzcOEjdq5jk1L5_z1IoCKbti_j5AMuNtRo78eML4l3CKJS7JyC0A5BXJ5a9qEZr7Rt1xxhFkdGGbEXiCTDMv-J2jfSTgIYZZcAEAPQQ";
                var url = string.Format("https://api.weixin.qq.com/cgi-bin/media/get?access_token={0}&media_id={1}", tokenBase.access_token, media_id);
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);

                req.Method = "GET";

                myResponse = (HttpWebResponse)req.GetResponse();
                StreamReader reader = new StreamReader(myResponse.GetResponseStream(), Encoding.UTF8);
                Stream stream = myResponse.GetResponseStream();

                #region 保存下载图片  
                FileStream fileStream = new FileStream(imgFilePath, FileMode.Create, FileAccess.Write);
                byte[] bytes = new byte[4096];
                int readSize = 0;
                while ((readSize = stream.Read(bytes, 0, 4096)) > 0)
                {
                    fileStream.Write(bytes, 0, readSize);
                    fileStream.Flush();
                }
                #endregion

                myResponse.Close();
                stream.Close();
                fileStream.Close();
            }
            //异常请求  
            catch (WebException ex)
            {
                fullDir1 = "";
            }
            finally
            {

            }
            return Content(fullDir1);

        }

    }
}
