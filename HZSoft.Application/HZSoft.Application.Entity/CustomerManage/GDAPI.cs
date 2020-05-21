using HZSoft.Util;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HZSoft.Application.Entity.CustomerManage
{
    public class GDAPI
    {
        public static GeocodesItem AddressToGeo(string CustomerAddress)
        {
            GeocodesItem geo = null;
            RestApi restApi = new RestApi();
            string url = @"http://restapi.amap.com/v3/geocode/geo?key=cf3dd05a8192fd1839628b39e589c89e&city=0539&address=" + CustomerAddress;//output=XML&
            string responseJson = HttpClientHelper.Get(url);
            restApi = JsonConvert.DeserializeObject<RestApi>(responseJson.Replace("[]", "\"\""));

            if (restApi.count != "0")
            {
                geo = restApi.geocodes[0];
            }
            if (geo == null)
            {
                throw new Exception("地址【 " + CustomerAddress + " 】转换坐标失败！" + responseJson);
            }
            return geo;
        }

        //逆地理编码：将经纬度转换为详细结构化的地址，且返回附近周边的POI、AOI信息。
        public static Regeocode GetRegeoByGeo(string coor)
        {
            Regeocode Regeo = null;
            string urlRegeo = @"http://restapi.amap.com/v3/geocode/regeo?key=cf3dd05a8192fd1839628b39e589c89e&location=" + coor + "&radius=1&extensions=all&batch=false&roadlevel=0";//output=XML&
            string responseJsonRegeo = HttpClientHelper.Get(urlRegeo);
            InverseAddress inverse = JsonConvert.DeserializeObject<InverseAddress>(responseJsonRegeo.Replace("[]", "\"\""));
            if (inverse.status == "1")
            {
                Regeo = inverse.regeocode;
            }
            return Regeo;
        }

        public static void getCoordinate(string location, out double wxLat, out double wxLon, out double bdLat, out double bdLon)
        {
            string[] jw = location.Split(',');
            //微信转换百度坐标
            wxLon = 0; wxLat = 0; bdLon = 0; bdLat = 0;
            if (jw.Length == 2)
            {
                wxLon = double.Parse(jw[0]);//经度
                wxLat = double.Parse(jw[1]);//维度
                MapConverter.GCJ02ToBD09(wxLat, wxLon, out bdLat, out bdLon);
            }
        }
    }
}
