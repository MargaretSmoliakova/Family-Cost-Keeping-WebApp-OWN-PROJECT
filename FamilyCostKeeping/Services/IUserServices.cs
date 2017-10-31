using FamilyCostKeeping.Models;
using FamilyCostKeeping.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FamilyCostKeeping.Services
{
    public interface IUserServices
    {
        int GetDaysOfCurrentMonthLeft(int userId);
        double GetCurrentBalance(int userId);
        Currency GetPreferredCurrency(int userId);
    }
}
