using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.Interfaces;
using TaskManager.Models;

namespace TaskManager.Repositories
{
    public class MockQuestRopository : IQuestRepository
    {
        private static List<Quest> Quests = new List<Quest>
        {
            new Quest
            {
                Id = 0, Description = "Wash the dishes", Name = "House celaning",InProgress = false, ExpiryTime = DateTime.Now.AddDays(1).ToString("HH:mm:ss"),
                ExpiryDate = DateTime.Now.AddDays(1).ToString("dd/MM/yyyy"), IsItExpiried = false, Points = 5, AddTime = DateTime.Now.ToString("HH:mm:ss"),
                AddDate = DateTime.Now.ToString("dd/MM/yyyy")
            },
             new Quest
            {
                Id = 1, Description = "Buy: tow eggs, one bread and 5 beers", Name = "Shopping",InProgress = false, ExpiryTime = DateTime.Now.AddDays(2).ToString("HH:mm:ss"),
                ExpiryDate = DateTime.Now.AddDays(2).ToString("dd/MM/yyyy"), IsItExpiried = false, Points = 5, AddTime = DateTime.Now.ToString("HH:mm:ss"),
                AddDate = DateTime.Now.ToString("dd/MM/yyyy")
            }
        };

        public void CreateQuest(Quest quest)
        {
            quest.Id = Quests.Count();
            Quests.Add(quest);
        }

        public IEnumerable<Quest> GetAllQuests()
        {
            return Quests;
        }

        public Quest GetQuest(int id)
        {
            return Quests.Where(p => p.Id == id).FirstOrDefault();
        }

        public void MarkQuestAsFinished(Quest quest)
        {
            FinishedQuest finished = new FinishedQuest
            {
                Id = MockFinishedQuestsRepository.FinishedQuests.Count,
                DoneDate = DateTime.Now.ToString("dd/MM/yyyy"),
                DoneTime = DateTime.Now.ToString("HH:mm:ss"),
                //TODO: add identity   Users=
                //Users = new User { Id = 0, Name = "Alex", Email = "xyz@wp.pl", Password="1234", IsActivate = true,
                // Roles= new Role { Id= 0, RoleName= "User" },
                //},
                Name = quest.Name,
                Description = quest.Description,
                AddTime = quest.AddTime,
                AddDate = quest.AddDate,
                Points = quest.Points
            };

            MockFinishedQuestsRepository.FinishedQuests.Add(finished);
        }

        public void RevertQuestInProgressFlag(int id)
        {
            var quest = Quests.Where(p => p.Id == id).FirstOrDefault();
            if (quest != null)
            {
                bool revert = !quest.InProgress;
                quest.InProgress = revert;
            }
        }

        public void RemoveQuest(int id)
        {
            var taskToRemove = Quests.Where(p => p.Id == id).FirstOrDefault();
            if (taskToRemove != null)
            {
                Quests.Remove(taskToRemove);
            }
        }

        public bool SaveChanges()
        {
            return true;
        }

        public void UpdateQuest(int id, Quest quest)
        {
            var index = Quests.FindIndex(p => p.Id == id);
            if (index > -1)
            {
                quest.Id = id;
                Quests[index] = quest;
            }
        }
    }
}