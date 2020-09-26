using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace FlightPlanner.Models
{
    public class FlightStorage
    {
        public static List<Flight> FlightDb = new List<Flight>();
        public static object x = new object();

        public static List<Flight> GetFlightList()
        {
            lock (x)
            {
                using (var context = new FlightPlannerDbContext())
                {
                    var flightList = context.Flights.Include(f => f.From).Include(f => f.To).ToList();
                    return flightList;
                }
            }
        }

        public static Flight GetFlightById(int id)
        {
            using (var context = new FlightPlannerDbContext())
            {
                var flight = context.Flights
                    .Include(f => f.To)
                    .Include(f => f.From)
                    .SingleOrDefault(f => f.Id == id);
                return flight;
            }
        }

        public static int FlightCount()
        {
            lock (x)
            {
                using (var context = new FlightPlannerDbContext())
                {
                    return context.Flights.Count();
                }
            }
        }

        public static bool HasSameFlight(Flight flight)
        {
            lock (x)
            {
                using (var context = new FlightPlannerDbContext())
                {
                    var localList = context.Flights.Include(f => f.From).Include(f => f.To).ToList();
                    return localList.Any(f => f.IsEqual(flight));
                }
            }
        }

        public static Flight Add(Flight flight)
        {
            lock (x)
            {
                using (var context = new FlightPlannerDbContext())
                {
                    var addedFlight = context.Flights.Add(flight);
                    context.SaveChanges();
                    return addedFlight;
                }
            }
        }

        public static void RemoveFlightById(int id)
        {
            lock (x)
            {
                using (var context = new FlightPlannerDbContext())
                {
                    var flight = context.Flights.SingleOrDefault(f => f.Id == id);
                    if (flight == null)
                        return;
                    context.Flights.Remove(flight);
                    context.SaveChanges();
                }
            }
        }

        public static void RemoveAllFlights()
        {
            lock (x)
            {
                using (var context = new FlightPlannerDbContext())
                {
                    context.Flights.RemoveRange(context.Flights);
                    context.SaveChanges();
                }
            }
        }

        public static PageResult<Flight> SearchFlights(SearchFlightRequest request)
        {
            lock (x)
            {
                using (var context = new FlightPlannerDbContext())
                {
                    var flightList = context.Flights.Include(f => f.From).Include(f => f.To).ToList();
                    var allFlights = flightList
                        .Where(x => x.From.airport == request.From)
                        .Where(x => x.To.airport == request.To)
                        .Where(x =>
                            (DateTime.Parse(x.DepartureTime).Date).CompareTo(DateTime.Parse(request.DepartureDate)
                                .Date) == 0)
                        .Distinct()
                        .ToArray();
                    return new PageResult<Flight>() { Items = allFlights, TotalItems = allFlights.Length };
                }
            }
        }
    }
}
