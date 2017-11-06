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
    public class HomeController : Controller
    {
        [Authorize]
        public ViewResult Index([FromServices] IUserServices userServices)
        {
            GeneralUserInfoViewModel generalUserInfo = new GeneralUserInfoViewModel();
            generalUserInfo.DaysOfCurrentMonthLeft = userServices.GetDaysOfCurrentMonthLeft();
            generalUserInfo.Balance = userServices.GetCurrentBalance();
            generalUserInfo.PreferredCurrency = userServices.GetPreferredCurrency();

            return View(generalUserInfo);
        }        
    }
}
