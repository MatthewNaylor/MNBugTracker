using Microsoft.AspNet.Identity;
using MNBugTracker.Helpers;
using MNBugTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MNBugTracker.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        private UserRolesHelper roleHelper = new UserRolesHelper();

        // GET: Admin
        [Authorize(Roles = "Admin,")]
        public ActionResult ManageUserRoles()
        {
            ViewBag.Users = new SelectList(db.Users.ToList(), "Id", "Email");
            ViewBag.Roles = new SelectList(db.Roles.ToList(), "Name", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ManageUserRoles(string users, string roles)
        {
            foreach (var role in roleHelper.ListUserRoles(users))
            {
                roleHelper.RemoveUserFromRole(users, role);
            }

            roleHelper.AddUserToRole(users, roles);

            return RedirectToAction("UserIndex");
        }

        // GET: Admin
        [Authorize(Roles = "Admin")]
        public ActionResult ManageMyRole(string id)
        {
            var myCurrentRole = roleHelper.ListUserRoles(id).FirstOrDefault();
            ViewBag.UserId = id;
            ViewBag.Roles = new SelectList(db.Roles.ToList(), "Name", "Name", myCurrentRole);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ManageMyRole(string userId, string roles)
        {
            foreach (var currentRole in roleHelper.ListUserRoles(userId))
            {
                roleHelper.RemoveUserFromRole(userId, currentRole);
            }

            roleHelper.AddUserToRole(userId, roles);

            return RedirectToAction("UserIndex");
        }

        public ActionResult UserIndex()
        {
            var users = db.Users.Select(u => new UserViewModel
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                DisplayName = u.DisplayName,
                Email = u.Email,
                ProfilePic = u.ProfilePic
            }).ToList();

            return View(users);
        }

        public ActionResult Oops(int? id)
        {
            //ViewBag.TicketTitle = db.Tickets.Find(id).Title;
            return View();
        }


    }
}