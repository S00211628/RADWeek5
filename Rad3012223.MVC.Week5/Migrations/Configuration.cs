namespace Rad3012223.MVC.Week5.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Tracker.WebAPIClient;

    internal sealed class Configuration : DbMigrationsConfiguration<Rad3012223.MVC.Week5.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Rad3012223.MVC.Week5.Models.ApplicationDbContext context)
        {
            ActivityAPIClient.Track(StudentID: "S00211628", StudentName: "Martin Melody", activityName: "RAD301 Week5Lab 2223", Task: "Creating Application DB Database");
        }
    }
}
