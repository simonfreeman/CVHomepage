using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CVHomepage.Startup))]
namespace CVHomepage
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
