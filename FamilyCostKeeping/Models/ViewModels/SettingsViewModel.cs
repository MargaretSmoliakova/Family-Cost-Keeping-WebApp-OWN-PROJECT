using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FamilyCostKeeping.Models.ViewModels
{
    public class SettingsViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string Mail { get; set; }
        public Currency PreferredCurrency { get; set; }
        public int MonthStartDay { get; set; }
        public bool IsWeekendsEscapedInMonthlyRefreshing { get; set; }
        public virtual ICollection<Category> Categories { get; set; }
    }
}
