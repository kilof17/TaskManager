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

        public Task<Quest> GetQuestByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse> MarkQuestAsFinishedAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse> MarkQuestInProgressAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse> RemoveQuestAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse> UnmarkQuestFinishedAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse> UnmarkQuestInProgressAsync(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdateQuest(CreateQuest updateQuest)
        {
            throw new NotImplementedException();
        }
    }
}