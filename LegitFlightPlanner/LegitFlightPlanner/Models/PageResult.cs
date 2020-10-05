using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using System.Web;
using flight_planner_core.Models;
using flight_planner_core.Services;

namespace LegitFlightPlanner.Models
{
    public class PageResult
    {
        public int Page { get; set; } = 0;
        public int TotalItems { get; set; } = 0;
        public Flight[] Items { get; set; } = new Flight[0];

        public async Task<PageResult> FindBySearchRequest(SearchFlightRequest request, IFlightService flightservice)
        {
            var flightList = await flightservice.GetAsync();
            var allFlights = flightList
                .Where(x => x.From.airport == request.From)
                .Where(x => x.To.airport == request.To)
                .Where(x =>
                    (DateTime.Parse(x.DepartureTime).Date).CompareTo(DateTime.Parse(request.DepartureDate)
                        .Date) == 0)
                .Distinct()
                .ToArray();

            //GetTotalItemCount(allFlights);

            return new PageResult() {Items = allFlights, TotalItems = allFlights.Length};
        }

        //private void GetTotalItemCount(IEnumerable<Flight> allFlights)
        //{
        //    TotalItems = allFlights.Count();
        //}
    }
}
