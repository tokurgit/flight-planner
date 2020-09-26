using System;

namespace FlightPlanner.Models
{
    public class Airport
    {
        public string Country { get; set; }
        public string City { get; set; }
        public string airport { get; set; }
        public int Id { get; set; }

        private Airport()
        {

        }
        public Airport(string country, string city, string airport)
        {
            this.Country = country;
            this.City = city;
            this.airport = airport;
        }
        public bool IsEqual(object obj)
        {
            var other = obj as Airport;

            if (other != null && !other.IsNotValid())
            {
                return String.Equals(this.City.Trim(), other.City.Trim(),StringComparison.InvariantCultureIgnoreCase) &&
                       String.Equals(this.Country.Trim(), other.Country.Trim(), StringComparison.InvariantCultureIgnoreCase) &&
                       String.Equals(this.airport.Trim(), other.airport.Trim(), StringComparison.InvariantCultureIgnoreCase);
            }

            return false;
        }

        public bool IsNotValid()
        {
            if (!String.IsNullOrEmpty(this.City) ||
               !String.IsNullOrEmpty(this.Country) ||
               !String.IsNullOrEmpty(this.airport))
            {
                return false;
            }

            return true;
        }

        public bool IsAirportWhatLookingFor(string val)
        {
            var optimizedString = val.Trim().ToLower();
            var cityLowerCase = this.City.ToLower();
            var countryLowerCase = this.Country.ToLower();
            var airportLowerCase = this.airport.ToLower();

            if (cityLowerCase.Contains(optimizedString) ||
                countryLowerCase.Contains(optimizedString) ||
                airportLowerCase.Contains(optimizedString))
            {
                return true;
            }

            return false;
        }
    }
}