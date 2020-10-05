using flight_planner_core.Interfaces;

namespace flight_planner_core.Models
{
    public abstract class Entity : IEntity
    {
        public int Id { get; set; }
    }
}
