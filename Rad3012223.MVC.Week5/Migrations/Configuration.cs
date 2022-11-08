namespace Rad3012223.MVC.Week5.Migrations
{
    using Microsoft.Ajax.Utilities;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Rad3012223.ClubData.ClassLibrary.Week5.DTOs;
    using Rad3012223.ClubData.ClassLibrary.Week5.Models;
    using Rad3012223.MVC.Week5.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Xml.Linq;
    using System.Xml.XPath;
    using Tracker.WebAPIClient;
    using Member = ClubData.ClassLibrary.Week5.Models.Member;

    internal sealed class Configuration : DbMigrationsConfiguration<Rad3012223.MVC.Week5.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Rad3012223.MVC.Week5.Models.ApplicationDbContext context)
        {
            ActivityAPIClient.Track(StudentID: "S00211628", StudentName: "Martin Melody", activityName: "RAD301 Week5Lab 2223", Task: "Populating Application User as member");


            #region Usual user setup and role code
            var manager =
                new UserManager<ApplicationUser>(
                    new UserStore<ApplicationUser>(context));

            var roleManager =
                new RoleManager<IdentityRole>(
                    new RoleStore<IdentityRole>(context));

            #endregion

            context.Roles.AddOrUpdate(r => r.Name,
                new IdentityRole { Name = "Admin" }
                );

            context.Roles.AddOrUpdate(r => r.Name,
                new IdentityRole { Name = "ClubAdmin" }
                );

            context.Roles.AddOrUpdate(r => r.Name,
                new IdentityRole { Name = "Member" }
                );

            PasswordHasher ps = new PasswordHasher();

            context.Users.AddOrUpdate(u => u.UserName,
                new ApplicationUser
                {
                    EntityID = "Admin",
                    UserName = "admin@itsligo.ie",
                    Email = "admin@itsligo.ie",
                    SecurityStamp = Guid.NewGuid().ToString(),
                    PasswordHash = ps.HashPassword("Rad3022021$1")
                });


            seed_appliation_members(manager, context);

            context.SaveChanges();

        }

        private void seed_appliation_members(UserManager<ApplicationUser> manager, ApplicationDbContext context)
        {
            using(Week5ClubContext cx = new Week5ClubContext())
            {
                Club club = cx.Club.First();
                Member adminMember = cx.Member.FirstOrDefault(m => m.MemberID == club.adminID);
                club.clubMembers.ForEach(m =>
                {
                    IdentityResult result = manager.Create(new ApplicationUser
                    {
                        EmailConfirmed = true,
                        EntityID = m.StudentID,
                        Email = m.StudentID + "@itsligo.ie",
                        UserName = m.StudentID + "@itsligo.ie",
                        SecurityStamp = Guid.NewGuid().ToString(),
                    }, m.StudentID + "$1");
                    if (result.Succeeded)
                    {
                        ApplicationUser clubAdmin = manager.FindByEmail(adminMember.StudentID + "@itsligo.ie");
                        if (clubAdmin != null)
                        {
                            manager.AddToRoles(clubAdmin.Id, new string[] { "ClubAdmin" });
                        }
                    }
                });
            }
        }
    }
}
