using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HZSoft.Application.Entity.CustomerManage
{
    [Table("POS_PoiTypeCode")]
    public partial class POS_PoiTypeCode
    {
        public int Id { get; set; }

        [StringLength(100)]
        public string TypeCode { get; set; }

        [StringLength(100)]
        public string BigCategory { get; set; }

        [StringLength(100)]
        public string MidCategory { get; set; }

        [StringLength(100)]
        public string SubCategory { get; set; }

        [StringLength(100)]
        public string BigCategoryEn { get; set; }

        [StringLength(100)]
        public string MidCategoryEn { get; set; }

        [StringLength(100)]
        public string SubCategoryEn { get; set; }
    }
}
