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

        public async Task<IEnumerable<FinishedQuest>> GetAllFinishedQuestsAsync()
        {
            //return await _context.FinishedQuests // TODO: finish
            var finishedQuests = _context.FinishedQuests.Select(p => new FinishedQuest
            {
                Id = p.Id,
                //AddDate = p.AddDate,
                //AddTime = p.AddTime,
            });
            return finishedQuests;
        }

        public Task<IEnumerable<FinishedQuest>> GetUserFinishedQuestsAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveFinishedQuestAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}