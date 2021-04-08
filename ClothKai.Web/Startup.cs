using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ClothKai.Web.Startup))]
namespace ClothKai.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
