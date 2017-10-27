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
        private readonly BaseRepository<User> _userRepo;
        private readonly BaseRepository<TimePeriodsSetting> _timePeriodsSettingRepo;
        private readonly BaseRepository<Notification> _notificationRepo;
        private readonly BaseRepository<Earning> _earningRepo;
        private readonly BaseRepository<Cost> _costRepo;
        private readonly BaseRepository<Category> _categoryRepo;

        private FamilyCostKeepingDbContext _dbContext;
        

        public UnitOfWork()
        {
            _userRepo = new BaseRepository<User>(_dbContext.Users);
            _timePeriodsSettingRepo = new BaseRepository<TimePeriodsSetting>(_dbContext.TimePeriodSettings);
            _notificationRepo = new BaseRepository<Notification>(_dbContext.Notifications);
            _earningRepo = new BaseRepository<Earning>(_dbContext.Earnings);
            _costRepo = new BaseRepository<Cost>(_dbContext.Costs);
            _categoryRepo = new BaseRepository<Category>(_dbContext.Categories);
        }

#region IUnitOfWork Implementation

        public IBaseRepository<User> UserRepository => _userRepo;

        public IBaseRepository<TimePeriodsSetting> TimePeriodsSettingRepository => _timePeriodsSettingRepo;

        public IBaseRepository<Notification> NotificationRepository => _notificationRepo;

        public IBaseRepository<Earning> EarningRepository => _earningRepo;

        public IBaseRepository<Cost> CostRepository => _costRepo;

        public IBaseRepository<Category> CategoryRepository => _categoryRepo;

        public void Save() => _dbContext.SaveChanges();        

#endregion
    }
}
