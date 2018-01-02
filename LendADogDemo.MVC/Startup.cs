using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LendADogDemo.MVC.Startup))]
namespace LendADogDemo.MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
