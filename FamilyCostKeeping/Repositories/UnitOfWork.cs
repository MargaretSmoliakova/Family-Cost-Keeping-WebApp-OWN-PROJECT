using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FamilyCostKeeping.Models;
using FamilyCostKeeping.Data;

namespace FamilyCostKeeping.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private BaseRepository<User> _userRepo;
        private BaseRepository<TimePeriodsSetting> _timePeriodsSettingRepo;
        private BaseRepository<Notification> _notificationRepo;
        private BaseRepository<Earning> _earningRepo;
        private BaseRepository<Cost> _costRepo;
        private BaseRepository<Category> _categoryRepo;

        private FamilyCostKeepingDbContext _dbContext;


        public UnitOfWork(FamilyCostKeepingDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
#region IUnitOfWork Implementation

        public IBaseRepository<User> UserRepository => _userRepo = new BaseRepository<User>(_dbContext.Users);

        public IBaseRepository<TimePeriodsSetting> TimePeriodsSettingRepository => _timePeriodsSettingRepo = new BaseRepository<TimePeriodsSetting>(_dbContext.TimePeriodsSettings);

        public IBaseRepository<Notification> NotificationRepository => _notificationRepo = new BaseRepository<Notification>(_dbContext.Notifications);

        public IBaseRepository<Earning> EarningRepository => _earningRepo = new BaseRepository<Earning>(_dbContext.Earnings);

        public IBaseRepository<Cost> CostRepository => _costRepo = new BaseRepository<Cost>(_dbContext.Costs);

        public IBaseRepository<Category> CategoryRepository => _categoryRepo = new BaseRepository<Category>(_dbContext.Categories);
        
        public void Save() => _dbContext.SaveChanges();        

#endregion
    }
}
