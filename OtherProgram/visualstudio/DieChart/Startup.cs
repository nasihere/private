using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DieChart.Startup))]
namespace DieChart
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
