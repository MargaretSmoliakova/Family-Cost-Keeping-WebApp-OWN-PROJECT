using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FamilyCostKeeping.Models.Requests;
using FamilyCostKeeping.Services;

namespace FamilyCostKeeping.Controllers
{
    public class SignupController : Controller
    {
        [HttpGet]
        public ViewResult Index() => View();

        [HttpPost]
        public IActionResult Index(SignupRequest signupRequest, [FromServices] IUserServices userServices)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            userServices.CreateUser(signupRequest);

            TempData["message"] = "Signed up successfully! Please authenticate.";

            return View();
        }
    }
}