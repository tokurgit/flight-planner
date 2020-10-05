using System;
using System.Collections.Generic;
using System.Deployment.Internal;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using flight_planner_core.Models;

namespace flight_planner_core.Services
{
    public interface IEntityService<T> where T : Entity
    {
        //NEDRIKST MAINIT-----------------------------!!!!!!!!!!!_-----------------------

        IQueryable<T> Query();
        IQueryable<T> QueryById(int Id);
        IEnumerable<T> Get();
        Task<T> GetById(int Id);
        ServiceResult Create(T entity);
        ServiceResult Delete(T entity);
        ServiceResult Update(T entity);
        bool Exists(int Id);
    }
}
