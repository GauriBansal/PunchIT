using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PunchIT.Startup))]
namespace PunchIT
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
