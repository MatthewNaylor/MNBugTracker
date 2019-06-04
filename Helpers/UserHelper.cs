using MNBugTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MNBugTracker.Helpers
{
    public class UserHelper
    {
        static ApplicationDbContext db = new ApplicationDbContext();
        public static string GetUserImage(string userId)
        {
            return db.Users.Find(userId).ProfilePic;
        }

        public static string GetUserFirstName(string userId)
        {
            return db.Users.Find(userId).FirstName;
        }

        public static string GetUserLastName(string userId)
        {
            return db.Users.Find(userId).LastName;
        }

        //public static string GetUserRole(string userId)
        //{
        //    return db.Users.Find(userId).Roles.;
        //}
    }
}