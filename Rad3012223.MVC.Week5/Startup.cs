using Microsoft.Owin;
using Owin;
using Tracker.WebAPIClient;

[assembly: OwinStartupAttribute(typeof(Rad3012223.MVC.Week5.Startup))]
namespace Rad3012223.MVC.Week5
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            ActivityAPIClient.Track(StudentID: "S00211628", StudentName: "Martin Melody", activityName: "RAD301 Week5Lab 2223", Task: "Week 6Index controller and View implemented andstyled");
        
        }
    }
}
