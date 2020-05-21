using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HZSoft.Application.Entity.WeChatManage
{
    //public class Keywords
    //{
    //}

    //public class Cities
    //{
    //}

    //public class Suggestion
    //{
    //    public List<Keywords> keywords { get; set; }
    //    public List<Cities> cities { get; set; }
    //}

    //public class Biz_type
    //{
    //}

    //public class Distance
    //{
    //}

    //public class Biz_ext
    //{
    //}

    //public class Tel
    //{
    //}

    //public class Importance
    //{
    //}

    //public class Shopid
    //{
    //}

    //public class Poiweight
    //{
    //}

    public class Pois
    {
        public string id { get; set; }
        /// <summary>
        ///名称: 临沂综合保税区
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 兴趣点类型: 商务住宅;产业园区;产业园区
        /// </summary>
        public string type { get; set; }
        /// <summary>
        /// 兴趣点类型编码:120100
        /// </summary>
        public string typecode { get; set; }
        /// <summary>
        /// 行业类型
        /// </summary>
        //public List<Biz_type> biz_type { get; set; }
        /// <summary>
        /// 临工路100号
        /// </summary>
        public string address { get; set; }
        /// <summary>
        /// 118.508359,34.994342
        /// </summary>
        public string location { get; set; }
        /// <summary>
        /// 0539-8870010
        /// </summary>
        //public List<Tel> tel { get; set; }
        //public List<Distance> distance { get; set; }
        //public List<Biz_ext> biz_ext { get; set; }
        /// <summary>
        /// 山东省
        /// </summary>
        public string pname { get; set; }
        /// <summary>
        /// 临沂市
        /// </summary>
        public string cityname { get; set; }
        /// <summary>
        /// 河东区
        /// </summary>
        public string adname { get; set; }
        //public List<Importance> importance { get; set; }
        //public List<Shopid> shopid { get; set; }
        public string shopinfo { get; set; }
        //public List<Poiweight> poiweight { get; set; }
    }

    public class RestPoi
    {
        /// <summary>
        /// 结果状态值，值为0或1
        /// </summary>
        public string status { get; set; }
        /// <summary>
        /// 搜索方案数目
        /// </summary>
        public string count { get; set; }
        /// <summary>
        /// 返回状态说明
        /// </summary>
        public string info { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string infocode { get; set; }
        /// <summary>
        /// 城市建议列表
        /// </summary>
        //public Suggestion suggestion { get; set; }
        /// <summary>
        /// 搜索POI信息列表
        /// </summary>
        public List<Pois> pois { get; set; }
    }
}
