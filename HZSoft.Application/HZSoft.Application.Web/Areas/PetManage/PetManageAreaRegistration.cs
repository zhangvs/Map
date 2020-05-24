using System.Web.Mvc;

namespace HZSoft.Application.Web.Areas.ReportManage
{
    public class PetManageAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "PetManage";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
              this.AreaName + "_Default",
              this.AreaName + "/{controller}/{action}/{id}",
              new { area = this.AreaName, controller = "Home", action = "Index", id = UrlParameter.Optional },
              new string[] { "HZSoft.Application.Web.Areas." + this.AreaName + ".Controllers" }
            );
        }
    }
}
