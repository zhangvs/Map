using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HZSoft.Application.Entity.CustomerManage
{
    public class AddressComponent
    {
        //<country>中国</country>
        //<province>山东省</province>
        //<city>临沂市</city>
        //<citycode>0539</citycode>
        //<district>兰山区</district>
        //<adcode>371302</adcode>
        //<township>银雀山街道</township>
        //<towncode>371302002000</towncode>
        public string country { get; set; }
        public string province { get; set; }
        public string city { get; set; }
        public string citycode { get; set; }
        public string district { get; set; }
        public string adcode { get; set; }
        public string township { get; set; }
        public string towncode { get; set; }
    }

    //<aoi>
    //<id>B021B0XJRV</id>
    //<name>万阅城</name>
    //<adcode>371302</adcode>
    //<location>118.335936,35.06204</location>
    //<area>17920.247748</area>
    //<distance>0</distance>
    //<type>120302</type>
    //</aoi>
    public class Aois
    {
        public string id { get; set; }
        public string name { get; set; }
        public string adcode { get; set; }
        public string location { get; set; }
        public string area { get; set; }
        public string distance { get; set; }
        public string type { get; set; }
    }
    //<pois type = "list" >
    //< poi >
    //< id > B021B0C2U0 </ id >
    //< name > 上城国际金融中心B座 </ name >
    //< type > 商务住宅; 楼宇;商务写字楼</type>
    //<tel/>
    //<direction>西北</direction>
    //<distance>45.4203</distance>
    //<location>118.359436,35.050589</location>
    //<address>金雀山路10号</address>
    //<poiweight>0.210726</poiweight>
    //<businessarea/>
    //</poi>
    //</pois>
    public class PoisList
    {
        public string id { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public string direction { get; set; }
        public string distance { get; set; }
        public string location { get; set; }
        public string address { get; set; }
        public string poiweight { get; set; }
    }
    public class Regeocode
    {
        public string formatted_address { get; set; }
        public AddressComponent addressComponent { get; set; }
        public List<PoisList> pois { get; set; }
        public List<Aois> aois { get; set; }

    }

    public class InverseAddress
    {
        public string status { get; set; }
        public string info { get; set; }
        public string infocode { get; set; }
        public Regeocode regeocode { get; set; }
    }
}
