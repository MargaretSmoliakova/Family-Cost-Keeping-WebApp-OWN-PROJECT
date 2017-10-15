using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FamilyEconomic.Models
{
    public class CashFlowCost : CashFlow
    {
        public bool IsMonthly { get; set; }
    }
}