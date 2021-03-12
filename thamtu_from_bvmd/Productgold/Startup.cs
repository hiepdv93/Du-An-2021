using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Productgold.Startup))]
namespace Productgold
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
