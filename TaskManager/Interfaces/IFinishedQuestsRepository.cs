using System.Collections.Generic;
using TaskManager.Models;

namespace TaskManager.Repositories
{
    public interface IFinishedQuestsRepository
    {
        IEnumerable<FinishedQuest> GetAllFinishedQuests();

        IEnumerable<FinishedQuest> GetUserFinishedQuests(string userId);

        void RemoveFinishedQuest(int id);
    }
}