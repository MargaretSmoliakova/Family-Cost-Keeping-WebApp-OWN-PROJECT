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
            Mock<IUserServices> mock = new Mock<IUserServices>();


            //Act
            var rr = mock.Object.GetDaysOfCurrentMonthLeft(1);

            //Assert
            Assert.NotNull(rr);
        }    
    }
}
