using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(baggageonlinestores.Startup))]
namespace baggageonlinestores
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
