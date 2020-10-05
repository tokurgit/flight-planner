using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using flight_planner_core.Models;
using flight_planner_core.Services;
using flight_planner_data;

namespace flight_planner_services
{
    public class FlightService : EntityService<Flight>, IFlightService
    {
        public FlightService(IFlightPlannerDbContext context) : base(context)
        {
        }

        public async Task<ServiceResult> CreateFlight(Flight flight)
        {
            if (flight == null || HasSameFlight(flight))
            {
                throw new ArgumentException(nameof(flight));
            }
            //could be optimized, repeating pattern 3x

            _ctx.Flights.Add(flight);
            await _ctx.SaveChangesAsync();

            return new ServiceResult(true).Set(flight);
        }
        public async Task<Flight> GetFlightById(int id)
        {
            return await _ctx.Flights.Include(f=>f.From).Include(t=>t.To).SingleOrDefaultAsync(e => e.Id == id);
        }

        public async Task<IEnumerable<Flight>> GetAsync()
        {
            return await _ctx.Set<Flight>().Include(f => f.From).Include(f => f.To).ToListAsync();
        }

        public bool HasSameFlight(Flight flight)
        {
            var wholeList = _ctx.Flights.Include(f => f.From).Include(f => f.To).ToList();
            return wholeList.Any(f => FlightValidationService.IsTheSameFlight(f,flight));
        }
        //------------basicApiCOntroller nevar innicializēt AirportService ar ctor overload, tāpēc liku šeit, lai izietu testus

        //public IEnumerable<Airport> GetAirports()
        //{
        //    //return _ctx.Set<Airport>().ToList();
        //    var c = Get<Airport>();
        //    return c;
        //}

        //public async Task<IEnumerable<Airport>> FindByIncompletePhrase(string phrase)
        //{
        //    List<Airport> foundAirports = new List<Airport>();
        //    var airportList =await GetAsync();
        //    foreach (var flight in airportList)
        //    {

        //        if (SearchByPhrase(flight.From, phrase))
        //        {
        //            foundAirports.Add(flight.From);
        //        }

        //        if (SearchByPhrase(flight.To, phrase))
        //        {
        //            foundAirports.Add(flight.To);
        //        }
        //    }

        //    return foundAirports;
        //}

        //private static bool SearchByPhrase(Airport airport, string phrase)
        //{
        //    var optimisedPhrase = phrase.Trim().ToLower();
        //    var city = airport.City.Trim().ToLower();
        //    var country = airport.Country.Trim().ToLower();
        //    var airportCode = airport.airport.Trim().ToLower();

        //    return city.Contains(optimisedPhrase) ||
        //           country.Contains(optimisedPhrase) ||
        //           airportCode.Contains(optimisedPhrase);
        //}
    }
}
