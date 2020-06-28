using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DisTrack.ViewModels
{
    public class TripViewModel
    {
        public string Departure { get; set; }
        public string Destination { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public double Miles { get; set; }
        public decimal Cost { get; set; } 
    }
}
