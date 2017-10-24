using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FamilyCostKeeping.Data
{
    public static class DbInitializer
    {
        public static void Initialize(FamilyCostKeepingDbContext context)
        {
            context.Database.EnsureCreated();
        }
    }
}
