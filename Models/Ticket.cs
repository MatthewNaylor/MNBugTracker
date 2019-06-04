using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MNBugTracker.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        [Display(Name = "Ticket Type")]
        public int TicketTypeId { get; set; }
        [Display(Name = "Ticket Status")]
        public int TicketStatusId { get; set; }
        [Display(Name = "Ticket Priority")]
        public int TicketPriorityId { get; set; }
        [Display(Name = "Owner")]
        public string OwnerUserId { get; set; }
        [Display(Name = "Assinged User")]
        public string AssignedToUserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTimeOffset Created { get; set; }
        public DateTimeOffset? Updated { get; set; }

        //Missing Navigational Properties

        //All of my Parents
        public virtual Project Project { get; set; }
        public virtual TicketType TicketType { get; set; }
        public virtual TicketPriority TicketPriority { get; set; }
        public virtual TicketStatus TicketStatus { get; set; }
        public virtual ApplicationUser OwnerUser { get; set; }
        public virtual ApplicationUser AssignedToUser { get; set; }

        //All of my Children
        public virtual ICollection<TicketComment> TicketComments { get; set; }
        public virtual ICollection<TicketAttachment> TicketAttachments { get; set; }
        public virtual ICollection<TicketHistory> TicketHistories { get; set; }
        public virtual ICollection<TicketNotification> TicketNotifications { get; set; }

        public Ticket()
        {
            TicketComments = new HashSet<TicketComment>();
            TicketAttachments = new HashSet<TicketAttachment>();
            TicketHistories = new HashSet<TicketHistory>();
            TicketNotifications = new HashSet<TicketNotification>();
        }


    }
}