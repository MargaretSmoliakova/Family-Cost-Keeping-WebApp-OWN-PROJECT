using FamilyCostKeeping.Models;
using FamilyCostKeeping.Models.Requests;
using FamilyCostKeeping.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace FamilyCostKeeping.Services
{
    public interface IUserServices
    {
        int GetDaysOfCurrentMonthLeft();
        double GetCurrentBalance();
        Currency GetPreferredCurrency();
        bool IsAuthenticated(AuthenticationRequest authenticationRequest);
        void CreateUser(SignupRequest signupRequest);
        Task CreateCookies(AuthenticationRequest authenticationRequest, HttpContext httpContext);
    }
}
