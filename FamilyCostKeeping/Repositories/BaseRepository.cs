using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace FamilyCostKeeping.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly DbSet<T> _dbSet;



        public BaseRepository(DbSet<T> dbSet) => _dbSet = dbSet;
        
#region IBaseRepository<T> Implementation

        public void Add(T entity) => _dbSet.Add(entity);

        public void Delete(T entity) => _dbSet.Remove(entity);

        public IQueryable<T> GetAll() => _dbSet;

        public IEnumerable<T> Find(Expression<Func<T, bool>> expression) => _dbSet.Where(expression);
        
        public void Update(T entity) => _dbSet.Update(entity);        
    }

#endregion
}
