using System.Net;
using System.Net.Http;
using System.Web.Http;
using FlightPlanner.Models;

namespace FlightPlanner.Controllers
{
    public class TestingController : ApiController
    {
       [Route("testing-api/clear")]
        public HttpResponseMessage Post(HttpRequestMessage message)
        {
            FlightStorage.RemoveAllFlights();

            if (FlightStorage.FlightCount() == 0)
            {
                return message.CreateResponse(HttpStatusCode.OK);
            }
            else
            {
                return message.CreateResponse(HttpStatusCode.NotFound);
            }
        }
    }
}
