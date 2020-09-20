using System;
using System.Collections.Generic;
using System.Linq;

namespace FlightPlanner.Models
{
    public class FlightStorage
    {
        public static List<Flight> FlightDb = new List<Flight>();
        public static object x = new object();
        private static int _id;

        private static List<Flight> GetFlightList()
        {
            lock (x)
            {
                var flightList = FlightDb.ToList();
                return flightList;
            }
        }

        public static Flight GetFlightById(int id)
        {
            var flightList = GetFlightList();
            var flight = flightList.FirstOrDefault(x => x.Id == id);
            return flight;
        }

        public static int FlightCount()
        {
            lock (x)
            {
                var count = FlightDb.Count;
                return count;
            }
        }

        public static bool HasSameFlight(Flight flight)
        {
            var flightList = GetFlightList();
            var hasSameFlight = flightList.Any(x => x.IsEqual(flight));
            return hasSameFlight;
        }

        public static void Add(Flight flight)
        {
            _id++;
            flight.Id = _id;
            if (!HasSameFlight(flight))
            {
                lock (x)
                {
                    FlightDb.Add(flight);
                }
            }
        }

        public static int FindIndex(Flight flight)
        {
            var flightList = GetFlightList();
            var index = flightList.ToList().IndexOf(flight);
            return index;
        }

        public static void RemoveFlightAtIndex(int id)
        {
            lock (x)
            {
                FlightDb.RemoveAt(id);
            }
        }

        public static void RemoveAllFlights()
        {
            lock (x)
            {
                FlightDb.Clear();
            }
        }

        public static List<Flight> FindAllFlightsForCustomer(SearchFlightRequest request)
        {

            var flightList = GetFlightList();
            var allFlights = flightList
                .Where(x => x.From.airport == request.From)
                .Where(x => x.To.airport == request.To)
                .Where(x => (DateTime.Parse(x.DepartureTime).Date).CompareTo(DateTime.Parse(request.DepartureDate).Date) == 0)
                .ToList();
            return allFlights;
        }
    }
}
