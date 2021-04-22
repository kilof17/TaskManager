using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.Data;
using TaskManager.Models;

namespace TaskManager.Repositories
{
    public class FinishedQuestsRepository : IFinishedQuestsRepository
    {
        private TaskManagerDbContext _context;

        public FinishedQuestsRepository(TaskManagerDbContext context)
        {
            _context = context;
        }

        public Task<IEnumerable<FinishedQuest>> GetAllFinishedQuestsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<FinishedQuest>> GetUserFinishedQuestsAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveFinishedQuestAsync(string id)
        {
            throw new NotImplementedException();
        }
    }
}