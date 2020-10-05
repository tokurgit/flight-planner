﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LegitFlightPlanner.Models
{
    public class AirportRequest
    {
        public int Id { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Airport { get; set; }
    }
}