// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Global.asax.cs" company="">
//   
// </copyright>
// <summary>
//   The web api application.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WF.UAP.UA.UCRA.WebApi
{
    #region

    using System.Web;
    using System.Web.Http;

    #endregion

    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    /// <summary>
    /// The web api application.
    /// </summary>
    public class WebApiApplication : HttpApplication
    {
        /// <summary>
        /// The application_ start.
        /// </summary>
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            FilterConfig.RegisterWebApiFilters(GlobalConfiguration.Configuration.Filters);

            HandlerConfig.RegisterWebApiHandler(GlobalConfiguration.Configuration.MessageHandlers);

            Bootstrapper.Initialise();
        }
    }
}