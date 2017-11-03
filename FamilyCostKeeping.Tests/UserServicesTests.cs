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
using System.Linq.Expressions;
using FamilyCostKeeping.Models.Requests;

namespace FamilyCostKeeping.Tests
{
    public class UserServicesTests
    {
        [Fact]
        public void Can_Get_Days_Of_Current_Month_Left()
        {
            //Arrange
            Mock<IUnitOfWork> unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(m => m.UserRepository.Find(It.IsAny<Expression<Func<User, bool>>>()))
                .Returns(new List<User>
                                    {
                                        new User
                                        {
                                            UserId = 1,
                                            TimePeriodsSetting = new TimePeriodsSetting
                                            {
                                                MonthStartDay = 31
                                            }
                                        }
                                    }
                );

            IUserServices userServices = new UserServices(unitOfWorkMock.Object);

            //Act
            var result = userServices.GetDaysOfCurrentMonthLeft();

            //Assert
            Assert.True(result == 31);
        }

    }
}
