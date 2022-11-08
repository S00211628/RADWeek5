using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Rad3012223.ClubData.ClassLibrary.Week5.Models;
using Rad3012223.MVC.Week5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Member = Rad3012223.ClubData.ClassLibrary.Week5.Models.Member;

namespace Rad3012223.MVC.Week5.Controllers
{

    [Authorize(Roles = "Admin,Club Admin")]
    public class ClubAdminS00211268Controller : Controller
    {
        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext
                    .GetOwinContext()
                    .GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        private Week5ClubContext db = new Week5ClubContext();
        public ActionResult Index()
        {
            if (User.IsInRole("Club Admin"))
            {
                // Get the
                ApplicationUser clubAdmin = UserManager.FindByName(User.Identity.Name);
                Member adminMember = db.Member.FirstOrDefault(m => m.StudentID == clubAdmin.EntityID);

                if (adminMember == null)
                {
                    ViewBag.Name = User.Identity.Name + " You have a role but no clubs to maintain";
                    return View();
                }
                ViewBag.Name = adminMember.studentMember.FirstName + " " + adminMember.studentMember.SecondName;
                List<Club> clubs = db.Club
                    .Where(p => p.adminID == adminMember.MemberID).ToList();
                return View(clubs);
            }
            else
            {
                ViewBag.Name = User.Identity.Name + " You are not Authorised to access my clubs";
                return View();
            }
        }


        public ActionResult Approve(int? ClubId)
        {
            Club club = db.Club.Find(ClubId);
            MainClubViewModel mcvm = new MainClubViewModel
            {
                ClubID = club.ClubId,
                cluName = club.ClubName,
                Unapproved = club.clubMembers.Where(m => m.approved == false).ToList(),
                Approved = club.clubMembers.Where(m => m.approved == true).ToList(),
            };

            return View(mcvm);
        }

        [HttpPost]
        public ActionResult Approve(MainClubViewModel model)
        {
            if (ModelState.IsValid)
            {
                foreach (var member in model.Unapproved)
                    db.Member.Find(member.MemberID).approved = member.approved;
                foreach (var member in model.Approved)
                    db.Member.Find(member.MemberID).approved = member.approved;
                db.SaveChanges();
                return RedirectToAction("Approve", new { ClubId = model.ClubID });
            }

            return View(model);
        }

    }
}