using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FamilyCostKeeping.Models
{
    public class TimePeriodsSetting
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TimePeriodsSettingId { get; set; }        
        public int MonthStartDay { get; set; }
        public bool IsWeekendsEscapedInMonthlyRefreshing { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}
