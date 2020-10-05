using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using flight_planner_core.Models;

namespace flight_planner_core.Services
{
    public interface IAirportService: IEntityService<Airport>
    {
        Task<IEnumerable<Airport>> FindByIncompletePhrase(string phrase);
        Task<IEnumerable<Airport>> GetAsync();
        bool SearchByPhrase(Airport airport, string phrase);
    }
}
