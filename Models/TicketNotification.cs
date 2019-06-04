using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MNBugTracker.Models
{
    public class TicketNotification
    {
        public int Id { get; set; }
        public int TicketId { get; set; }
        public string UserId { get; set; }
        public string CreaterId { get; set; }


        public string NotificationBody { get; set; }
        public DateTime Created { get; set; }
        public bool Unread { get; set; }

        //My Parents
        public virtual Ticket Ticket { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}