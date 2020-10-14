using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Threading.Tasks;
using flight_planner_core.Models;

namespace flight_planner_data
{
    public interface IFlightPlannerDbContext
    {
        DbSet<T> Set<T>() where T : class;
        DbEntityEntry<T> Entry<T>(T entity) where T : class;
        int SaveChanges();
        Task<int> SaveChangesAsync();
        DbSet<Flight> Flights { get; set; }
        DbSet<Airport> Airports { get; set; }
    }
}
