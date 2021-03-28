using System.Collections.Generic;
using TaskManager.Models;

namespace TaskManager.Repositories
{
    public interface IFinishedQuestsRepository
    {
        IEnumerable<FinishedQuest> GetAllFinishedQuests();
    }
}