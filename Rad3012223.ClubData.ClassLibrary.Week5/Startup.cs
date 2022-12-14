using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Rad3012223.ClubData.ClassLibrary.Week5.StartupOwin))]

namespace Rad3012223.ClubData.ClassLibrary.Week5
{
    public partial class StartupOwin
    {
        public void Configuration(IAppBuilder app)
        {
            //AuthStartup.ConfigureAuth(app);
        }
    }
}
