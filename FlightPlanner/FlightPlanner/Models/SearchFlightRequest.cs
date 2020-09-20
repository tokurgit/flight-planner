using System;

namespace FlightPlanner.Models
{
    public class SearchFlightRequest
    {
        public string From;
        public string To;
        public string DepartureDate;

        public SearchFlightRequest
        (
            string from,
            string to,
            string departureDate
        )
        {
            this.From = from;
            this.To = to;
            this.DepartureDate = departureDate;
        }

        public bool IsEqual()
        {
            return String.Equals(this.From, this.To);
        }

        public bool IsValid()
        {
            if (String.IsNullOrEmpty(this.From) ||
                String.IsNullOrEmpty(this.To) ||
                String.IsNullOrEmpty(this.DepartureDate))
            {
                return false;
            }

            if (this.IsEqual())
            {
                return false;
            }

            return true;
        }
    }
}