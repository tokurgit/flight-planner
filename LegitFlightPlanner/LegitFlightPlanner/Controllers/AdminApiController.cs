using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using flight_planner_core.Models;
using flight_planner_core.Services;
using flight_planner_services;
using LegitFlightPlanner.Attributes;
using LegitFlightPlanner.Models;

namespace LegitFlightPlanner.Controllers
{
    [BasicAuthentication]
    [Route("admin-api")]
    public class AdminApiController : BasicApiController
    {
        static SemaphoreSlim semaphoreSlim = new SemaphoreSlim(1, 1);

        public AdminApiController(IFlightService flightService, IMapper mapper) : base(flightService, mapper)
        {
        }

        [HttpGet, Route("admin-api/flights/{id}")]
        public async Task<IHttpActionResult> GetFlightsById(int id)
        {
            var flight = await _flightService.GetById(id);
            if (flight == null)
            {
                return NotFound();
            }

            return Ok(
                _mapper.Map(flight, new FlightResponse())
                );
        }

        [HttpPut, Route("admin-api/flights")]
        public async Task<IHttpActionResult> Put(Flight flight)
        {
            if (flight == null)
            {
                return Conflict();
            }

            await semaphoreSlim.WaitAsync();
            try
            {
                if (FlightValidationService.IsFlightValid(flight))
                {
                    if (_flightService.HasSameFlight(flight))
                    {
                        return Conflict();
                    }

                    ServiceResult result = await _flightService.CreateFlight(flight);
                    int flightId = result.Entity.Id;
                    Flight addedFlight = await _flightService.GetById(flightId);
                    FlightResponse correctFlight = _mapper.Map<FlightResponse>(addedFlight);

                    return Created($"admin-api/flights/{correctFlight.Id}", correctFlight);
                }
            }
            finally
            {
                semaphoreSlim.Release();
            }

            return BadRequest();
        }

        [HttpDelete, Route("admin-api/flights/{id}")]
        public async Task<IHttpActionResult> Delete(int id)
        {
            Flight toBeDeleted = await _flightService.GetById(id);
            if (toBeDeleted == null)
            {
                return Ok();
            }
            _flightService.Delete(toBeDeleted);
            return Ok();
        }
    }
}
