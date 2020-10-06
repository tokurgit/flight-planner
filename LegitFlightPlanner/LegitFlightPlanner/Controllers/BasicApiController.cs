using System.Web.Http;
using AutoMapper;
using flight_planner_core.Services;

namespace LegitFlightPlanner.Controllers
{
    public class BasicApiController : ApiController
    {
        protected readonly IFlightService _flightService;
        protected readonly IMapper _mapper;
        public BasicApiController(IFlightService flightService, IMapper mapper)
        {
            _flightService = flightService;
            _mapper = mapper;
        }
    }
}
