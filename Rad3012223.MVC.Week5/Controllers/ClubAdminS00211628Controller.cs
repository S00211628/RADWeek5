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


        public ActionResult Approve(int? ClubId)
        {

            Club club = db.Clubs.Find(ClubId);

            var clubid = club.ClubId;
            var clubName = club.ClubName;
            var unapproved = club.clubMembers.Where(m => m.approved == false).ToList();
            var approved = club.clubMembers.Where(m => m.approved == true).ToList();



            ClubViewModels mcvm = new ClubViewModels
            {
                ClubID = clubid,
                ClubName = clubName,
                Unapproved = unapproved,
                Approved = approved,
            };

            return View(mcvm);

        }


        [HttpPost]
        public ActionResult Approve(ClubViewModels model)
        {

            var m = model;

            if (ModelState.IsValid)
            {
                if (model.Unapproved != null)
                {
                    foreach (var member in model.Unapproved)
                    {
                        db.Members.Find(member.MemberID).approved = member.approved;
                    }
                }
                if(model.Approved !=null)
                {
                    foreach (var member in model.Approved)
                    {
                        db.Members.Find(member.MemberID).approved = member.approved;
                    }
                }
                
               
                db.SaveChanges();
                return RedirectToAction("Approve", new { Clubid = model.ClubID });
            }

            return View(model);
        }


    }
}