using Microsoft.Owin;
using Owin;
using WF.UAP.UA.UAPortal.WebAPI;

[assembly: OwinStartup(typeof(Startup))]

namespace WF.UAP.UA.UAPortal.WebAPI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
