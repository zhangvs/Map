using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HZSoft.Util
{
    public class LocationHelper
    {
        /// <summary>
        /// 根据用户表中设置的区域，权限内的坐标点
        /// </summary>
        /// <returns></returns>
        public static string GetLocationSql(string des)
        {
            string locationSql = "";
            if (!string.IsNullOrEmpty(des))
            {
                Regex r = new Regex(@"[\u4e00-\u9fa5]+");//包括中文
                if (!r.IsMatch(des))
                {
                    //半径圈
                    if (des.IndexOf('|') > 0)
                    {
                        string[] locations = des.Split('|');
                        for (int i = 0; i < locations.Length; i++)
                        {
                            locationSql += "SELECT * FROM Ku_Location where dbo.f_GetDistance(" + locations[i] + @",bdlon,bdlat)<=1000 UNION ";//多个区县用|分隔
                        }
                        locationSql = "(" + locationSql.Substring(0, locationSql.Length - 6) + ")";
                    }
                    else
                    {
                        locationSql = "(SELECT * FROM Ku_Location where dbo.f_GetDistance(" + des + @",bdlon,bdlat)<=1000) ";//导入的没有SellerId，区域限制
                    }
                }
                else
                {
                    //区县
                    if (des.IndexOf('|') > 0)
                    {
                        des = des.Replace("|", "','");
                        locationSql = "(SELECT * FROM Ku_Location where district in (" + des + "))";//多个区县用|分隔
                    }
                    else
                    {
                        locationSql = "(SELECT * FROM Ku_Location where district ='" + des + "')";//导入的没有SellerId，区域限制
                    }
                }
            }
            else
            {
                locationSql = "Ku_Location";
            }
            return locationSql;
        }
    }
}
