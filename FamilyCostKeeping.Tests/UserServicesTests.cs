using System;
using Xunit;
using FamilyCostKeeping.Repositories;
using FamilyCostKeeping.Models;
using Moq;
using System.Collections.Generic;
using FamilyCostKeeping.Services;
using System.Linq.Expressions;

namespace FamilyCostKeeping.Tests
{
    public class UserServicesTests
    {
        #region Public
        #region CurrentMonthLeft (every time rewrite Asserts as DateTime is used. Last date is 12 Nov 2017)
        [Fact]
        public void Can_Get_Days_Of_Current_Month_Left_10_WeekendsEscaped()
        {
            //Arrange
            IUserServices userServices = new UserServices(GetMockObjectForCurrentMonthLeft(1, 10, true));
            //Act
            var result = userServices.GetDaysOfCurrentMonthLeft(1);
            //Assert
            Assert.True(result == 26);
        }

        [Fact]
        public void Can_Get_Days_Of_Current_Month_Left_30_WeekendsEscaped()
        {
            //Arrange            
            IUserServices userServices = new UserServices(GetMockObjectForCurrentMonthLeft(1, 30, true));
            //Act
            var result = userServices.GetDaysOfCurrentMonthLeft(1);
            //Assert
            Assert.True(result == 18);
        }

        [Fact]
        public void Can_Get_Days_Of_Current_Month_Left_31()
        {
            //Arrange            
            IUserServices userServices = new UserServices(GetMockObjectForCurrentMonthLeft(1, 31, false));
            //Act
            var result = userServices.GetDaysOfCurrentMonthLeft(1);
            //Assert
            Assert.True(result == 18);
        }

        [Fact]
        public void Can_Get_Days_Of_Current_Month_Left_14()
        {
            //Arrange            
            IUserServices userServices = new UserServices(GetMockObjectForCurrentMonthLeft(1, 14, false));
            //Act
            var result = userServices.GetDaysOfCurrentMonthLeft(1);
            //Assert
            Assert.True(result == 2);
        }

        [Fact]
        public void Can_Get_Days_Of_Current_Month_Left_19_WeekendsEscaped()
        {
            //Arrange            
            IUserServices userServices = new UserServices(GetMockObjectForCurrentMonthLeft(1, 19, true));
            //Act
            var result = userServices.GetDaysOfCurrentMonthLeft(1);
            //Assert
            Assert.True(result == 5);
        }

        [Fact]
        public void Can_Get_Days_Of_Current_Month_Left_12_WeekendsEscaped()
        {
            //Arrange            
            IUserServices userServices = new UserServices(GetMockObjectForCurrentMonthLeft(1, 12, true));
            //Act
            var result = userServices.GetDaysOfCurrentMonthLeft(1);
            //Assert
            Assert.True(result == 30);
        }
        #endregion
        #endregion

        #region Private
        private IUnitOfWork GetMockObjectForCurrentMonthLeft(int userId, int monthStartDay, bool isWeekendsEscaped)
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

            return unitOfWorkMock.Object;
        }
        #endregion
    }
}
