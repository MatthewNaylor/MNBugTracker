using MNBugTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MNBugTracker.Helpers
{
    public class NotificationHelper
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public void TriggerAssignmentNotifications(int ticketId, string oldDeveloper, string newDeveloper)
        {
            var newAssingnment = string.IsNullOrEmpty(oldDeveloper) && !string.IsNullOrEmpty(newDeveloper);
            var unAssingnment = !string.IsNullOrEmpty(oldDeveloper) && string.IsNullOrEmpty(newDeveloper);
            var reAssingnment = !string.IsNullOrEmpty(oldDeveloper) && !string.IsNullOrEmpty(newDeveloper) && (oldDeveloper != newDeveloper);

            if (newAssingnment)
            {
                AddAssignmentNotification(ticketId, newDeveloper);
            }
            else if (unAssingnment)
            {
                AddUnAssignmentNotification(ticketId, oldDeveloper);
            }
            else if (reAssingnment)
            {
                AddUnAssignmentNotification(ticketId, oldDeveloper);
                AddAssignmentNotification(ticketId, newDeveloper);
            }
        }

        private void AddAssignmentNotification(int ticketId, string newDeveloper)
        {

            var newNotification = new TicketNotification
            {
                Created = DateTime.Now,
                TicketId = ticketId,
                Unread = true,
                UserId = newDeveloper,
                NotificationBody = $"You've been assigned to ticket: {ticketId}."
            };

            db.TicketNotifications.Add(newNotification);
            db.SaveChanges();
        }

        private void AddUnAssignmentNotification(int ticketId, string oldDeveloper)
        {
            var newNotification = new TicketNotification
            {
                Created = DateTime.Now,
                TicketId = ticketId,
                Unread = true,
                UserId = oldDeveloper,
                NotificationBody = $"You've been removed from ticket: {ticketId}."
            };

            db.TicketNotifications.Add(newNotification);
            db.SaveChanges();
        }
    }
}