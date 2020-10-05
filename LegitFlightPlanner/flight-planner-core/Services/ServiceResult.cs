using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using flight_planner_core.Interfaces;
using flight_planner_core.Models;

namespace flight_planner_core.Services
{
    public class ServiceResult
    {
        public bool Succeeded { get; }
        public int Id { get; }
        public IEntity Entity { get; private set; }
        public IEnumerable<string> Errors = new List<string>();

        public ServiceResult (bool succeeded)
        {
            Succeeded = succeeded;
        }
        
        public ServiceResult(int id, bool succeeded)
        {
            Id = id;
            Succeeded = succeeded;
        }

        public ServiceResult Set(IEnumerable<string> errors)
        {
            Errors = errors;
            return this;
        }

        public ServiceResult Set (Entity entity)
        {
            Entity = entity;
            return this;
        }
    }
}
