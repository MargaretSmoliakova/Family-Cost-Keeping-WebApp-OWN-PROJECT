using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FamilyCostKeeping.Repositories
{
    public interface IBaseRepository<T> where T : class
    {
        void Add(T entity);
        IQueryable<T> GetAll();
        IEnumerable<T> Find(Expression<Func<T, bool>> expression);
        void Update(T entity);
        void Delete(T entity);
    }
}
