using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Module11.Startup))]
namespace Module11
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
