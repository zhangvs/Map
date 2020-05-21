using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HZSoft.Application.Entity.WeChatManage
{
    //public class City
    //{
    //}

    //public class Neighborhood
    //{
    //    public string name { get; set; }
    //    public string type { get; set; }
    //}

    //public class Building
    //{
    //    public string name { get; set; }
    //    public string type { get; set; }
    //}

    //public class StreetNumber
    //{
    //    public string street { get; set; }
    //    public string number { get; set; }
    //    public string location { get; set; }
    //    public string direction { get; set; }
    //    public string distance { get; set; }
    //}

    //public class BusinessAreas
    //{
    //    public string location { get; set; }
    //    public string name { get; set; }
    //    public string id { get; set; }
    //}

    public class AddressComponent
    {
        /// <summary>
        /// 中国
        /// </summary>
        public string country { get; set; }
        /// <summary>
        /// 北京市 山东省
        /// </summary>
        public string province { get; set; }
        /// <summary>
        /// 临沂市
        /// </summary>
        public string city { get; set; }
        /// <summary>
        /// 010 0539
        /// </summary>
        public string citycode { get; set; }
        /// <summary>
        /// 海淀区 河东区
        /// </summary>
        public string district { get; set; }
        /// <summary>
        /// 110108  371312
        /// </summary>
        public string adcode { get; set; }
        /// <summary>
        /// 燕园街道    九曲街道
        /// </summary>
        public string township { get; set; }
        /// <summary>
        /// 110108015000    371312001000
        /// </summary>
        public string towncode { get; set; }
        //public Neighborhood neighborhood { get; set; }
        //public Building building { get; set; }
        //public StreetNumber streetNumber { get; set; }
        //public List<BusinessAreas> businessAreas { get; set; }
    }

    //public class Pois
    //{
    //    public string id { get; set; }
    //    public string name { get; set; }
    //    public string type { get; set; }
    //    public string tel { get; set; }
    //    public string direction { get; set; }
    //    public string distance { get; set; }
    //    public string location { get; set; }
    //    public string address { get; set; }
    //    public string poiweight { get; set; }
    //    public string businessarea { get; set; }
    //}

    //public class Roads
    //{
    //    public string id { get; set; }
    //    public string name { get; set; }
    //    public string direction { get; set; }
    //    public string distance { get; set; }
    //    public string location { get; set; }
    //}

    //public class Roadinters
    //{
    //    public string direction { get; set; }
    //    public string distance { get; set; }
    //    public string location { get; set; }
    //    public string first_id { get; set; }
    //    public string first_name { get; set; }
    //    public string second_id { get; set; }
    //    public string second_name { get; set; }
    //}

    //public class Aois
    //{
    //    public string id { get; set; }
    //    public string name { get; set; }
    //    public string adcode { get; set; }
    //    public string location { get; set; }
    //    public string area { get; set; }
    //    public string distance { get; set; }
    //    public string type { get; set; }
    //}

    public class Regeocode
    {
        public string formatted_address { get; set; }
        public AddressComponent addressComponent { get; set; }
        //public List<Pois> pois { get; set; }
        //public List<Roads> roads { get; set; }
        //public List<Roadinters> roadinters { get; set; }
        //public List<Aois> aois { get; set; }
    }

    public class RestLocation
    {
        public string status { get; set; }
        public string info { get; set; }
        public string infocode { get; set; }
        public Regeocode regeocode { get; set; }
    }
}
