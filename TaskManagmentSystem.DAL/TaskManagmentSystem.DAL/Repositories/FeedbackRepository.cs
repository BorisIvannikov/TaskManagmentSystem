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
    public class FeedbackRepository : IFeedbackRepository
    {
        private readonly ApplicationDBContext _db;

        public FeedbackRepository(ApplicationDBContext db)
        {
            _db = db;
        }

        public async Task<bool> Create(Feedback entity)
        {
            await _db.Feedbacks.AddAsync(entity);
            await _db.SaveChangesAsync();
            return true;            
        }


        public Task<bool> Delete(Feedback entity)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Feedback>> Select()
        {
            return await _db.Feedbacks.ToListAsync();
        }

        public Task<Feedback> Update(Feedback entity)
        {
            throw new NotImplementedException();
        }
    }
}
