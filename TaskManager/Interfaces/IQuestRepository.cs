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

        Task<ApiResponse> RemoveQuestAsync(string id);

        void UpdateQuest(CreateQuest updateQuest);

        Task<IEnumerable<Quest>> GetAllQuestsAsync();

        Task<Quest> GetQuestByIdAsync(string id);

        Task<ApiResponse> UnmarkQuestFinishedAsync(string id);

        Task<ApiResponse> MarkQuestAsFinishedAsync(string id);

        Task<ApiResponse> UnmarkQuestInProgressAsync(string id);

        Task<ApiResponse> MarkQuestInProgressAsync(string id);
    }
}