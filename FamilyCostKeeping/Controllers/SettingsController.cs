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
            int userId = GetUserIdFromCookies();

            return View(userServices.GetSettings(userId));
        }

        [HttpPost]
        public IActionResult Index(int i)
        {            
            return View();
        }
        #endregion

        #region Private
        private int GetUserIdFromCookies()
        {
            int.TryParse(
                HttpContext.User.Claims
                .FirstOrDefault(x => x.Type == "userId")
                .Value, out int userId);

            return userId;
        }
        #endregion
    }
}