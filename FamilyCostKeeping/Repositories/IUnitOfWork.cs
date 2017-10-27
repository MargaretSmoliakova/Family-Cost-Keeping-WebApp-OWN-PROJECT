using FamilyCostKeeping.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FamilyCostKeeping.Repositories
{
    public interface IUnitOfWork
    {
        IBaseRepository<User> UserRepository { get; }
        IBaseRepository<TimePeriodsSetting> TimePeriodsSettingRepository { get; }
        IBaseRepository<Notification> NotificationRepository { get; }
        IBaseRepository<Earning> EarningRepository { get; }
        IBaseRepository<Cost> CostRepository { get; }
        IBaseRepository<Category> CategoryRepository { get; }

        void Save();
    }
}
