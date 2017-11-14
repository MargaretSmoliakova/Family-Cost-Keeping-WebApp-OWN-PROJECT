using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FamilyCostKeeping.Models.ViewModels
{
    public class GeneralUserInfoViewModel
    {    
        public DateTime CurrentUtcDateTime { get; set; }
        public int DaysOfCurrentMonthLeft { get; set; }
        public double Balance { get; set; }
        public Currency PreferredCurrency { get; set; }

    }
}
