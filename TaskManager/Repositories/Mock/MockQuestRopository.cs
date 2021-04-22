using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManager.DTOs;
using TaskManager.Interfaces;
using TaskManager.Models;

namespace TaskManager.Repositories
{
    public class MockQuestRopository : IQuestRepository
    {
        public void CreateQuestAsync(Quest quest)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Quest>> GetAllQuestsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Quest> GetQuestByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse> MarkQuestAsFinishedAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse> MarkQuestInProgressAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse> RemoveQuestAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse> UnmarkQuestFinishedAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse> UnmarkQuestInProgressAsync(string id)
        {
            throw new NotImplementedException();
        }

        public void UpdateQuest(CreateQuest updateQuest)
        {
            throw new NotImplementedException();
        }
    }
}