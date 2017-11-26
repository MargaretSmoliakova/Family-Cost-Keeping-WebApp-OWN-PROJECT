using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FamilyCostKeeping.Models.Internal
{
    public class RemainingDaysParametersBunch
    {
        private IClock _clock;

        public RemainingDaysParametersBunch([FromServices] IClock clock)
        {
            _clock = clock;
        }

        public int MonthStartDayOriginal { get; set; }
        public DateTime CurrentUtcDateTime => _clock.UtcNow;
        public int ValidMonthStartDayNextMonth { get; set; }
        public int ValidMonthStartDayThisMonth { get; set; }
        public int RemainingDays { get; set; }
        public DateTime MonthLaterFromCurrentUtcDateTime => CurrentUtcDateTime.AddMonths(1);    
    }
}
