using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MNBugTracker.Models
{
    public class UserViewModel
    {
        [NotMapped]
        public string Id { get; set; }

        [Display(Name ="First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Display Name")]
        public string DisplayName { get; set; }

        [Display, EmailAddress]
        public string Email { get; set; }

        [Display(Name = "Profile Picture")]
        public string ProfilePic { get; set; }

    }
}