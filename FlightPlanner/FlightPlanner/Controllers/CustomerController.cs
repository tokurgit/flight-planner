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
        public HttpResponseMessage airports(HttpRequestMessage message, string search)
        {
            List<Airport> airports = new List<Airport>();
            foreach (var flight in FlightStorage.FlightDb)
            {
                if (flight.From.IsAirportWhatLookingFor(search))
                {
                    airports.Add(flight.From);
                }

                if (flight.To.IsAirportWhatLookingFor(search))
                {
                    airports.Add(flight.To);
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
                return message.CreateResponse(HttpStatusCode.OK, flight);
            }
        }

        [HttpPost, Route("api/flights/search")]
        public HttpResponseMessage Post(HttpRequestMessage message, SearchFlightRequest request)
        {
            var pageResult = new PageResult<Flight>();
            if (request != null && request.IsValid())
            {
                var allFlights = FlightStorage.FindAllFlightsForCustomer(request);
                pageResult.Items = allFlights.ToArray();
                pageResult.TotalItems = pageResult.Items.Length;
                return message.CreateResponse(HttpStatusCode.OK, pageResult);
            }

            return message.CreateResponse(HttpStatusCode.BadRequest);
        }
    }
}
