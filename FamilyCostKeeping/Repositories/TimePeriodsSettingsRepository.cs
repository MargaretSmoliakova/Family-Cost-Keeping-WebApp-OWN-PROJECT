using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FamilyCostKeeping.Models;
using FamilyCostKeeping.Data;
using Microsoft.EntityFrameworkCore;

namespace FamilyCostKeeping.Repositories
{
    public class TimePeriodsSettingsRepository : ITimePeriodsSettingsRepository
    {
        private readonly FamilyCostKeepingDbContext _dbContext;
        private DbSet<TimePeriodsSetting> TimePeriodsSettings => _dbContext.TimePeriodSettings;


        public TimePeriodsSettingsRepository(FamilyCostKeepingDbContext context) => _dbContext = context;

        public void Create(TimePeriodsSetting entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(TimePeriodsSetting entity)
        {
            throw new NotImplementedException();
        }

        public TimePeriodsSetting GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(TimePeriodsSetting entity)
        {
            throw new NotImplementedException();
        }
    }
}
