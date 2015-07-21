using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace WF.UAP.UA.UAPortal.WebAPI
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
        }
    }
}
