using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FamilyCostKeeping.Models;
using FamilyCostKeeping.Data;
using Microsoft.EntityFrameworkCore;

namespace FamilyCostKeeping.Repositories
{
    public class NotificationsRepository : INotificationsRepository
    {
        private readonly FamilyCostKeepingDbContext _dbContext;
        private DbSet<Notification> Notifications => _dbContext.Notifications;

        public void Create(Notification entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Notification entity)
        {
            throw new NotImplementedException();
        }

        public Notification GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Notification entity)
        {
            throw new NotImplementedException();
        }
    }
}
