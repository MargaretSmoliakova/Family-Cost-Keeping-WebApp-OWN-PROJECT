using FamilyCostKeeping.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FamilyCostKeeping.Models;

namespace FamilyCostKeeping.Services
{
    // TODO think how to get current principal information to replace 1 number and do I actually need this linq
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
            var v = _unitOfWork.UserRepository
                .Find(s => s.UserId == 1)
                .FirstOrDefault();
            var v1 = v.TimePeriodsSetting;
            var v2 = v1.MonthStartDay;

            return v2;             
        }

        public Currency GetPreferredCurrency()
        {
            return _unitOfWork.UserRepository
                .Find(u => u.UserId == 1)
                .FirstOrDefault()
                .PreferredCurrency;
        }



        private int GetCurrentUserId()
        {
            int userId = 0;

            return userId;
        }
    }
}
