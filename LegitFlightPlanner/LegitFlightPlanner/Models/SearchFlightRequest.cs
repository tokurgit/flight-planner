using System;

namespace LegitFlightPlanner.Models
{
    public class SearchFlightRequest
    {
        public string From { get; set; }
        public string To { get; set; }
        public string DepartureDate { get; set; }
        public SearchFlightRequest(string from, string to, string departTime)
        {
            From = from;
            To = to;
            DepartureDate = departTime;
        }

        public bool IsValidRequest()
        {
            if (String.IsNullOrEmpty(this.From) ||
                String.IsNullOrEmpty(this.To) ||
                String.IsNullOrEmpty(this.DepartureDate))
            {
                return false;
            } 
            return!string.Equals(this.From, this.To);
        }
    }
}