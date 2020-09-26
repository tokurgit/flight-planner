namespace FlightPlanner.Models
{
    public class FlightNoId
    {
        public AirportNoId From { get; set; }
        public AirportNoId To { get; set; }
        public string Carrier { get; set; }
        public string DepartureTime { get; set; }
        public string ArrivalTime { get; set; }
        public int Id { get; set; }

        public static FlightNoId CreateFromFlightAndAirportNoId(Flight flight, AirportNoId fromAirport, AirportNoId toAirport)
        {
            return new FlightNoId()
            {
                From = fromAirport,
                To = toAirport,
                Carrier = flight.Carrier,
                DepartureTime = flight.DepartureTime,
                ArrivalTime = flight.ArrivalTime,
                Id = flight.Id
            };
        }

        public static FlightNoId CreateFromFlight(Flight flight)
        {
            var fromAirport = AirportNoId.CreateFromAirportFromFlight(flight);
            var toAirport = AirportNoId.CreateToAirportFromFlight(flight);

            return new FlightNoId()
            {
                From = fromAirport,
                To = toAirport,
                Carrier = flight.Carrier,
                DepartureTime = flight.DepartureTime,
                ArrivalTime = flight.ArrivalTime,
                Id = flight.Id
            };
        }
    }
}