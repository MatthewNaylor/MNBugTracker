using MNBugTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MNBugTracker.Helpers
{
    public class TicketHelper
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public int GetNewTicketStatus(string oldDeveloper, string newDeveloper)
        {
            var newAssingnment = string.IsNullOrEmpty(oldDeveloper) && !string.IsNullOrEmpty(newDeveloper);
            var unAssingnment = !string.IsNullOrEmpty(oldDeveloper) && string.IsNullOrEmpty(newDeveloper);
            var reAssingnment = !string.IsNullOrEmpty(oldDeveloper) && !string.IsNullOrEmpty(newDeveloper) && (oldDeveloper != newDeveloper);


            var statusId = -1;
            if (newAssingnment)
            {
                statusId = db.TicketStatuses.FirstOrDefault(t => t.Name == "Assigned").Id;
            }
            else if (unAssingnment)
            {
                statusId = db.TicketStatuses.FirstOrDefault(t => t.Name == "Unassigned").Id;
            }
            else if (reAssingnment)
            {
                statusId = db.TicketStatuses.FirstOrDefault(t => t.Name == "Assigned").Id;
            }
            else
            {
                statusId = db.TicketStatuses.FirstOrDefault(t => t.Name == "Unknown").Id;
            }

            return statusId;
        }

        public static string GetPriorityClass(string priority)
        {
            //new TicketPriority { Name = "Extreme", Description = "The Highest Priority" },
            //new TicketPriority { Name = "High", Description = "The Second Highest Priority" },
            //new TicketPriority { Name = "Medium", Description = "The Second Lowst Priority" },
            //new TicketPriority { Name = "Low", Description = "The Lowest Priority" },
            //new TicketPriority { Name = "None", Description = "Not Prioritized" }

            var colorClass = "";

            switch (priority)
            {
                case "Extreme":
                    colorClass = "bg-red";
                    break;
                case "High":
                    colorClass = "bg-orange";
                    break;
                case "Medium":
                    colorClass = "bg-indigo";
                    break;
                case "Low":
                    colorClass = "bg-blue";
                    break;
                case "None":
                    colorClass = "bg-cyan";
                    break;
                default:
                    break;
            }
            return colorClass;
        }

        public static string GetStatusClass(string status)
        {
            //new TicketPriority { Name = "Extreme", Description = "The Highest Priority" },
            //new TicketPriority { Name = "High", Description = "The Second Highest Priority" },
            //new TicketPriority { Name = "Medium", Description = "The Second Lowst Priority" },
            //new TicketPriority { Name = "Low", Description = "The Lowest Priority" },
            //new TicketPriority { Name = "None", Description = "Not Prioritized" }

            var colorClass = "";

            switch (status)
            {
                case "Completed":
                    colorClass = "bg-green";
                    break;
                default:
                    break;
            }
            return colorClass;
        }
    }
}