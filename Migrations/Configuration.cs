namespace MNBugTracker.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using MNBugTracker.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<MNBugTracker.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(MNBugTracker.Models.ApplicationDbContext context)
        {
            var roleManager = new RoleManager<IdentityRole>(
                new RoleStore<IdentityRole>(context));

            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                roleManager.Create(new IdentityRole { Name = "Admin" });
            }

            if (!context.Roles.Any(r => r.Name == "ProjectManager"))
            {
                roleManager.Create(new IdentityRole { Name = "ProjectManager" });
            }

            if (!context.Roles.Any(r => r.Name == "Developer"))
            {
                roleManager.Create(new IdentityRole { Name = "Developer" });
            }

            if (!context.Roles.Any(r => r.Name == "Submitter"))
            {
                roleManager.Create(new IdentityRole { Name = "Submitter" });
            }

            var userManager = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(context));

            if (!context.Users.Any(u => u.Email == "mnaylor4122@davidsonccc.edu"))
            {
                userManager.Create(new ApplicationUser
                {
                    Email = "mnaylor4122@davidsonccc.edu",
                    UserName = "mnaylor4122@davidsonccc.edu",
                    FirstName = "Matthew",
                    LastName = "Naylor",
                    DisplayName = "Breeze",
                    ProfilePic = "/ProfilePictures/2220.jpeg"
                }, "Superman85");
            }

            var userId = userManager.FindByEmail("mnaylor4122@davidsonccc.edu").Id;
            userManager.AddToRole(userId, "Admin");

            if (!context.Users.Any(u => u.Email == "JasonTwichell@mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    Email = "JasonTwichell@mailinator.com",
                    UserName = "JasonTwichell@mailinator.com",
                    FirstName = "Jason",
                    LastName = "Twichell",
                    DisplayName = "Manager",
                    ProfilePic = "/ProfilePictures/DefaultProfilePic.png"
                }, "ABC&123");
            }

            userId = userManager.FindByEmail("JasonTwichell@mailinator.com").Id;
            userManager.AddToRole(userId, "ProjectManager");

            if (!context.Users.Any(u => u.Email == "Matt@mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    Email = "Matt@mailinator.com",
                    UserName = "Matt@mailinator.com",
                    FirstName = "Matt",
                    LastName = "Naylor",
                    DisplayName = "Dev",
                    ProfilePic = "/ProfilePictures/DefaultProfilePic.png"
                }, "ABC&123");
            }

            userId = userManager.FindByEmail("Matt@mailinator.com").Id;
            userManager.AddToRole(userId, "Developer");

            if (!context.Users.Any(u => u.Email == "Matt2@mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    Email = "Matt2@mailinator.com",
                    UserName = "Matt2@mailinator.com",
                    FirstName = "Matt",
                    LastName = "Naylor",
                    DisplayName = "Sub",
                    ProfilePic = "/ProfilePictures/DefaultProfilePic.png"
                }, "ABC&123");
            }

            userId = userManager.FindByEmail("Matt2@mailinator.com").Id;
            userManager.AddToRole(userId, "Submitter");

            //Demo Users
            if (!context.Users.Any(u => u.Email == "DemoAdmin@mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    Email = "DemoAdmin@mailinator.com",
                    UserName = "DemoAdmin@mailinator.com",
                    FirstName = "Adam",
                    LastName = "Eden",
                    DisplayName = "demoAdmin",
                    ProfilePic = "/ProfilePictures/DefaultProfilePic.png"
                }, "Superman85!");
            }

            userId = userManager.FindByEmail("DemoAdmin@mailinator.com").Id;
            userManager.AddToRole(userId, "Admin");

            if (!context.Users.Any(u => u.Email == "DemoProjectManager@mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    Email = "DemoProjectManager@mailinator.com",
                    UserName = "DemoProjectManager@mailinator.com",
                    FirstName = "Pat",
                    LastName = "peter",
                    DisplayName = "ProjectManager",
                    ProfilePic = "/ProfilePictures/DefaultProfilePic.png"
                }, "Superman85!");
            }

            userId = userManager.FindByEmail("DemoProjectManager@mailinator.com").Id;
            userManager.AddToRole(userId, "ProjectManager");

            if (!context.Users.Any(u => u.Email == "DemoDeveloper@mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    Email = "DemoDeveloper@mailinator.com",
                    UserName = "DemoDeveloper@mailinator.com",
                    FirstName = "Dan",
                    LastName = "Don",
                    DisplayName = "Developer",
                    ProfilePic = "/ProfilePictures/DefaultProfilePic.png"
                }, "Superman85!");
            }

            userId = userManager.FindByEmail("DemoDeveloper@mailinator.com").Id;
            userManager.AddToRole(userId, "Developer");

            if (!context.Users.Any(u => u.Email == "DemoSubmitter@mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    Email = "DemoSubmitter@mailinator.com",
                    UserName = "DemoSubmitter@mailinator.com",
                    FirstName = "Sean",
                    LastName = "Son",
                    DisplayName = "Submitter",
                    ProfilePic = "/ProfilePictures/DefaultProfilePic.png"
                }, "Superman85!");
            }

            userId = userManager.FindByEmail("DemoSubmitter@mailinator.com").Id;
            userManager.AddToRole(userId, "Submitter");


            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            context.TicketPriorities.AddOrUpdate(
                t => t.Name, 
                new TicketPriority { Name = "Extreme", Description = "The Highest Priority" },
                new TicketPriority { Name = "High", Description = "The Second Highest Priority" },
                new TicketPriority { Name = "Medium", Description = "The Second Lowst Priority" },
                new TicketPriority { Name = "Low", Description = "The Lowest Priority" },
                new TicketPriority { Name = "None", Description = "Not Prioritized" }

                );

            context.TicketTypes.AddOrUpdate(
                t => t.Name,
                new TicketType { Name = "Defect", Description = "A reported defect in a supported project or application" },
                new TicketType { Name = "New functionality request", Description = "A new request for functionality in a supported project or application" },
                new TicketType { Name = "Call for documentation", Description = "A new request for documentation in a supported project or application" }
                );

            context.TicketStatuses.AddOrUpdate(
                t => t.Name,
                new TicketStatus { Name = "New/Unassigned", Description = "This will be the status of all newly created Tickets"},
                new TicketStatus { Name = "Unassigned", Description = "This will be the status of all any unassigned Tickets" },
                new TicketStatus { Name = "Assigned", Description = "This will be the status of all assigned Tickets" },
                new TicketStatus { Name = "Completed", Description = "This will be the status of all completed Tickets" },
                new TicketStatus { Name = "Archived", Description = "This will be the status of all Archived Tickets" },
                new TicketStatus { Name = "Unknown", Description = "This will be the status of any ticket where the status is unknown" }
                );

            context.Projects.AddOrUpdate(
                new Project { Name = "Matt's Portfolio" },
                new Project { Name = "Matt's Blog" },
                new Project { Name = "Matt's BugTracker" }
                );
        }
    }
}
