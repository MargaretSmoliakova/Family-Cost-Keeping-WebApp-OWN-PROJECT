using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FamilyCostKeeping.Models
{
    public class Notification
    {
        public int NotificationId { get; set; }        
        public DateTime DatesWhenNotify { get; set; }
        public string Comments { get; set; }

        public int CostId { get; set; }
        public Cost Cost { get; set; }
    }
}
