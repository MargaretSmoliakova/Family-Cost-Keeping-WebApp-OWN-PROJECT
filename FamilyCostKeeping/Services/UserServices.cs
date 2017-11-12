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


        #region Public
        public UserServices ([FromServices] IUnitOfWork unitOfWork) => 
            _unitOfWork = unitOfWork;
        
        public double GetCurrentBalance (int userId) => 
            GetUser(userId)
            .CurrentBalance;

        //TODO rewrite the logic here and consider the case when user initially created
        public int GetDaysOfCurrentMonthLeft(int userId)
        {
            int remainingDays = 0;
            int validMonthStartDay = 0;
            int validMonthStartDayNextMonth = 0;
            DateTime currentUtcDateTime = DateTime.UtcNow;
            DateTime monthLaterFromCurrentUtcDateTime = currentUtcDateTime.AddMonths(1);            
            int monthStartDay = _unitOfWork.TimePeriodsSettingRepository
                                .Find(s => s.UserId == userId)
                                .FirstOrDefault()
                                .MonthStartDay;

            validMonthStartDay = GetValidMonthStartDate(monthStartDay, currentUtcDateTime.Month, currentUtcDateTime.Year);
            validMonthStartDayNextMonth = GetValidMonthStartDate(monthStartDay, monthLaterFromCurrentUtcDateTime.Month, monthLaterFromCurrentUtcDateTime.Year);

            if (currentUtcDateTime.Day >= validMonthStartDay)
            {
                remainingDays = (DateTime.Parse($"{monthLaterFromCurrentUtcDateTime.Year}-{monthLaterFromCurrentUtcDateTime.Month}-{validMonthStartDayNextMonth}")
                            - currentUtcDateTime).Days + 1;
            }
            else
            {
                remainingDays = (DateTime.Parse($"{currentUtcDateTime.Year}-{currentUtcDateTime.Month}-{validMonthStartDay}")
                            - currentUtcDateTime).Days + 1;
            }
            

            if (_unitOfWork.TimePeriodsSettingRepository
                .Find(s => s.UserId == userId)
                .FirstOrDefault()
                .IsWeekendsEscapedInMonthlyRefreshing)
            {                
                DayOfWeek dayOfWeekCurrentMonth = new DateTime(currentUtcDateTime.Year, currentUtcDateTime.Month, validMonthStartDay).DayOfWeek;
                DayOfWeek dayOfWeekNextMonth = new DateTime(monthLaterFromCurrentUtcDateTime.Year, monthLaterFromCurrentUtcDateTime.Month, validMonthStartDayNextMonth).DayOfWeek;

                if (currentUtcDateTime.Day >= validMonthStartDay
                    && (dayOfWeekNextMonth == DayOfWeek.Sunday || dayOfWeekNextMonth == DayOfWeek.Saturday))
                {
                    remainingDays = dayOfWeekNextMonth == DayOfWeek.Saturday ? remainingDays - 1 : remainingDays;
                    remainingDays = dayOfWeekNextMonth == DayOfWeek.Sunday ? remainingDays - 2 : remainingDays;
                }
                else if (currentUtcDateTime.Day < validMonthStartDay
                    && (dayOfWeekCurrentMonth == DayOfWeek.Sunday || dayOfWeekCurrentMonth == DayOfWeek.Saturday))
                {
                    remainingDays = dayOfWeekCurrentMonth == DayOfWeek.Saturday ? remainingDays - 1 : remainingDays;
                    remainingDays = dayOfWeekCurrentMonth == DayOfWeek.Sunday ? remainingDays - 2 : remainingDays;
                }
            }

            return remainingDays;
        }

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
            User newUser = new User
            {
                FirstName = signupRequest.FirstName,
                LastName = signupRequest.LastName,
                Mail = signupRequest.Mail,
                LogInName = signupRequest.LogInName,
                Password = signupRequest.Password,
                CreatedDateTime = DateTime.Now.ToUniversalTime()
            };

            _unitOfWork.UserRepository
                .Add(newUser);

            _unitOfWork.TimePeriodsSettingRepository
                .Add(new TimePeriodsSetting
                {
                    User = newUser,
                    MonthStartDay = DateTime.UtcNow.Day,
                    IsWeekendsEscapedInMonthlyRefreshing = false
                });

            _unitOfWork.Save();
        }

        public async Task CreateCookies 
            (AuthenticationRequest authenticationRequest, HttpContext httpContext)
        {
            int userId = _unitOfWork.UserRepository
                            .Find(u => u.LogInName.Equals(authenticationRequest.LogInName)
                                       && u.Password.Equals(authenticationRequest.Password))
                            .FirstOrDefault()
                            .UserId;

            List<Claim> claims = new List<Claim>
                            {
                                new Claim("userId", userId.ToString())
                            };

            ClaimsPrincipal principal = new ClaimsPrincipal(new ClaimsIdentity(claims, "login"));

            if (authenticationRequest.RememberCredentials)
                await httpContext.SignInAsync
                (principal, new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTimeOffset.Now.AddDays(90)
                });
            else
                await httpContext.SignInAsync(principal);

        }
        #endregion

        #region Private
        private User GetUser (int userId) =>
            _unitOfWork.UserRepository
            .Find(u => u.UserId == userId)
            .FirstOrDefault();

        private int GetValidMonthStartDate(int monthStartDay, int month, int year)
        {
            int validMonthStartDay = monthStartDay;
            int daysInCurrentMonth = DateTime.DaysInMonth(year, month);

            if (monthStartDay > daysInCurrentMonth)
            {
                validMonthStartDay = daysInCurrentMonth;
            }

            return validMonthStartDay;
        }
        #endregion
    }
}
