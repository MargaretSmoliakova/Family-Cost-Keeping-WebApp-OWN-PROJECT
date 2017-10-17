using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FamilyEconomic.Models;

namespace FamilyEconomic.Data
{
    public class FamilyEconomicDbContext : DbContext
    {
        public FamilyEconomicDbContext(DbContextOptions<FamilyEconomicDbContext> options) : base(options)
        { }

        public DbSet<User> Users { get; set; }
        public DbSet<TimePeriodsSetting> TimePeriodSettings { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CashFlowCost> Costs { get; set; }
        public DbSet<CashFlowEarning> Earnings { get; set; }
    }
}
