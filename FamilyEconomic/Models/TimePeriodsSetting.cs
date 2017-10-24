using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FamilyCostKeeping.Models
{
    public class TimePeriodsSetting
    {
        public int TimePeriodsSettingId { get; set; }        
        public int MonthStartDay { get; set; }
        public bool IsWeekendsEscapedInMonthlyRefreshing { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}
