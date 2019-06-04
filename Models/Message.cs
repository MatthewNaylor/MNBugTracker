using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MNBugTracker.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string FromUser { get; set; }
        public string ToUser { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTime Created { get; set; }
    }
}