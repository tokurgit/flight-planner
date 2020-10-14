using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using flight_planner_core.Models;

namespace flight_planner_core.Services
{
    public interface IDbService
    {
        IQueryable<T> Query<T>() where T: Entity;
        IQueryable<T> QueryById<T>(int Id) where T: Entity;
        IEnumerable<T> Get<T>() where T : Entity;
        Task<T> GetById<T>(int Id) where T : Entity;
        ServiceResult Create<T>(T entity) where T : Entity;
        ServiceResult Delete<T>(T entity) where T : Entity;
        ServiceResult Update<T>(T entity) where T : Entity;
        bool Exists<T>(int Id) where T : Entity;
    }
}

