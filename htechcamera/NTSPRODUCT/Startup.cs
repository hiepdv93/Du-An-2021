using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(NTSPRODUCT.Startup))]
namespace NTSPRODUCT
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
            ConfigureAuth(app);
        }
    }
}
