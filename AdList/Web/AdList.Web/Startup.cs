using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AdList.Web.Startup))]

namespace AdList.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            this.ConfigureAuth(app);
        }
    }
}
