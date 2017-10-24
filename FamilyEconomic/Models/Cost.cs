using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FamilyCostKeeping.Models
{
    public class Cost : CashFlow
    {
        public int CostId { get; set; }
        public bool IsMonthly { get; set; }

        public ICollection<Notification> Notifications { get; set; }
    }
}