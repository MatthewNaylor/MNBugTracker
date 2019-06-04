using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MNBugTracker.Startup))]
namespace MNBugTracker
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
