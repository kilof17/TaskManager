using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.Models;

namespace TaskManager.Repositories
{
    public class FinishedQuestsRepository : IFinishedQuestsRepository
    {
        public IEnumerable<FinishedQuest> GetAllFinishedQuests()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<FinishedQuest> GetUserFinishedQuests(string userId)
        {
            throw new NotImplementedException();
        }

        public void RemoveFinishedQuest(int id)
        {
            throw new NotImplementedException();
        }
    }
}