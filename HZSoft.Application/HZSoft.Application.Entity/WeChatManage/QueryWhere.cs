using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HZSoft.Application.Entity.WeChatManage
{
    /// <summary>
    /// 
    /// </summary>
    public class QueryWhere
    {
        public string wxLat { get; set; }
        public string wxLon { get; set; }
        public string SearchName { get; set; }
        public string OpenId { get; set; }
        public string UserId { get; set; }
        public string OfficeId { get; set; }
        public string DataAuthorize { get; set; }
        public string AreaAuthority { get; set; }
    }

    public class Query
    {
        public string wxLat { get; set; }
        public string wxLon { get; set; }
        public string SearchName { get; set; }
        public string OpenId { get; set; }
        public string UserId { get; set; }
        public string LocationId { get; set; }
        public string DataAuthorize { get; set; }
        public string AreaAuthority { get; set; }
    }
}
