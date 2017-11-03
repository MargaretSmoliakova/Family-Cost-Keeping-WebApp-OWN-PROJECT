using FamilyCostKeeping.Models;
using FamilyCostKeeping.Models.Requests;
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
        int GetDaysOfCurrentMonthLeft();
        double GetCurrentBalance();
        Currency GetPreferredCurrency();
        bool TryAuthenticate(AuthenticationRequest authenticationRequest);
    }
}
