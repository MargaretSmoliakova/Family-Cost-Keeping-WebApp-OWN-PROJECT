using FamilyCostKeeping.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FamilyCostKeeping.Repositories.Extentions
{
    public static class UserRepositoryExtension
    {
        public static void GetMethodForExample(this IBaseRepository<User> repository, int integer, string st)
        {
            // Do query or something here and return the result

            // The result is the following code:
            // unitOfWork.UserRepository.GetMethodForExample(1, "foo");

        }
    }
}
