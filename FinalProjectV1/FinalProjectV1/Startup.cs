using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FinalProjectV1.Startup))]
namespace FinalProjectV1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
