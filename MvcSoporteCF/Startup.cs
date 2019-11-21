using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MvcSoporteCF.Startup))]
namespace MvcSoporteCF
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
