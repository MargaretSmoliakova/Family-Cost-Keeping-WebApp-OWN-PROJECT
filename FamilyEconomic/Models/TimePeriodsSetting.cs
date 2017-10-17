using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FamilyEconomic.Models
{
    public class TimePeriodsSetting
    {
        public User User { get; set; }
        public int MonthStartDay { get; set; }
        public bool IsWeekendsEscapedInMonthlyRefreshing { get; set; }        
    }
}
