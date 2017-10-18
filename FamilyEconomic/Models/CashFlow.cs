using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FamilyCostKeeping.Models
{
    public abstract class CashFlow
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Amount { get; set; }
        public Category Category { get; set; }
        public string Comment { get; set; }        
    }
}
