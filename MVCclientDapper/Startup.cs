using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MVCclientDapper.Startup))]
namespace MVCclientDapper
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
