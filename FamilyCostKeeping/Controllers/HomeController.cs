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

namespace FamilyCostKeeping.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index([FromServices] IUserServices userServices)
        {
            GeneralUserInfoViewModel generalUserInfo = new GeneralUserInfoViewModel();
            generalUserInfo.DaysOfCurrentMonthLeft = userServices.GetDaysOfCurrentMonthLeft(1);
            generalUserInfo.Balance = userServices.GetCurrentBalance(1);
            generalUserInfo.PreferredCurrency = userServices.GetPreferredCurrency(1);

            return View(generalUserInfo);
        }        
    }
}
