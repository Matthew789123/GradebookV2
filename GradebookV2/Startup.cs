using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GradebookV2.Startup))]
namespace GradebookV2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
