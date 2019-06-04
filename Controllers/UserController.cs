using Microsoft.AspNet.Identity;
using MNBugTracker.Helpers;
using MNBugTracker.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MNBugTracker.Controllers
{
    public class UserController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult EditProfile()
        {
            var userId = User.Identity.GetUserId();
            var user = db.Users.Find(userId);
            var myUserViewModel = new UserViewModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                DisplayName = user.DisplayName,
                ProfilePic = user.ProfilePic,
                Email = user.Email
            };

            return View(myUserViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProfile(UserViewModel user, HttpPostedFileBase Avatar)
        {

            if (ModelState.IsValid)
            {
                var newUser = db.Users.Find(user.Id);
                {
                    newUser.FirstName = user.FirstName;
                    newUser.LastName = user.LastName;
                    newUser.DisplayName = user.DisplayName;
                    newUser.Email = user.Email;
                    newUser.ProfilePic = user.ProfilePic;
                    newUser.UserName = user.Email;
                }

                if (Avatar != null)
                {
                    if (ImageUploadValidator.IsWebFriendlyImage(Avatar))
                    {
                        var fileName = Path.GetFileName(Avatar.FileName);
                        Avatar.SaveAs(Path.Combine(Server.MapPath("~/ProfilePictures/"), fileName));
                        newUser.ProfilePic = "/ProfilePictures/" + fileName;
                    }
                }

                db.SaveChanges();
                
            }
            return RedirectToAction("ProfilePage", "Home");
        }
    }
}