using System.Linq;
using System.Web.Http;
using AutoMapper;
using flight_planner_core.Services;

namespace LegitFlightPlanner.Controllers
{
    public class TestingController : BasicApiController
    {
        private readonly IAirportService _airportService;

        public TestingController(IAirportService airportService, IFlightService flightService, IMapper mapper) : base(flightService, mapper)
        {
            _airportService = airportService;
        }

        [Route("testing-api/clear")]
        public IHttpActionResult Post()
        {
            foreach (var entry in _flightService.Get())
            {
                _flightService.Delete(entry);
            }

            foreach (var airport in _airportService.Get())
            {
                _airportService.Delete(airport);
            }
            
            if (_flightService.Query().ToList().Count == 0)
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
    }
}