using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FamilyEconomic.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string SignInName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Mail { get; set; }
        public IEnumerable<CashFlowEarning> Earnings { get; set; }
        public IEnumerable<CashFlowCost> Costs { get; set; }
        public IEnumerable<User> FriendUsers { get; set; }
        public TimePeriods TimePeriods { get; set; }
        public IEnumerable<Notification> Notification { get; set; }
    }
}