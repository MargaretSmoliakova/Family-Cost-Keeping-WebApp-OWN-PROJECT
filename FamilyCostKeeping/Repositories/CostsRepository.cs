using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FamilyCostKeeping.Models;
using FamilyCostKeeping.Data;
using Microsoft.EntityFrameworkCore;

namespace FamilyCostKeeping.Repositories
{
    public class CostsRepository : ICostsRepository
    {
        private readonly FamilyCostKeepingDbContext _dbContext;
        private DbSet<Cost> Costs => _dbContext.Costs;

        public void Create(Cost entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Cost entity)
        {
            throw new NotImplementedException();
        }

        public Cost GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Cost entity)
        {
            throw new NotImplementedException();
        }
    }
}
