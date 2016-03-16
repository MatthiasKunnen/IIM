using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(IIM.Startup))]
namespace IIM
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
