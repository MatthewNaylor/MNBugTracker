using Microsoft.AspNet.Identity;
using MNBugTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MNBugTracker.Helpers
{
    public class TicketHistoryHelper
    {
        private static ApplicationDbContext db = new ApplicationDbContext();

        public void RecordTicketChanges(Ticket oldTicket, Ticket newTicket)
        {

            if (oldTicket.TicketTypeId != newTicket.TicketTypeId)
            {
                AddTicketHistory(newTicket.Id, "TicketTypeId", oldTicket.TicketTypeId.ToString(), newTicket.TicketTypeId.ToString());
            }

            if (oldTicket.TicketStatusId != newTicket.TicketStatusId)
            {
                AddTicketHistory(newTicket.Id, "TicketStatusId", oldTicket.TicketStatusId.ToString(), newTicket.TicketStatusId.ToString());

            }

            if (oldTicket.TicketPriorityId != newTicket.TicketPriorityId)
            {
                AddTicketHistory(newTicket.Id, "TicketPriorityId", oldTicket.TicketPriorityId.ToString(), newTicket.TicketPriorityId.ToString());

            }

            if (oldTicket.AssignedToUserId != newTicket.AssignedToUserId)
            {
                AddTicketHistory(newTicket.Id, "AssignedToUserId", oldTicket.AssignedToUserId, newTicket.AssignedToUserId);

            }

            if (oldTicket.Title != newTicket.Title)
            {
                AddTicketHistory(newTicket.Id, "Title", oldTicket.Title, newTicket.Title);

            }
        }

        public void AddTicketHistory(int ticketId, string propertyName, string oldValue, string newValue)
        {
            var newTicketHistory = new TicketHistory
            {
                Property = propertyName,
                OldValue = oldValue,
                NewValue = newValue,
                ChangedDate = DateTime.Now,
                UserId = HttpContext.Current.User.Identity.GetUserId(),
                TicketId = ticketId
            };

            db.TicketHistories.Add(newTicketHistory);
            db.SaveChanges();
        }

        public static string GetHistoryInfo(string id, string property)
        {
            if (string.IsNullOrEmpty(id))
            {
                return "";
            }
            var info = id;
            switch (property)
            {
                case "AssignedToUserId":
                case "OwnerUserId":
                    info = db.Users.Find(id).Email;
                    break;
                case "TicketStatusId":
                    info = db.TicketStatuses.Find(Convert.ToInt32(id)).Name;
                    break;
                case "TicketPriorityId":
                    info = db.TicketPriorities.Find(Convert.ToInt32(id)).Name;
                    break;
                case "TicketTypeId":
                    info = db.TicketTypes.Find(Convert.ToInt32(id)).Name;
                    break;
                default:
                    break;
            }
            return info;
        }
    }
}