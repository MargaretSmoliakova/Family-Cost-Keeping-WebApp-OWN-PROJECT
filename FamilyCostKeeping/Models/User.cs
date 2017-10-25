using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FamilyCostKeeping.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string SignInName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Mail { get; set; }

        public ICollection<Earning> Earnings { get; set; }
        public ICollection<Cost> Costs { get; set; }        

        public TimePeriodsSetting TimePeriodsSetting { get; set; }
    }
}