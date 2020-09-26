using System;

namespace FlightPlanner.Models
{
    public class Flight
    {
        public int Id { get; set; }
        public Airport From { get; set; }
        public Airport To { get; set; }
        public string Carrier { get; set; }
        public string DepartureTime { get; set; }
        public string ArrivalTime { get; set; }

        public bool IsEqual(object obj)
        {
            var other = obj as Flight;
            if (other != null && !other.IsNotValidEntry())
            {
                var fromEq = this.From.IsEqual(other.From);
                var toEq = this.To.IsEqual(other.To);
                var carrEq = String.Equals(this.Carrier.Trim(), other.Carrier.Trim(),
                    StringComparison.InvariantCultureIgnoreCase);
                var depTimeEq = String.Equals(this.DepartureTime.Trim(), other.DepartureTime.Trim(),
                    StringComparison.InvariantCultureIgnoreCase);
                var arrTimeEq = String.Equals(this.ArrivalTime.Trim(), other.ArrivalTime.Trim(), StringComparison.InvariantCultureIgnoreCase);

                return fromEq && toEq && carrEq && depTimeEq && arrTimeEq;
            }

            return false;
        }

        public bool IsNotValidEntry()
        {
            if (this.From == null ||
                this.To == null ||
                String.IsNullOrEmpty(this.Carrier) ||
                String.IsNullOrEmpty(this.DepartureTime) ||
                String.IsNullOrEmpty(this.ArrivalTime))
            {
                return true;
            }
            else if (this.From.IsNotValid() || this.To.IsNotValid())
            {
                return true;
            }
            else if (!this.From.IsEqual(this.To) && DateTime.Parse(DepartureTime).CompareTo(DateTime.Parse(ArrivalTime)) < 0)
            {
                return false;
            }

            return true;
        }
    }
}