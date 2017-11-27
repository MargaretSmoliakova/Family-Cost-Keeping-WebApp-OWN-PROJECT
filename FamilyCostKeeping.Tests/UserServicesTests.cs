using System;
using Xunit;
using FamilyCostKeeping.Repositories;
using FamilyCostKeeping.Models;
using Moq;
using System.Collections.Generic;
using FamilyCostKeeping.Services;
using System.Linq.Expressions;
using FamilyCostKeeping.Models.Internal;
using FamilyCostKeeping.Models.ViewModels;
using System.Linq;
using FamilyCostKeeping.Models.Requests;
using Microsoft.AspNetCore.Http;

namespace FamilyCostKeeping.Tests
{
    public class UserServicesTests
    {
        #region Public
        #region CurrentMonthLeft
        [Fact]
        public void Can_Get_Days_Of_Current_Month_Left_10_WeekendsEscaped()
        {
            //Arrange
            IUserServices userServices = new UserServices
                (GetMockObjectForCurrentMonthLeft(1, 10, true).unitOfWork, GetMockObjectForCurrentMonthLeft(1, 10, true).clock);
            //Act
            var result = userServices.GetDaysOfCurrentMonthLeft(1);
            //Assert
            Assert.True(result == 26);
        }

        [Fact]
        public void Can_Get_Days_Of_Current_Month_Left_30_WeekendsEscaped()
        {
            //Arrange            
            IUserServices userServices = new UserServices
                (GetMockObjectForCurrentMonthLeft(1, 30, true).unitOfWork, GetMockObjectForCurrentMonthLeft(1, 30, true).clock);
            //Act
            var result = userServices.GetDaysOfCurrentMonthLeft(1);
            //Assert
            Assert.True(result == 18);
        }

        [Fact]
        public void Can_Get_Days_Of_Current_Month_Left_31()
        {
            //Arrange            
            IUserServices userServices = new UserServices
                (GetMockObjectForCurrentMonthLeft(1, 31, false).unitOfWork, GetMockObjectForCurrentMonthLeft(1, 31, false).clock);
            //Act
            var result = userServices.GetDaysOfCurrentMonthLeft(1);
            //Assert
            Assert.True(result == 18);
        }

        [Fact]
        public void Can_Get_Days_Of_Current_Month_Left_14()
        {
            //Arrange            
            IUserServices userServices = new UserServices
                (GetMockObjectForCurrentMonthLeft(1, 14, false).unitOfWork, GetMockObjectForCurrentMonthLeft(1, 14, false).clock);
            //Act
            var result = userServices.GetDaysOfCurrentMonthLeft(1);
            //Assert
            Assert.True(result == 2);
        }

        [Fact]
        public void Can_Get_Days_Of_Current_Month_Left_19_WeekendsEscaped()
        {
            //Arrange            
            IUserServices userServices = new UserServices
                (GetMockObjectForCurrentMonthLeft(1, 19, true).unitOfWork, GetMockObjectForCurrentMonthLeft(1, 19, true).clock);
            //Act
            var result = userServices.GetDaysOfCurrentMonthLeft(1);
            //Assert
            Assert.True(result == 5);
        }

        [Fact]
        public void Can_Get_Days_Of_Current_Month_Left_12_WeekendsEscaped()
        {
            //Arrange            
            IUserServices userServices = new UserServices
                (GetMockObjectForCurrentMonthLeft(1, 12, true).unitOfWork, GetMockObjectForCurrentMonthLeft(1, 12, true).clock);

            //Act
            var result = userServices.GetDaysOfCurrentMonthLeft(1);
            //Assert
            Assert.True(result == 30);
        }
        #endregion

