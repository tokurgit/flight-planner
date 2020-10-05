using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using flight_planner_core.Models;
using flight_planner_core.Services;
using flight_planner_data;

namespace flight_planner_services
{
    public class AirportService : EntityService<Airport>, IAirportService
    {
        public AirportService(IFlightPlannerDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Airport>> GetAsync()
        {
            return await _ctx.Set<Airport>().ToListAsync();
        }

        public async Task<IEnumerable<Airport>> FindByIncompletePhrase(string phrase)
        {
            List<Airport> foundAirports = new List<Airport>();
            var airportList = await GetAsync();
            foreach (var airport in airportList)
            {
                if (SearchByPhrase(airport, phrase))
                {
                    foundAirports.Add(airport);
                }
            }

            return foundAirports;
        }

        public bool SearchByPhrase(Airport airport, string phrase)
        {
            var optimisedPhrase = phrase.Trim().ToLower();
            var city = airport.City.Trim().ToLower();
            var country = airport.Country.Trim().ToLower();
            var airportCode = airport.airport.Trim().ToLower();

            return city.Contains(optimisedPhrase) ||
                   country.Contains(optimisedPhrase) ||
                   airportCode.Contains(optimisedPhrase);
        }
    }
}
