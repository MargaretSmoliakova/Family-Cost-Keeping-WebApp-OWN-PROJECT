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

        [Fact]
        public void Can_Get_Preferred_Currency()
        {
            //Arrange
            Mock<IUnitOfWork> unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(m => m.UserRepository.Find(It.IsAny<Expression<Func<User, bool>>>()))
                .Returns(new List<User>
                                    {
                                        new User
                                        {
                                            UserId = 1,
                                            PreferredCurrency = Currency.USD
                                        }
                                    }
                );

            IUserServices userServicesMock = new UserServices(unitOfWorkMock.Object);

            //Act
            var result = userServicesMock.GetPreferredCurrency();

            //Assert
            Assert.True(result == Currency.USD);
        }

        [Fact]
        public void Can_Get_Current_Balance()
        {
            //Arrange
            Mock<IUnitOfWork> unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(m => m.UserRepository.Find(It.IsAny<Expression<Func<User, bool>>>()))
                .Returns(new List<User>
                                    {
                                        new User
                                        {
                                            UserId = 1,
                                            CurrentBalance = 569.85
                                        }
                                    }
                );

            IUserServices userServicesMock = new UserServices(unitOfWorkMock.Object);

            //Act
            var result = userServicesMock.GetCurrentBalance();

            //Assert
            Assert.Equal(result, 569.85);
        }
    }
}
