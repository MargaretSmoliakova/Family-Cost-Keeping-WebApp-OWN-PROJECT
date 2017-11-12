using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FamilyCostKeeping.Models.Requests;
using FamilyCostKeeping.Services;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace FamilyCostKeeping.Controllers
{
    public class AuthenticationController : Controller
    {
        [HttpGet]
        public IActionResult Index() => View();

        [HttpPost]
        public async Task<IActionResult> Index
            (AuthenticationRequest authenticationRequest, [FromServices] IUserServices userServices)
        {
            if (! ModelState.IsValid)
            {
                return View();
            }

            if (! userServices.IsAuthenticated(authenticationRequest))
            {
                TempData["invalidDataMessage"] = "Such user does not exist or you have entered wrong Login and(or) Password! Sorry...";
                return View();
            }

            await userServices.CreateCookies(authenticationRequest, HttpContext);

            return RedirectToAction("Index", "Home");
        }        
    }
}