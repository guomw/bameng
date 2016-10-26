using System.Web.Http;
using System.Web.Mvc;

namespace BAMENG.API.Areas.HelpPage
{
    public class HelpPageAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "HelpPage";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {

            //context.MapRoute(
            //   "HelpPage_Model",
            //   "Help_1/{action}/{apiId}",
            //   new { controller = "Help", action = "api", apiId = UrlParameter.Optional });


            context.MapRoute(
                "HelpPage_Default",
                "Help/{action}/{apiId}",
                new { controller = "Help", action = "Index", apiId = UrlParameter.Optional });

            HelpPageConfig.Register(GlobalConfiguration.Configuration);
        }
    }
}