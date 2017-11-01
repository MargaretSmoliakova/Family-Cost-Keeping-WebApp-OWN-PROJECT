using System;
using Xunit;
using FamilyCostKeeping.Repositories;
using FamilyCostKeeping.Models;
using Moq;
using System.Linq;
using System.Collections.Generic;
using FamilyCostKeeping.Data;
using Microsoft.EntityFrameworkCore;
using FamilyCostKeeping.Services;

namespace FamilyCostKeeping.Tests
{
    public class UserServicesTests
    {
        [Fact]
        public void Can_Get_Days_Of_Current_Month_Left()
        {
            //Arrange
            Mock<DbContext> mockContext = new Mock<DbContext>();

            Mock<IUnitOfWork> mock = new Mock<IUnitOfWork>(mockContext);
            mock.Setup(m => m.UserRepository).Returns(new List<User> { });
                new User
                {
                    UserId = 1,
                    TimePeriodsSetting = new TimePeriodsSetting
                        {
                            MonthStartDay = 31,
                            IsWeekendsEscapedInMonthlyRefreshing = true
                        }
                }                
            });
            

            IUserServices userServices = new UserServices(mock.Object);
            
            //Act
            var rr = userServices.GetDaysOfCurrentMonthLeft(1);

            //Assert
            Assert.True(rr == 30);
        }    
    }
}
