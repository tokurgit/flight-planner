using System.Collections.Generic;
using System.Threading.Tasks;
using flight_planner_core.Models;

namespace flight_planner_core.Services
{
    public interface IFlightService : IEntityService<Flight>
    {
        //DRIKST PARTAISIT VAJADZIGAS METODES PARTAISOT UZ TASK<IEnumerable> un citiem tipiem

        //drikst taisit jaunu servisu,,drikst taisit jaunus interfeisus

        Task<ServiceResult> CreateFlight(Flight flight);
        Task<Flight> GetFlightById(int id);
        Task<IEnumerable<Flight>> GetAsync();

        //TaskIEnumerable<Airport>> GetAirportAsync();
        //Task<IEnumerable<Airport>> FindByIncompletePhrase(string phrase);

        bool HasSameFlight(Flight flight);
    }
}
