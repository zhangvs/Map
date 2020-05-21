using HZSoft.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace HZSoft.Application.Web
{
    public class WeixinConfig
    {
        public static string AppID { private set; get; }
        public static string AppSecret { private set; get; }
        public static string RedirectUri { private set; get; }
        public static string GetCodeUrl { private set; get; }
        public static string GetTokenUrl { private set; get; }
        public static string GetTokenBaseUrl { private set; get; }
        public static string GetUserInfoUrl { private set; get; }


        public static void Register()
        {
            AppID = Config.GetValue("AppID"); ;
            AppSecret = Config.GetValue("AppSecret");
            RedirectUri = Config.GetValue("Domain") + "/WeChatManage/WXLogin/Redirect";
            GetCodeUrl = "https://open.weixin.qq.com/connect/oauth2/authorize?appid=" + AppID + "&redirect_uri=" + HttpUtility.UrlEncode(WeixinConfig.RedirectUri) + "&response_type=code&scope=snsapi_userinfo&state={0}#wechat_redirect";//HttpUtility.UrlEncode(WeixinConfig.ReturnUri)
            GetTokenUrl = "https://api.weixin.qq.com/sns/oauth2/access_token?appid=" + AppID + "&secret=" + AppSecret + "&code={0}&grant_type=authorization_code";
            GetTokenBaseUrl = "https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid=" + AppID + "&secret=" + AppSecret;
            GetUserInfoUrl = "https://api.weixin.qq.com/sns/userinfo?access_token={0}&openid={1}&lang=zh_CN";
        }


        ////↓↓↓↓↓↓↓↓↓↓请在这里配置您的基本信息↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓
        ///// <summary>
        ///// 应用ID
        ///// </summary>
        //public static string AppID = "wx24e47efa56c2e554";
        ///// <summary>
        ///// 应用密钥
        ///// </summary>
        //public static string AppSecret = "1f8de99c6304d13e5a65efa418638ee4";


        ///// <summary>
        ///// URL(服务器地址)
        ///// </summary>
        //public static string RedirectUri = WebConfigurationManager.AppSettings["website_wx"].ToString() + "/weixin/Redirect";

        ///// <summary>
        ///// 获得微信授权登录Code
        ///// </summary>
        //public static string GetCodeUrl = "https://open.weixin.qq.com/connect/oauth2/authorize?appid=" + AppID + "&redirect_uri=" + HttpUtility.UrlEncode(WeixinConfig.RedirectUri) + "&response_type=code&scope=snsapi_userinfo&state={0}#wechat_redirect";

        ///// <summary>
        ///// 获取微信TokenURL
        ///// </summary>
        //public static string GetTokenUrl = "https://api.weixin.qq.com/sns/oauth2/access_token?appid=" + AppID + "&secret=" + AppSecret + "&code={0}&grant_type=authorization_code";

        ///// <summary>
        ///// 获得微信用户信息 {0} access_token {1} openid
        ///// </summary>
        //public static string GetUserInfoUrl = "https://api.weixin.qq.com/sns/userinfo?access_token={0}&openid={1}&lang=zh_CN";

        ///// <summary>
        ///// 商户号
        ///// </summary>
        //public static string MchId = "1488393312";

        ///// <summary>
        ///// 微信支付秘钥
        ///// </summary>
        //public static string Key = "lywenkai741852963Lywenkai7418529";

        ///// <summary>
        ///// 支付完成后的回调处理页面
        ///// </summary>
        //public static string TenPayV3Notify = WebConfigurationManager.AppSettings["website_wx"].ToString() + "/Weixin/Notify";


        ////↓↓↓↓↓↓↓↓↓↓请在这里配置WFT 参数↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓


        ///// <summary>
        ///// WFT商户号
        ///// </summary>
        //public static string WFTMchId = "102513790412";//"102513790412";7551000001

        ///// <summary>
        ///// WFT支付秘钥
        ///// </summary>
        //public static string WFTKey = "a7721c6663c6fc19b90a8e893f6d9dff";//"a7721c6663c6fc19b90a8e893f6d9dff";9d101c97133837e13dde2d32a5054abb
        ///// <summary>
        ///// WFT接口请求地址
        ///// </summary>
        //public static string WFTReqUrl = "https://pay.swiftpass.cn/pay/gateway";
    }
}
