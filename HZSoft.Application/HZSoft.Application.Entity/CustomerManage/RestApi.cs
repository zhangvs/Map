using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HZSoft.Application.Entity.CustomerManage
{
    public class GeocodesItem
    {
        /// <summary>
        /// 山东省临沂市兰山区临沂城建时代广场|1号楼
        /// </summary>
        public string formatted_address { get; set; }
        /// <summary>
        /// 山东省
        /// </summary>
        public string province { get; set; }
        /// <summary>
        /// 0539
        /// </summary>
        public string citycode { get; set; }
        /// <summary>
        /// 临沂市
        /// </summary>
        public string city { get; set; }
        /// <summary>
        /// 兰山区
        /// </summary>
        public string district { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string township { get; set; }
        /// <summary>
        /// 371322
        /// </summary>
        public string adcode { get; set; }
        /// <summary>
        /// 坐标
        /// </summary>
        public string location { get; set; }
        /// <summary>
        /// 门牌号
        /// </summary>
        public string level { get; set; }

    }

    public class RestApi
    {
        /// <summary>
        /// 
        /// </summary>
        public string status { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string info { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string infocode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string count { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<GeocodesItem> geocodes { get; set; }
    }
}
