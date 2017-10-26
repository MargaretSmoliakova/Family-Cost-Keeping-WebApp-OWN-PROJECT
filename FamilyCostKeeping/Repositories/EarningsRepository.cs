using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FamilyCostKeeping.Models;
using Microsoft.EntityFrameworkCore;
using FamilyCostKeeping.Data;

namespace FamilyCostKeeping.Repositories
{
    public class EarningsRepository : IEarningsRepository
    {
        private readonly FamilyCostKeepingDbContext _dbContext;
        private DbSet<Earning> Earnings => _dbContext.Earnings;

        public void Create(Earning entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Earning entity)
        {
            throw new NotImplementedException();
        }

        public Earning GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Earning entity)
        {
            throw new NotImplementedException();
        }
    }
}
