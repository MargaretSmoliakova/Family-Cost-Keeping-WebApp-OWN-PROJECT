using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FamilyCostKeeping.Models;
using FamilyCostKeeping.Data;
using Microsoft.EntityFrameworkCore;

namespace FamilyCostKeeping.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly FamilyCostKeepingDbContext _dbContext;

        private DbSet<User> Users
        {
            get
            {
                return _dbContext.Users;
            }
        }

    public UserRepository(FamilyCostKeepingDbContext context)
        {
            _dbContext = context;
        }

        public void Create(User entity)
        {
            Users?.Add(entity);
            Save();
        }

        public void Delete(User entity)
        {
            _dbContext.Users.Remove(entity);
            Save();
        }

        public User GetById(int id)
        {
            return _dbContext.Users.Select(u => u).Where(u => u.UserId == id).FirstOrDefault();
        }        

        public void Update(User entity)
        {
            _dbContext.en.Users.Select(u => u).Where(u => u.UserId == entity.UserId).
        }

        private void Save()
        {
            _dbContext.SaveChanges();
        }
    }
}
