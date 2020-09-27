using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FlightPlanner.Models;

namespace FlightPlanner.Controllers
{
    [Route("api")]
    public class CustomerController : ApiController
    {
        [HttpGet, Route("api/airports")]
        public HttpResponseMessage Airports(HttpRequestMessage message, string search)
        {
            var airports = new List<AirportNoId>();
            foreach (var flight in FlightStorage.GetFlightList())
            {
                if (flight.From.IsAirportWhatLookingFor(search))
                {
                    var fromAirport = AirportNoId.CreateFromAirportFromFlight(flight);
                    airports.Add(fromAirport);
                }

                if (flight.To.IsAirportWhatLookingFor(search))
                {
                    var toAirport = AirportNoId.CreateToAirportFromFlight(flight);
                    airports.Add(toAirport);
                }
            }

            if (airports.Count > 0)
            {
                return message.CreateResponse(HttpStatusCode.OK, airports.ToArray());
            }

            return message.CreateResponse(HttpStatusCode.NotFound);
        }

        [HttpGet, Route("api/flights/{id}")]
        public HttpResponseMessage Get(HttpRequestMessage message, int id)
        {
            var flight = FlightStorage.GetFlightById(id);
            if (flight == null)
            {
                return message.CreateResponse(HttpStatusCode.NotFound);
            }
            else
            {
                var flightInValidFormat = FlightNoId.CreateFromFlight(flight);
                return message.CreateResponse(HttpStatusCode.OK, flightInValidFormat);
            }
        }

        [HttpPost, Route("api/flights/search")]
        public HttpResponseMessage Post(HttpRequestMessage message, SearchFlightRequest request)
        {
            if (request != null && request.IsValid())
            {
                var pageResult = FlightStorage.SearchFlights(request); 
                return message.CreateResponse(HttpStatusCode.OK, pageResult);
            }

            return message.CreateResponse(HttpStatusCode.BadRequest);
        }
    }
}
