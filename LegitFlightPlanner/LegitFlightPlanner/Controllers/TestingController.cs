using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using flight_planner_core.Models;
using flight_planner_core.Services;

namespace LegitFlightPlanner.Controllers
{
    public class TestingController : BasicApiController
    {
        [Route("testing-api/clear")]
        public IHttpActionResult Post()
        {
            foreach (var entry in _flightService.Get())
            {
                _flightService.Delete(entry);
            }

            //partaisit ,lai ir kads miniserviss,kas izdzes visus ierakstus no Datubazes, jo flightservice NEDRISKTETU izdzest
            //visus ierakstus no datubazes,max tikai flights, citadak nav logiski,nav ieverots SingleRespPrinciple

            if (_flightService.Query().ToList().Count == 0)
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        public TestingController(IFlightService flightService, IMapper mapper) : base(flightService, mapper)
        {
        }
    }
}