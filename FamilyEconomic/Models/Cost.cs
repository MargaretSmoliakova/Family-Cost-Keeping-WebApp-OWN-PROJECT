using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FamilyCostKeeping.Models
{
    public class Cost : CashFlow
    {
        public bool IsMonthly { get; set; }
    }
}