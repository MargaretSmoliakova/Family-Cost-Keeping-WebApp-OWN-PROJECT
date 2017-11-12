using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FamilyCostKeeping.Models.Internal
{
    public class RemainingDaysParametersBunch
    {
        public int MonthStartDayOriginal { get; set; }
        public DateTime CurrentUtcDateTime { get; } = DateTime.UtcNow;
        public int ValidMonthStartDayNextMonth { get; set; }
        public int ValidMonthStartDayThisMonth { get; set; }
        public int RemainingDays { get; set; }
        public DateTime MonthLaterFromCurrentUtcDateTime
        {
            get
            {
                return CurrentUtcDateTime.AddMonths(1); ;
            }
        }        
    }
}
