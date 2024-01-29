using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaskManagmentSystem.DAL.Interfaces;
using TaskManagmentSystem.Domain.Entity;

namespace TaskManagmentSystem.DAL.Repositories
{
    public class UserEntityProfileRepository : IUserEntityProfileRepository
    {
        private readonly ApplicationDBContext _db;

        public UserEntityProfileRepository(ApplicationDBContext db)
        {
            _db = db;
        }

        public async Task<bool> Create(UserEntityProfile entity)
        {

            await _db.UserEntityProfile.AddAsync(entity);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(UserEntityProfile entity)
        {
            _db.UserEntityProfile.Remove(entity);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<List<UserEntityProfile>> Select()
        {
            return await _db.UserEntityProfile.ToListAsync();
        }

        public async Task<UserEntityProfile> Update(UserEntityProfile entity)
        {
            _db.UserEntityProfile.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
