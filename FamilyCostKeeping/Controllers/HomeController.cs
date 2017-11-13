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
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace FamilyCostKeeping.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        #region Actions
        public IActionResult Index([FromServices] IUserServices userServices)
        {
            int userId = GetUserIdFromCookies();           

            return View(userServices.GetGeneralUserInfo(userId));
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index", "Authentication");
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
