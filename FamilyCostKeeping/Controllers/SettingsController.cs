using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FamilyCostKeeping.Services;

namespace FamilyCostKeeping.Controllers
{
    public class SettingsController : Controller
    {
        #region Actions
        [HttpGet]
        public IActionResult Index([FromServices] IUserServices userServices)
        {
            int userId = userServices.GetUserIdFromCookies(HttpContext);

            return View(userServices.GetSettings(userId));
        }

        [HttpPost]
        public IActionResult Index()
        {            
            return View();
        }
        #endregion
    }
}