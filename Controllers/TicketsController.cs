using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using MNBugTracker.Helpers;
using MNBugTracker.Models;

namespace MNBugTracker.Controllers
{
    public class TicketsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private UserRolesHelper roleHelper = new UserRolesHelper();
        private ProjectsHelper projectHelper = new ProjectsHelper();
        private NotificationHelper notificationHelper = new NotificationHelper();
        private TicketHelper ticketHelper = new TicketHelper();
        private TicketHistoryHelper historyHelper = new TicketHistoryHelper();


        [Authorize(Roles = "Admin, ProjectManager, Developer, Submitter")]
        public ActionResult MyIndex()
        {
            var userId = User.Identity.GetUserId();
            var myTickets = projectHelper.ListUserTickets(userId);
            return View("Index", myTickets.Where(t => t.TicketStatus.Name != "Completed").ToList());
        }

        public ActionResult ProjectTicketIndex(int id)
        {
            var tickets = db.Projects.Find(id).Tickets.ToList();
            return View("Index", tickets.Where(t => t.TicketStatus.Name != "Completed").ToList());
        }

        [Authorize (Roles = "Admin")]
        public ActionResult CompletedIndex()
        {
            var tickets = db.Tickets;
            return View("Index", tickets.Where(t => t.TicketStatus.Name == "Completed").ToList());
        }

        // GET: Tickets
        [Authorize (Roles = "Admin")]
        public ActionResult Index()
        {
            var tickets = db.Tickets;
            return View(tickets.ToList());
        }

        [Authorize]
        //GET:
        public ActionResult DetailsUpload(int id)
        {
            ViewBag.TicketId = id;
            return View();
        }

