using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CmsMvc.Startup))]
namespace CmsMvc
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
