using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(IQRecruitmentTool.Startup))]
namespace IQRecruitmentTool
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
