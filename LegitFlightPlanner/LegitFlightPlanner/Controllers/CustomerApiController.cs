using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using flight_planner_core.Services;
using LegitFlightPlanner.Models;

//using FlightPlanner.Models;

namespace LegitFlightPlanner.Controllers
{
    [Route("api")]
    public class CustomerController : BasicApiController
    {
        [HttpGet, Route("api/airports")]
        public async Task<IHttpActionResult> Airports(string search)
        {
            var listOfAirportResponse = new List<AirportResponse>();
            var listOfAirports = await _airportService.FindByIncompletePhrase(search);
            foreach (var airport in listOfAirports)
            {
                var temp = _mapper.Map<AirportResponse>(airport);
                listOfAirportResponse.Add(temp);
            }
            if (listOfAirportResponse.Count > 0) return Ok(listOfAirportResponse.ToArray());

            return NotFound();
        }

        [HttpGet, Route("api/flights/{id}")]
        public async Task<IHttpActionResult> Get(int id)
        {
            var flight = await _flightService.GetFlightById(id);
            if (flight == null)
            {
                return NotFound();
            }
            else
            {
                var flightInValidFormat = _mapper.Map<FlightResponse>(flight);
                return Ok(flightInValidFormat);
            }
        }

        [HttpPost, Route("api/flights/search")]
        public async Task<IHttpActionResult> Post(SearchFlightRequest request)
        {
            if (request != null && request.IsValidRequest())
            {
                var pageResult = await new PageResult().FindBySearchRequest(request,_flightService);  
                return Ok(pageResult);
            }

            return BadRequest();
        }

        public CustomerController(IFlightService flightService, IMapper mapper) : base(flightService, mapper)
        {
        }
    }
}
