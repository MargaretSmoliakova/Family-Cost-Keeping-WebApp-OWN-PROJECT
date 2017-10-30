using FamilyCostKeeping.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FamilyCostKeeping.Models;

namespace FamilyCostKeeping.Services
{
    public class UserServices : IUserServices
    {
        private readonly IUnitOfWork _unitOfWork;


        public UserServices([FromServices] IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public double GetCurrentBalance()
        {
            return _unitOfWork.UserRepository
                .Find(u => u.UserId == 1)
                .FirstOrDefault()
                .CurrentBalance;
        }

        public int GetDaysOfCurrentMonthLeft()
        {
            return _unitOfWork.TimePeriodsSettingRepository
                .Find(t => t.UserId == 1)
                .FirstOrDefault()
                .MonthStartDay;
        }

        public Currency GetPreferredCurrency()
        {
            return _unitOfWork.UserRepository
                .Find(u => u.UserId == 1)
                .FirstOrDefault()
                .PreferredCurrency;
        }
    }
}
