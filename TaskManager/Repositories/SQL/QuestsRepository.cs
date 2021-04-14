using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.Data;
using TaskManager.DTOs;
using TaskManager.Interfaces;
using TaskManager.Models;

namespace TaskManager.Repositories
{
    public class QuestsRepository : IQuestRepository
    {
        private readonly TaskManagerDbContext _context;
        private readonly IMapper _mapper;

        public QuestsRepository(TaskManagerDbContext context,
                                IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        #region Create quest

        public async void CreateQuestAsync(Quest task)
        {
            task.Added_ISO8601 = DateTime.Now;
            task.InProgress = false;
            task.IsItExpiried = false;

            await _context.Quests.AddAsync(task);
        }

        #endregion Create quest

        #region Get all quests

        public async Task<IEnumerable<Quest>> GetAllQuestsAsync()
        {
            return await _context.Quests.IgnoreAutoIncludes().ToListAsync();
        }

        #endregion Get all quests

        #region Get quest by id

        public async Task<Quest> GetQuestByIdAsync(int id)
        {
            return await _context.Quests.FindAsync(id);
        }

        #endregion Get quest by id

        #region Remove quest

        public async Task<ApiResponse> RemoveQuestAsync(int id)
        {
            var taskToRemove = await _context.Quests.FindAsync(id);
            if (taskToRemove != null)
            {
                _context.Quests.Remove(taskToRemove);
                return new ApiResponse
                {
                    IsSuccess = true,
                };
            }
            return new ApiResponse
            {
                IsSuccess = false,
                Message = "Task not removed"
            };
        }

        #endregion Remove quest

        #region Update quest

        public void UpdateQuest(CreateQuest task)
        {
            _context.Entry(task).State = EntityState.Modified;
        }

        #endregion Update quest

        #region Unmark quest in progress

        public async Task<ApiResponse> UnmarkQuestInProgressAsync(int id)
        {
            var result = await _context.Quests.FindAsync(id);

            if (result.InProgress == true)
                result.InProgress = false;

            return new ApiResponse
            {
                Message = "Task already marked as is not progress",
                IsSuccess = true
            };
        }

        #endregion Unmark quest in progress

        #region Mark as quest in progress

        public async Task<ApiResponse> MarkQuestInProgressAsync(int id)
        {
            var result = await _context.Quests.FindAsync(id);

            if (result.InProgress == false)
                result.InProgress = true;

            return new ApiResponse
            {
                Message = "Task already marked as in progress",
                IsSuccess = true
            };
        }

        #endregion Mark as quest in progress

        #region Unmark quest finished

        public async Task<ApiResponse> UnmarkQuestFinishedAsync(int id)
        {
            var finishedQuest = await _context.FinishedQuests.FindAsync(id);
            if (finishedQuest == null)
                return new ApiResponse
                {
                    Message = "Finished Task not found",
                    IsSuccess = false
                };

            var quest = _mapper.Map(finishedQuest, new Quest());

            await _context.Quests.AddAsync(quest);

            return new ApiResponse
            {
                Message = "Task marked as finished",
                IsSuccess = true
            };
        }

        #endregion Unmark quest finished

        #region Mark quest as finished

        public async Task<ApiResponse> MarkQuestAsFinishedAsync(int id)
        {
            var quest = await _context.Quests.FindAsync(id);
            if (quest == null)
                return new ApiResponse
                {
                    Message = "Task not found",
                    IsSuccess = false
                };

            var finishedQuest = _mapper.Map(quest, new FinishedQuest());
            finishedQuest.Done_ISO8601 = DateTime.Now;

            await _context.FinishedQuests.AddAsync(finishedQuest);

            return new ApiResponse
            {
                Message = "Task marked as finished",
                IsSuccess = true
            };
        }

        #endregion Mark quest as finished

        #region Save changes in DB

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() >= 0;
        }

        #endregion Save changes in DB
    }
}