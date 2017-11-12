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
using FamilyCostKeeping.Models.Internal;

namespace FamilyCostKeeping.Services
{
    public class UserServices : IUserServices
    {
        private readonly IUnitOfWork _unitOfWork;


        #region Public
        public UserServices ([FromServices] IUnitOfWork unitOfWork) => 
            _unitOfWork = unitOfWork;
        
        public double GetCurrentBalance (int userId) => 
            GetUser(userId)
            .CurrentBalance;

        public int GetDaysOfCurrentMonthLeft(int userId)
        {
            RemainingDaysParametersBunch parametersBunch = new RemainingDaysParametersBunch();
            parametersBunch.MonthStartDayOriginal = _unitOfWork.TimePeriodsSettingRepository
                                                   .Find(s => s.UserId == userId)
                                                   .FirstOrDefault()
                                                   .MonthStartDay;

            parametersBunch.ValidMonthStartDayThisMonth = GetValidMonthStartDay
                (parametersBunch.MonthStartDayOriginal, parametersBunch.CurrentUtcDateTime.Month, parametersBunch.CurrentUtcDateTime.Year);
            parametersBunch.ValidMonthStartDayNextMonth = GetValidMonthStartDay
                (parametersBunch.MonthStartDayOriginal, parametersBunch.MonthLaterFromCurrentUtcDateTime.Month, parametersBunch.MonthLaterFromCurrentUtcDateTime.Year);

            CountRemainingDays(parametersBunch);
            
            if (_unitOfWork.TimePeriodsSettingRepository
                .Find(s => s.UserId == userId)
                .FirstOrDefault()
                .IsWeekendsEscapedInMonthlyRefreshing)
            {
                ConsiderWeekends(parametersBunch);
            }

            return parametersBunch.RemainingDays;
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

        private int GetValidMonthStartDay(int monthStartDay, int month, int year)
        {
            int validMonthStartDay = monthStartDay;
            int daysInCurrentMonth = DateTime.DaysInMonth(year, month);

            if (monthStartDay > daysInCurrentMonth)
            {
                validMonthStartDay = daysInCurrentMonth;
            }

            return validMonthStartDay;
        }

        private void CountRemainingDays(RemainingDaysParametersBunch parametersBunch)
        {
            if (parametersBunch.CurrentUtcDateTime.Day >= parametersBunch.ValidMonthStartDayThisMonth)
            {
                parametersBunch.RemainingDays = (DateTime.Parse($"{parametersBunch.MonthLaterFromCurrentUtcDateTime.Year}-{parametersBunch.MonthLaterFromCurrentUtcDateTime.Month}-{parametersBunch.ValidMonthStartDayNextMonth}")
                            - parametersBunch.CurrentUtcDateTime).Days + 1;
            }
            else
            {
                parametersBunch.RemainingDays = (DateTime.Parse($"{parametersBunch.CurrentUtcDateTime.Year}-{parametersBunch.CurrentUtcDateTime.Month}-{parametersBunch.ValidMonthStartDayThisMonth}")
                            - parametersBunch.CurrentUtcDateTime).Days + 1;
            }
        }

        private void ConsiderWeekends(RemainingDaysParametersBunch parametersBunch)
        {
            DayOfWeek dayOfWeekCurrentMonth = new DateTime(parametersBunch.CurrentUtcDateTime.Year, parametersBunch.CurrentUtcDateTime.Month, parametersBunch.ValidMonthStartDayThisMonth)
                                               .DayOfWeek;
            DayOfWeek dayOfWeekNextMonth = new DateTime(parametersBunch.MonthLaterFromCurrentUtcDateTime.Year, parametersBunch.MonthLaterFromCurrentUtcDateTime.Month, parametersBunch.ValidMonthStartDayNextMonth)
                                               .DayOfWeek;

            if (parametersBunch.CurrentUtcDateTime.Day >= parametersBunch.ValidMonthStartDayThisMonth
                && (dayOfWeekNextMonth == DayOfWeek.Sunday || dayOfWeekNextMonth == DayOfWeek.Saturday))
            {
                parametersBunch.RemainingDays = dayOfWeekNextMonth == DayOfWeek.Saturday ? parametersBunch.RemainingDays - 1 : parametersBunch.RemainingDays;
                parametersBunch.RemainingDays = dayOfWeekNextMonth == DayOfWeek.Sunday ? parametersBunch.RemainingDays - 2 : parametersBunch.RemainingDays;
            }
            else if (parametersBunch.CurrentUtcDateTime.Day < parametersBunch.ValidMonthStartDayThisMonth
                && (dayOfWeekCurrentMonth == DayOfWeek.Sunday || dayOfWeekCurrentMonth == DayOfWeek.Saturday))
            {
                parametersBunch.RemainingDays = dayOfWeekCurrentMonth == DayOfWeek.Saturday ? parametersBunch.RemainingDays - 1 : parametersBunch.RemainingDays;
                parametersBunch.RemainingDays = dayOfWeekCurrentMonth == DayOfWeek.Sunday ? parametersBunch.RemainingDays - 2 : parametersBunch.RemainingDays;
            }
        }
        #endregion
    }
}
