using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.Data;
using TaskManager.Interfaces;
using TaskManager.Models;

namespace TaskManager.Repositories
{
    public class QuestsRepository : IQuestRepository
    {
        private readonly TaskManagerDbContext _context;

        public QuestsRepository(TaskManagerDbContext context)
        {
            _context = context;
        }

        public void CreateQuest(Models.Quest task)
        {
            _context.Quests.Add(task);
        }

        public IEnumerable<Models.Quest> GetAllQuests()
        {
            return _context.Quests.ToList();
        }

        public Models.Quest GetQuest(int id)
        {
            return _context.Quests.Find(id);
        }

        public void MarkQuestAsFinished(Quest quest)
        {
            FinishedQuest finished = new FinishedQuest
            {
                DoneTime = DateTime.Now.ToString("HH:mm:ss"),

                //TODO: add identity   Users=
                Name = quest.Name,
                Description = quest.Description,
                AddTime = quest.AddTime,
                AddDate = quest.AddDate,
                Points = quest.Points
            };

            _context.FinishedQuests.Add(finished);
        }

        public void RevertQuestInProgressFlag(int id)
        {
            var quest = _context.Quests.Find(id);
            bool revert = !quest.InProgress;
            quest.InProgress = revert;
        }

        public void RemoveQuest(int id)
        {
            var taskToRemove = _context.Quests.Where(p => p.Id == id).FirstOrDefault();
            if (taskToRemove != null)
            {
                _context.Quests.Remove(taskToRemove);
            }
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() >= 0;
        }

        public void UpdateQuest(int id, Quest task)
        {
            task.Id = id;
            _context.Entry(task).State = EntityState.Modified;
        }
    }
}