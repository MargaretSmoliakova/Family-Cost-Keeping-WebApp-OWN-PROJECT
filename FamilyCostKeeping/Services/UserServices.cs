using FamilyCostKeeping.Models;
using FamilyCostKeeping.Models.Requests;
using FamilyCostKeeping.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FamilyCostKeeping.Services
{
    // TODO think how to get current principal information to replace 1 number and do I actually need this linq
    public class UserServices : IUserServices
    {
        private readonly IUnitOfWork _unitOfWork;



        public UserServices ([FromServices] IUnitOfWork unitOfWork) => 
            _unitOfWork = unitOfWork;
        
        public double GetCurrentBalance (int userId) => 
            GetUser(userId)
            .CurrentBalance;

        //TODO rewrite the logic here
        public int GetDaysOfCurrentMonthLeft (int userId) =>
            _unitOfWork.TimePeriodsSettingRepository
            .Find(s => s.UserId == userId)
            .FirstOrDefault()
            .MonthStartDay;

        public Currency GetPreferredCurrency (int userId) =>
            GetUser(userId)
            .PreferredCurrency;

        public bool IsAuthenticated (AuthenticationRequest authenticationRequest) =>
            _unitOfWork.UserRepository
            .Find(u => u.LogInName.Equals(authenticationRequest.LogInName) 
                       && u.Password.Equals(authenticationRequest.Password))
            .Any();

        public void CreateUser (SignupRequest signupRequest)
        {
            _unitOfWork.UserRepository
            .Add(new User
            {
                FirstName = signupRequest.FirstName,
                LastName = signupRequest.LastName,
                Mail = signupRequest.Mail,
                LogInName = signupRequest.LogInName,
                Password = signupRequest.Password,
                CreatedDateTime = DateTime.Now.ToUniversalTime()
            });

            _unitOfWork.Save();
        }

        public async Task CreateCookies (AuthenticationRequest authenticationRequest, HttpContext httpContext)
        {
            int userId = _unitOfWork.UserRepository
                        .Find(u => u.LogInName.Equals(authenticationRequest.LogInName)
                                   && u.Password.Equals(authenticationRequest.Password))
                        .FirstOrDefault()
                        .UserId;

            var claims = new List<Claim>
            {
                new Claim("id", userId.ToString())
            };
            var userIdentity = new ClaimsIdentity(claims, "login");
            ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);
            await httpContext.SignInAsync(principal);
        }



        private User GetUser (int userId) =>
            _unitOfWork.UserRepository
            .Find(u => u.UserId == userId)
            .FirstOrDefault();
    }
}
