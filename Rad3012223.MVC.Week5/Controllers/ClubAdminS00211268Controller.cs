using Microsoft.AspNet.Identity.Owin;
using Rad3012223.ClubData.ClassLibrary.Week5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
            return View();
        }
    }
}