        // GET: Tickets/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = db.Tickets.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            return View(ticket);
        }

        // GET: Tickets/Create
        [Authorize(Roles = "Submitter")]
        public ActionResult Create(int? projectId)
        {
            var myTicket = new Ticket();

            if(projectId == null)
            {
                ViewBag.ProjectId = new SelectList(db.Projects, "Id", "Name");
                myTicket.ProjectId = -1;
            }
            else
            {
                myTicket.ProjectId = (int)projectId;
            }
            

            ViewBag.TicketStatusId = new SelectList(db.TicketStatuses, "Id", "Name");
            ViewBag.TicketTypeId = new SelectList(db.TicketTypes, "Id", "Name");
            ViewBag.TicketPriorityId = new SelectList(db.TicketPriorities, "Id", "Name");
            return View(myTicket);
        }

        // POST: Tickets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProjectId,TicketTypeId,TicketStatusId,Title,Description,TicketPriorityId")] Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                ticket.OwnerUserId = User.Identity.GetUserId();
                ticket.TicketStatusId = db.TicketStatuses.FirstOrDefault(t => t.Name == "New/Unassigned").Id;
                ticket.Created = DateTimeOffset.Now;
                db.Tickets.Add(ticket);
                db.SaveChanges();
                return RedirectToAction("MyIndex");
            }

            ViewBag.ProjectId = new SelectList(db.Projects, "Id", "Name", ticket.ProjectId);
            ViewBag.TicketStatusId = new SelectList(db.TicketStatuses, "Id", "Name", ticket.TicketStatusId);
            ViewBag.TicketTypeId = new SelectList(db.TicketTypes, "Id", "Name", ticket.TicketTypeId);
            ViewBag.TicketPriorityId = new SelectList(db.TicketPriorities, "Id", "Name", ticket.TicketPriorityId);
            return View(ticket);
        }

        // GET: Tickets/Edit/5
        [Authorize(Roles = "Admin,ProjectManager,Developer,Submitter")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Ticket ticket = db.Tickets.Find(id);
            var userId = User.Identity.GetUserId();

            if ((User.IsInRole("Submitter") && ticket.OwnerUserId != userId) ||
                (User.IsInRole("Developer") && ticket.AssignedToUserId != userId))
            {
                return RedirectToAction("Oops", "Admin", new { id = ticket.Id});
            }
            
            if (ticket == null)
            {
                return HttpNotFound();
            }
            var developers = new List<ApplicationUser>();

            var usersOnProject = projectHelper.UsersOnProject(ticket.ProjectId);
            foreach (var user in usersOnProject)
            {
                if (roleHelper.IsUserInRole(user.Id, "Developer"))
                {
                    developers.Add(user);
                }
            }

            ViewBag.AssignedToUserId = new SelectList(developers, "Id", "FullContactinfo", ticket.AssignedToUserId);
            ViewBag.TicketPriorityId = new SelectList(db.TicketPriorities, "Id", "Name", ticket.TicketPriorityId);
            //ViewBag.Developers = new SelectList(db.Users, "Id", "FirstName", ticket.AssignedToUserId);
            //ViewBag.Submitters = new SelectList(db.Users, "Id", "FirstName", ticket.OwnerUserId);
            ViewBag.ProjectId = new SelectList(db.Projects, "Id", "Name", ticket.ProjectId);
            ViewBag.TicketStatusId = new SelectList(db.TicketStatuses, "Id", "Name", ticket.TicketStatusId);
            ViewBag.TicketTypeId = new SelectList(db.TicketTypes, "Id", "Name", ticket.TicketTypeId);
            return View(ticket);
        }

        // POST: Tickets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ProjectId,TicketTypeId,TicketPriorityId,TicketStatusId,OwnerUserId,AssignedToUserId,Title,Description,Created,Updated")] Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                //var oldAssignedToUserId = db.Tickets.AsNoTracking().FirstOrDefault(t => t.Id == ticket.Id).AssignedToUserId;
                //var oldTicketStatusId = db.Tickets.AsNoTracking().FirstOrDefault(t => t.Id == ticket.Id).TicketStatusId;

                var oldTicket = db.Tickets.AsNoTracking().FirstOrDefault(t => t.Id == ticket.Id);
                notificationHelper.TriggerAssignmentNotifications(ticket.Id, oldTicket.AssignedToUserId, ticket.AssignedToUserId);

                if (oldTicket.TicketStatusId == ticket.TicketStatusId)
                {
                    ticket.TicketStatusId = ticketHelper.GetNewTicketStatus(oldTicket.AssignedToUserId, ticket.AssignedToUserId);
                }




                
                ticket.Updated = DateTime.Now;
                db.Entry(ticket).State = EntityState.Modified;
                db.SaveChanges();
                historyHelper.RecordTicketChanges(oldTicket, ticket);
                return RedirectToAction("MyIndex");
            }
            ViewBag.AssignedToUserId = new SelectList(db.Users, "Id", "FullContactinfo", ticket.AssignedToUserId);
            ViewBag.OwnerUserId = new SelectList(db.Users, "Id", "FirstName", ticket.OwnerUserId);
            ViewBag.ProjectId = new SelectList(db.Projects, "Id", "Name", ticket.ProjectId);
            ViewBag.TicketStatusId = new SelectList(db.TicketStatuses, "Id", "Name", ticket.TicketStatusId);
            ViewBag.TicketTypeId = new SelectList(db.TicketTypes, "Id", "Name", ticket.TicketTypeId);
            return View(ticket);
        }

        // GET: Tickets/Delete/5
        [Authorize (Roles = "Admin,ProjectManager,Developer,Submitter")]
        public ActionResult Delete(int? id)
        {
            Ticket ticket = db.Tickets.Find(id);
            var userId = User.Identity.GetUserId();

            if ((User.IsInRole("Submitter") && ticket.OwnerUserId != userId) ||
                (User.IsInRole("Developer") && ticket.OwnerUserId != userId))
            {
                return RedirectToAction("Login", "Account");
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (ticket == null)
            {
                return HttpNotFound();
            }
            return View(ticket);
        }

        // POST: Tickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ticket ticket = db.Tickets.Find(id);
            db.Tickets.Remove(ticket);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult MarkAsCompleted(int id)
        {
            var ticket = db.Tickets.Find(id);
            ticket.TicketStatusId = db.TicketStatuses.FirstOrDefault(t => t.Name == "Completed").Id;

            db.SaveChanges();
            return RedirectToAction("MyIndex");
        }
    }
}
