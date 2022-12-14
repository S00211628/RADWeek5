using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Rad3012223.ClubData.ClassLibrary.Week5.Models;
using Rad3012223.MVC.Week5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Rad3012223.MVC.Week5.Controllers
{
    [Authorize(Roles = "Admi,ClubAdmin")]
    public class ClubAdminS00211628Controller : Controller
    {
        // GET: ClubAdminS00211628

        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        private Week5ClubContext db = new Week5ClubContext();



        public ActionResult Index()
        {

            if(User.IsInRole("ClubAdmin"))
            {
                // Get the
                ApplicationUser clubAdmin = UserManager.FindByName(User.Identity.Name);
                Member adminMember = db.Members.FirstOrDefault(m => m.StudentID == clubAdmin.EntityID);

                if(adminMember == null)
                {
                    ViewBag.Name = User.Identity.Name + "You have a role but no clubs to Maintain";
                    return View();
                }

                ViewBag.Name = adminMember.studentMember.FirstName + " " + adminMember.studentMember.SecondName;
                List<Club> clubs = db.Clubs
                    .Where(p => p.adminID == adminMember.MemberID).ToList();
                return View(clubs);

            }
            else
            {
                ViewBag.Name = User.Identity.Name + " You are not Authorised to acces any clubs";
                return View();
            }

        }
    }
}