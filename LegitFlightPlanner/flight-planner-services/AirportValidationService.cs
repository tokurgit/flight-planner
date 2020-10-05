using System;
using System.CodeDom;
using flight_planner_core.Models;
using static System.String;

namespace flight_planner_services
{
    public class AirportValidationService
    {
        public static bool IsAirportValid(Airport airport)
        {
            if (airport == null)
            {
                return false;
            }
            return IsCountryValid(airport) &&
                   IsCityValid(airport) &&
                   IsAirportCodeValid(airport);
        }

        public static bool AreAirportsUnique(Airport from, Airport to)
        {
            if (!IsAirportValid(from) && !IsAirportValid(to)) return false;

            return !string.Equals(from.City.Trim(), to.City.Trim(), StringComparison.InvariantCultureIgnoreCase) &&
                   !string.Equals(from.airport.Trim(), to.airport.Trim(), StringComparison.InvariantCultureIgnoreCase);
        }

        public static bool AreIdenticalAirports(Airport from, Airport to)
        {
            return !AreAirportsUnique(from, to);
        }

        private static bool IsCountryValid(Airport airport)
        {
            return !IsNullOrEmpty(airport.Country);
        }

        private static bool IsCityValid(Airport airport)
        {
            return !IsNullOrEmpty(airport.City);
        }

        private static bool IsAirportCodeValid(Airport airport)
        {
            return !IsNullOrEmpty(airport.airport);
        }
    }
}
