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

        public double GetCurrentBalance(int userId)
        {
            return _unitOfWork.UserRepository
                .Find(u => u.UserId == userId)
                .FirstOrDefault()
                .CurrentBalance;
        }

        public int GetDaysOfCurrentMonthLeft(int userId)
        {
            var v = _unitOfWork.UserRepository
                .Find(s => s.UserId == userId)
                .FirstOrDefault();
            var v1 = v.TimePeriodsSetting;
            var v2 = v1.MonthStartDay;

            return v2;             
        }

        public Currency GetPreferredCurrency(int userId)
        {
            return _unitOfWork.UserRepository
                .Find(u => u.UserId == userId)
                .FirstOrDefault()
                .PreferredCurrency;
        }
    }
}
