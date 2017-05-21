using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LibOnline.Startup))]
namespace LibOnline
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
