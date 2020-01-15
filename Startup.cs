using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DigitalFilter_DIPLOMA.Startup))]
namespace DigitalFilter_DIPLOMA
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
