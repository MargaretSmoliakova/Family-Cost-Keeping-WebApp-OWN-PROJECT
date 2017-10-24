using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FamilyCostKeeping.Models;

namespace FamilyCostKeeping.Data
{
    public class FamilyCostKeepingDbContext : DbContext
    {
        public FamilyCostKeepingDbContext(DbContextOptions<FamilyCostKeepingDbContext> options) : base(options)
        { }

        public DbSet<User> Users { get; set; }
        public DbSet<TimePeriodsSetting> TimePeriodSettings { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Cost> Costs { get; set; }
        public DbSet<Earning> Earnings { get; set; }        
    }
}
