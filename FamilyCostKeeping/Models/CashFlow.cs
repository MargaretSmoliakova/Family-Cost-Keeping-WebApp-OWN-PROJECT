using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FamilyCostKeeping.Models
{
    public abstract class CashFlow
    {
        public string Name { get; set; }
        public double Amount { get; set; }
        public string Comment { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
