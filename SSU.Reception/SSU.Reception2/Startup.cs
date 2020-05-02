using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SSU.Reception.Startup))]
namespace SSU.Reception
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
