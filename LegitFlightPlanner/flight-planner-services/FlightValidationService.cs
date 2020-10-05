using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using flight_planner_core.Models;
using static System.String;

namespace flight_planner_services
{
    public class FlightValidationService : AirportValidationService
    {
        public static bool IsFlightValid(Flight flight)
        {
            if (flight == null) return false;

            if (
                IsCarrierValid(flight) &&
                IsDepartureTimeValid(flight) &&
                IsArrivalTimeValid(flight) &&
                IsAirportValid(flight.From) &&
                IsAirportValid(flight.To)
                )
            {
                return AreDatesValid(flight) && AreAirportsUnique(flight.From, flight.To);
            }

            return false;
        }

        public static bool IsTheSameFlight(Flight flightOne, Flight flightTwo)
        {
            var isValidFlight = IsFlightValid(flightTwo);
            if (flightOne != null && isValidFlight)
            {
                var fromEq = AreIdenticalAirports(flightOne.From,flightTwo.From);
                var toEq = AreIdenticalAirports(flightOne.To, flightTwo.To);
                var carrEq = string.Equals(flightOne.Carrier.Trim(), flightTwo.Carrier.Trim(),
                    StringComparison.InvariantCultureIgnoreCase);
                var depTimeEq = string.Equals(flightOne.DepartureTime.Trim(), flightTwo.DepartureTime.Trim(),
                    StringComparison.InvariantCultureIgnoreCase);
                var arrTimeEq = string.Equals(flightOne.ArrivalTime.Trim(), flightTwo.ArrivalTime.Trim(), StringComparison.InvariantCultureIgnoreCase);

                return fromEq && toEq && carrEq && depTimeEq && arrTimeEq;
            }

            return false;
        }
        private static bool IsCarrierValid(Flight flight)
        {
            return !IsNullOrEmpty(flight.Carrier);
        }

        private static bool IsDepartureTimeValid(Flight flight)
        {
            return !IsNullOrEmpty(flight.DepartureTime);
        }

        private static bool IsArrivalTimeValid(Flight flight)
        {
            return !IsNullOrEmpty(flight.ArrivalTime);
        }

        private static bool AreDatesValid(Flight flight)
        {
            var departureTime = flight.DepartureTime;
            var arrivalTime = flight.ArrivalTime;

            if (DateTime.Parse(departureTime).CompareTo(DateTime.Parse(arrivalTime)) < 0)
            {
                return true;
            }
            return false;
        }
    }
}
