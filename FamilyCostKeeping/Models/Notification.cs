using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FamilyCostKeeping.Models
{
    public class Notification
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int NotificationId { get; set; }        
        public int DateWhenNotify { get; set; }
        public string Comments { get; set; }

        public int CostId { get; set; }
        public virtual Cost Cost { get; set; }
    }
}
