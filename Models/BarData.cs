using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MNBugTracker.Models
{
    public class TickData
    {
        public int Order { get; set; }
        public string Label { get; set; }
    }

    public class SeriesBarData
    {
        public List<TickData> Ticks = new List<TickData>();
        public List<BarData> BarData = new List<BarData>();
    }

    public class BarData
    {
        public string label { get; set; }
        public List<List<int>> data { get; set; }
        public Bars bars { get; set; }
        public string color { get; set; }

        public BarData()
        {
            data = new List<List<int>>();
        }
    }

    public class Bars
    {
        public int order { get; set; }
        public string fillColor { get; set; }
    }
}