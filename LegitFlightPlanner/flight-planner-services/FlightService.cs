using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
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
    }
}
