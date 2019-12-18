using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Manage.Startup))]
namespace Manage
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
