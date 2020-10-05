using System.Data.Entity;
using flight_planner_core.Models;
using flight_planner_data.Migrations;

namespace flight_planner_data
{
    public class FlightPlannerDbContext: DbContext, IFlightPlannerDbContext
    {
        public FlightPlannerDbContext() : base("flight-planner")
        {
            Database.SetInitializer<FlightPlannerDbContext>(null);
            Database.SetInitializer<FlightPlannerDbContext>(new MigrateDatabaseToLatestVersion <FlightPlannerDbContext,Configuration>());

        }

        public DbSet<Flight> Flights { get; set; }
        public DbSet<Airport> Airports { get; set; }
    }
}
