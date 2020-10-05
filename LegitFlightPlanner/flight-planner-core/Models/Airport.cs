using System;

namespace flight_planner_core.Models
{
    public class Airport : Entity
    {
        public string Country { get; set; }
        public string City { get; set; }
        public string airport { get; set; }

        private Airport()
        {

        }
        public Airport(string country, string city, string airport)
        {
            this.Country = country;
            this.City = city;
            this.airport = airport;
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