using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManager.DTOs;
using TaskManager.Models;

namespace TaskManager.Interfaces
{
    public interface IQuestRepository
    {
        Task<bool> SaveChangesAsync();

        void CreateQuestAsync(Quest quest);

        Task<ApiResponse> RemoveQuestAsync(int id);

        void UpdateQuest(CreateQuest updateQuest);

        Task<IEnumerable<Quest>> GetAllQuestsAsync();

        Task<Quest> GetQuestByIdAsync(int id);

        Task<ApiResponse> UnmarkQuestFinishedAsync(int id);

        Task<ApiResponse> MarkQuestAsFinishedAsync(int id);

        Task<ApiResponse> UnmarkQuestInProgressAsync(int id);

        Task<ApiResponse> MarkQuestInProgressAsync(int id);
    }
}