        [Fact]
        public void Can_Get_General_User_Info()
        {
            //Arrange
            Mock<IUnitOfWork> unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(m => m.UserRepository.Find(It.IsAny<Expression<Func<User, bool>>>()))
                .Returns(new List<User>
                                {
                                    new User
                                    {
                                         UserId = 1,
                                         PreferredCurrency = Currency.USD,
                                         CurrentBalance = 389.44
                                    }
                                }
                );
            unitOfWorkMock.Setup(m => m.TimePeriodsSettingRepository.Find(It.IsAny<Expression<Func<TimePeriodsSetting, bool>>>()))
                .Returns(new List<TimePeriodsSetting>
                                    {
                                        new TimePeriodsSetting
                                        {
                                            UserId = 1,
                                            MonthStartDay = 15,
                                            IsWeekendsEscapedInMonthlyRefreshing = true
                                        }
                                    }
                );

            Mock<IClock> clockMock = new Mock<IClock>();
            clockMock.Setup(c => c.UtcNow).Returns(new DateTime(2017, 11, 12, 0, 0, 1));

            IUserServices userServices = new UserServices(unitOfWorkMock.Object, clockMock.Object);

            //Action
            var result = userServices.GetGeneralUserInfo(1);

            //Assert
            Assert.Equal(389.44, result.Balance);
            Assert.Equal(Currency.USD, result.PreferredCurrency);
            Assert.Equal(3, result.DaysOfCurrentMonthLeft);
        }

        [Fact]
        public void Can_Get_Settings()
        {
            //Arrange
            Mock<IUnitOfWork> unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(m => m.UserRepository.Find(It.IsAny<Expression<Func<User, bool>>>()))
                .Returns(new List<User>
                                {
                                    new User
                                    {
                                         UserId = 1,
                                         FirstName = "FName",
                                         LastName = "LName",
                                         Mail = "a@a.a",
                                         Password = "jjj",
                                         PreferredCurrency = Currency.USD                                         
                                    }
                                }
                );
            unitOfWorkMock.Setup(m => m.TimePeriodsSettingRepository.Find(It.IsAny<Expression<Func<TimePeriodsSetting, bool>>>()))
                .Returns(new List<TimePeriodsSetting>
                                    {
                                        new TimePeriodsSetting
                                        {
                                            UserId = 1,
                                            MonthStartDay = 1,
                                            IsWeekendsEscapedInMonthlyRefreshing = true
                                        }
                                    }
                );
            unitOfWorkMock.Setup(m => m.CategoryRepository.Find(It.IsAny<Expression<Func<Category, bool>>>()))
                .Returns(new List<Category>
                                            {
                                                new Category
                                                {
                                                    Name = "CName",
                                                    Description = "CDescr.",
                                                    UserId = 1
                                                }
                                            }
                );
            
            IUserServices userServices = new UserServices(unitOfWorkMock.Object);
            
            //Action
            var result = userServices.GetSettings(1);

            //Assert
            Assert.Equal("FName", result.FirstName);
            Assert.Equal("CName", result.Categories.First().Name);
            Assert.Equal(1, result.MonthStartDay);
        }

        #endregion

        #region Private
        private (IUnitOfWork unitOfWork, IClock clock) GetMockObjectForCurrentMonthLeft(int userId, int monthStartDay, bool isWeekendsEscaped)
        {
            Mock<IUnitOfWork> unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(m => m.TimePeriodsSettingRepository.Find(It.IsAny<Expression<Func<TimePeriodsSetting, bool>>>()))
                .Returns(new List<TimePeriodsSetting>
                                    {
                                        new TimePeriodsSetting
                                        {
                                            UserId = userId,
                                            MonthStartDay = monthStartDay,
                                            IsWeekendsEscapedInMonthlyRefreshing = isWeekendsEscaped
                                        }
                                    }
                );

            Mock<IClock> clockMock = new Mock<IClock>();
            clockMock.Setup(c => c.UtcNow).Returns(new DateTime(2017, 11, 12, 0, 0, 1));

            return (unitOfWorkMock.Object, clockMock.Object);
        }
        #endregion
    }
}
