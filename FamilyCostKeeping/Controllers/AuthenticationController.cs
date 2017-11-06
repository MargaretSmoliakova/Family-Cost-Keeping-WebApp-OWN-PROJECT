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
        public ViewResult Index() => View();

        [HttpPost]
        public async Task<IActionResult> Index(AuthenticationRequest authenticationRequest, [FromServices] IUserServices userServices)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (userServices.TryAuthenticate(authenticationRequest) == false)
            {
                TempData["message"] = "Such user does not exist or you have entered wrong Login and(or) Password! Sorry...";
                return View();
            }

            await CreateSessionCookies(authenticationRequest);

            return RedirectToAction("Index", "Home");     
        }


        
        private async Task CreateSessionCookies(AuthenticationRequest authenticationRequest)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, authenticationRequest.LogInName)
            };
            var userIdentity = new ClaimsIdentity(claims, "login");
            ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);
            await HttpContext.SignInAsync(principal);
        }
    }
}