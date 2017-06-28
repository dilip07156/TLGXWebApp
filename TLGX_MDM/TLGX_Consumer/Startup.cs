using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TLGX_Consumer.Startup))]
namespace TLGX_Consumer
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
