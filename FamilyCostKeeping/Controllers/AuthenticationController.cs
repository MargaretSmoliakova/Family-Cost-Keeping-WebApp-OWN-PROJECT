using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FamilyCostKeeping.Models.Requests;
using FamilyCostKeeping.Services;

namespace FamilyCostKeeping.Controllers
{
    public class AuthenticationController : Controller
    {
        [HttpGet]
        public ViewResult Index() => View();

        [HttpPost]
        public IActionResult Index(AuthenticationRequest authenticationRequest, [FromServices] IUserServices userServices)
        {
            if ()

            if (userServices.TryAuthenticate(authenticationRequest) == false)
            {
                TempData["message"] = "Such user does not exist! Sorry...";
                return View();
            }

            return RedirectToAction("Index", "Home");     
        }
    }
}