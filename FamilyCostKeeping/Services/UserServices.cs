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



        public UserServices([FromServices] IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;
        

        public double GetCurrentBalance() => 
            _unitOfWork.UserRepository
            .Find(u => u.UserId == GetCurrentUserId())
            .FirstOrDefault()
            .CurrentBalance;
        //TODO rewrite the logic here
        public int GetDaysOfCurrentMonthLeft() =>
            _unitOfWork.TimePeriodsSettingRepository
            .Find(s => s.UserId == GetCurrentUserId())
            .FirstOrDefault()
            .MonthStartDay;

        public Currency GetPreferredCurrency() =>
            _unitOfWork.UserRepository
            .Find(u => u.UserId == GetCurrentUserId())
            .FirstOrDefault()
            .PreferredCurrency;
            


        private int GetCurrentUserId()
        {
            int userId = 0;

            userId = 1;

            return userId;
        }
    }
}
