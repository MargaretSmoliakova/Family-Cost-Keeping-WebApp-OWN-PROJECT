using FamilyCostKeeping.Models;
using FamilyCostKeeping.Models.Requests;
using FamilyCostKeeping.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using FamilyCostKeeping.Models.ViewModels;

namespace FamilyCostKeeping.Services
{
    public interface IUserServices
    {
        int GetDaysOfCurrentMonthLeft (int userId);
        double GetCurrentBalance (int userId);
        Currency GetPreferredCurrency (int userId);
        bool IsAuthenticated (AuthenticationRequest authenticationRequest);
        int GetUserIdFromCookies(HttpContext httpContext);
        void CreateUser (SignupRequest signupRequest);
        Task CreateCookies (AuthenticationRequest authenticationRequest, HttpContext httpContext);
        GeneralUserInfoViewModel GetGeneralUserInfo(int userId);
        SettingsViewModel GetSettings(int userId);
    }
}
