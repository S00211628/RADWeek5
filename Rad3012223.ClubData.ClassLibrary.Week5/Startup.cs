using Owin;
using Tracker.WebAPIClient;


namespace Rad3012223.ClubData.ClassLibrary.Week5
{
    public partial class StartupOwin
    {
        public void Configuration(IAppBuilder app)
        {
            ActivityAPIClient.Track(StudentID: "S00211628", StudentName: "Martin Melody", activityName: "RAD301 Week6 Lab 2022", Task: "Week 6 Testing Authori");
        }
    }
}
