using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManager.Models;

namespace TaskManager.Repositories
{
    public interface IFinishedQuestsRepository
    {
        Task<IEnumerable<FinishedQuest>> GetAllFinishedQuestsAsync();

        Task<IEnumerable<FinishedQuest>> GetUserFinishedQuestsAsync(string userId);

        Task<bool> RemoveFinishedQuestAsync(int id);
    }
}