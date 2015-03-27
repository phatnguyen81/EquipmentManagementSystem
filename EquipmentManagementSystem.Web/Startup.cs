using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EquipmentManagementSystem.Web.Startup))]
namespace EquipmentManagementSystem.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
