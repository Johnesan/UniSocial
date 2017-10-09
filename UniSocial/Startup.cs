using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(UniSocial.Startup))]
namespace UniSocial
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
