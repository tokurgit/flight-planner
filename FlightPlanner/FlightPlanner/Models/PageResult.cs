namespace FlightPlanner.Models
{
    public class PageResult <Flight>
    {
        public int Page { get; set; } = 0;
        public Flight[] Items = new Flight[0];
        public int TotalItems { get; set; } = 0;

    }
}