namespace Rad3012223.ClubData.ClassLibrary.Week5.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Tracker.WebAPIClient;

    internal sealed class Configuration : DbMigrationsConfiguration<Rad3012223.ClubData.ClassLibrary.Week5.Models.Week5ClubContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Rad3012223.ClubData.ClassLibrary.Week5.Models.Week5ClubContext context)
        {
            ActivityAPIClient.Track(StudentID: "S00211628", StudentName: "Martin Melody", activityName: "RAD301 Week5Lab 2223", Task: "Creating ClubDB Database");
        }
    }
}
