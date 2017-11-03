using FamilyCostKeeping.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FamilyCostKeeping.Data
{
    public static class DbInitializer
    {
        public static void Initialize(FamilyCostKeepingDbContext context)
        {
            context.Database.EnsureCreated();

            if (!context.Users.Any())
            {
                var category = new Category { Name = "TestCategory", Description = "test category for testing" };
                context.Categories.Add(category);
                context.SaveChanges();

                var users = new User[]
                    {
                        new User {
                            FirstName = "TestFirstName",
                            LastName = "TestLastName",
                            LogInName = "TestLogInName",
                            Password = "TestPassword",
                            Created = DateTime.Now.ToLocalTime(),
                            Mail = "testmail@testmail.com",
                            CurrentBalance = 506.55,
                            PreferredCurrency = Currency.USD,
                            TimePeriodsSetting = new TimePeriodsSetting
                            {
                                UserId = 1,
                                MonthStartDay = 31,
                                IsWeekendsEscapedInMonthlyRefreshing = true
                            },
                            Costs = new List<Cost> { new Cost {
                                UserId = 1,
                                Name = "TestCost",
                                IsMonthly = true,
                                Comment = "for test purpose",
                                Amount = 55.23,
                                CategoryId = 1,
                                CreatedDateTime = DateTime.Now.ToLocalTime(),
                                Notifications = new List<Notification> {
                                    new Notification {
                                        CostId = 1,
                                        DateWhenNotify = 30,
                                        Comments = "test comment for notification"}
                                } }
                            },
                            Earnings = new List<Earning> { new Earning
                            {
                                UserId = 1,
                                Name = "TestEarning",
                                Amount = 99.33,
                                CategoryId = 1,
                                Comment = "test comment for earning",
                                CreatedDateTime = DateTime.Now.ToLocalTime()
                            } }
                        }
                    };
                foreach(User u in users)
                {
                    context.Users.Add(u);
                }
                context.SaveChanges();
            }
        }
    }
}
