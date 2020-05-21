using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace HZSoft.Util
{
    public class HttpClientHelper
    {
        #region Get
        /// <summary>  
        /// 执行基本的命令方法,以Get方式  
        /// </summary>  
        /// <param name="apiurl"></param>  
        /// <returns></returns>  
        public static String Get(string apiurl)
        {
            using (var httpClient = new HttpClient())
            {
                var Url = apiurl;
                httpClient.Timeout = TimeSpan.FromMilliseconds(3000);//3 秒超时
                var task = httpClient.GetAsync(Url);
                if (task.Result.IsSuccessStatusCode == false)
                    throw new Exception("接口访问失败:" + task.Result.StatusCode);
                var result = task.Result.Content.ReadAsStringAsync().Result;
                return result;
            }
        }
        #endregion  

        #region Post
        /// <summary>  
        /// 以Post方式提交命令  
        /// </summary>  
        public static String Post(string apiurl, string jsonString)
        {
            var handler = new HttpClientHandler() { AutomaticDecompression = DecompressionMethods.GZip };
            //创建HttpClient（注意传入HttpClientHandler）
            using (var http = new HttpClient(handler))
            {
                //var requestJson = JsonConvert.SerializeObject(jsonString);

                HttpContent httpContent = new StringContent(jsonString);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                //await异步等待回应
                var task = http.PostAsync(apiurl, httpContent);
                var result = task.Result.Content.ReadAsStringAsync().Result;


                //确保HTTP成功状态值
                //task.Result.EnsureSuccessStatusCode();
                if (task.Result.IsSuccessStatusCode == false)
                    throw new Exception("接口访问失败:" + task.Result.StatusCode);
                return result;
            }
        }
        #endregion

        #region Get Function    
        /// <summary>  
        /// 发起GET请求  
        /// </summary>  
        /// <typeparam name="T"></typeparam>  
        /// <param name="requestUrl"></param>  
        /// <returns></returns>  
        public static T Get<T>(String requestUrl)
        {
            String resultJson = HttpClientHelper.Get(requestUrl);
            LogHelper.AddLog(resultJson);
            return AnalyzeResult<T>(resultJson);
        }
        #endregion

        #region Post Function  
        /// <summary>  
        /// 发起POST请求  
        /// </summary>  
        /// <typeparam name="T"></typeparam>  
        /// <param name="requestUrl"></param>  
        /// <param name="requestParamOfJsonStr"></param>  
        /// <returns></returns>  
        public static T Post<T>(String requestUrl, String requestParamOfJsonStr)
        {
            String resultJson = HttpClientHelper.Post(requestUrl, requestParamOfJsonStr);
            return AnalyzeResult<T>(resultJson);
        }
        #endregion

        #region AnalyzeResult  
        /// <summary>  
        /// 分析结果  
        /// </summary>  
        /// <typeparam name="T"></typeparam>  
        /// <param name="resultJson"></param>  
        /// <returns></returns>  
        private static T AnalyzeResult<T>(string resultJson)
        {
            T result = default(T);
            if (!String.IsNullOrEmpty(resultJson))
            {
                result = JsonConvert.DeserializeObject<T>(resultJson);
            }
            
            return result;
        }
        #endregion
    }
}
