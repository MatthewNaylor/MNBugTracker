using MNBugTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MNBugTracker.Controllers
{
    public class GraphsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Graphs
        public JsonResult TicketsOnProjectByPriority()
        {
            var myData = new List<SeriesBarData>();
            var order = 1;
            foreach (var project in db.Projects.ToList())
            {
                myData.Add(new SeriesBarData
                {
                    BarData = new List<BarData>
                    {
                        new BarData
                        {
                            label = "Priority",
                            bars = new Bars{ fillColor = "blue", order = order},
                            color = "red",
                            data = new List<List<int>>
                            {
                                new List<int>{1, 5},
                                new List<int>{2, 10},
                                new List<int>{3, 10},
                                new List<int>{4, 10}
                            }
                        }                        
                    },
                    Ticks = new List<TickData> {
                            new TickData{Order = 1, Label = project.Name}
                    }
                });
                order++;
            }

            return Json(myData);
        }
    }
}