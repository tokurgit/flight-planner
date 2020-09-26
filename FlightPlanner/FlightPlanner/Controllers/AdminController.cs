using System.Net;
using System.Net.Http;
using System.Web.Http;
using FlightPlanner.Attributes;
using FlightPlanner.Models;

namespace FlightPlanner.Controllers
{
    [BasicAuthentification]
    [Route("admin-api")]
    public class AdminController : ApiController
    {
        [HttpGet, Route("admin-api/flights/{id}")]
        public HttpResponseMessage flights(HttpRequestMessage message, int id)
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

        [HttpPut, Route("admin-api/flights")]
        public HttpResponseMessage Put(HttpRequestMessage message, Flight flight)
        {
            var hasSameFlight = FlightStorage.HasSameFlight(flight);
            if (hasSameFlight)
            {
                return message.CreateResponse(HttpStatusCode.Conflict);
            }
            else
            {
                if (!flight.IsNotValidEntry())
                {
                    var addedFlight = FlightStorage.Add(flight);
                    var fromAirport = AirportNoId.CreateFromAirportFromFlight(flight);
                    var toAirport = AirportNoId.CreateToAirportFromFlight(flight);
                    var correctFlight = FlightNoId.CreateFromFlightAndAirportNoId(addedFlight, fromAirport, toAirport);
                    return message.CreateResponse(HttpStatusCode.Created, correctFlight);
                }

                return message.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        [HttpDelete, Route("admin-api/flights/{id}")]
        public HttpResponseMessage Delete(HttpRequestMessage message, int id)
        {
            var flight = FlightStorage.GetFlightById(id);
            if (flight == null)
            {
                return message.CreateResponse(HttpStatusCode.OK);
            }
            else
            {
                FlightStorage.RemoveFlightById(id);
                return message.CreateResponse(HttpStatusCode.OK, flight);
            }
        }
    }
}