using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CPSeed.Startup))]
namespace CPSeed
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
