using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FamilyCostKeeping.Models;
using FamilyCostKeeping.Models.ViewModels;
using FamilyCostKeeping.Repositories;
using FamilyCostKeeping.Services;
using Microsoft.AspNetCore.Authorization;

namespace FamilyCostKeeping.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {        
        public ViewResult Index([FromServices] IUserServices userServices)
        {
            int userId = GetUserIdFromCookies();

            return View(new GeneralUserInfoViewModel
                            {
                                DaysOfCurrentMonthLeft = userServices.GetDaysOfCurrentMonthLeft(userId),
                                Balance = userServices.GetCurrentBalance(userId),
                                PreferredCurrency = userServices.GetPreferredCurrency(userId)
                            });
        }



        private int GetUserIdFromCookies()
        {
            int userId = 0;

            int.TryParse(
                HttpContext.User.Claims
                .FirstOrDefault(x => x.Type == "id")
                .Value, out userId);

            return userId;
        }
    }
}
