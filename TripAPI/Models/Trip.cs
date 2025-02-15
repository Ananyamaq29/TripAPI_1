using System;

namespace TripAPI.Models
{
    public class Trip
    {
        public int Id { get; set; } // Primary key
        public string Destination { get; set; } // Destination of the trip
        public DateTime StartDate { get; set; } // Start date of the trip
        public DateTime EndDate { get; set; } // End date of the trip
        public string Preferences { get; set; } // User preferences for the trip
    }
}