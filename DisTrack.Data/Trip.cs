using System;
using System.Collections.Generic;
using System.Text;

namespace DisTrack.Data
{
    public class Trip
    {
        public int Id { get; set; }
        public string Departure { get; set; }
        public string Destination { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public double Miles { get; set; }
        public decimal Cost { get; set; }
    }
}
