namespace FlightPlanner.Models
{
    public class AirportNoId
    {
        public string Country { get; set; }
        public string City { get; set; }
        public string airport { get; set; }

        public static AirportNoId CreateFromAirportFromFlight(Flight flight)
        {
            return new AirportNoId()
            {
                Country = flight.From.Country,
                City = flight.From.City,
                airport = flight.From.airport
            };
        }

        public static AirportNoId CreateToAirportFromFlight(Flight flight)
        {
            return new AirportNoId()
            {
                Country = flight.To.Country,
                City = flight.To.City,
                airport = flight.To.airport
            };
        }
    }
}
