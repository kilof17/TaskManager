using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.Models;

namespace TaskManager.Repositories
{
    public class MockFinishedQuestsRepository : IFinishedQuestsRepository
    {
        public static List<FinishedQuest> FinishedQuests = new List<FinishedQuest>
        {
            new FinishedQuest
            {
                //Id=0,  AddTime = DateTime.Now.ToString("HH:mm:ss"), AddDate = DateTime.Now.ToString("dd/MM/yyyy"), Description="Eat breakfast", DoneDate = DateTime.Now.ToString("dd/MM/yyyy"),
                //DoneTime = DateTime.Now.ToString("HH:mm:ss"), Name = "Everydaty quest", Points = 10, Users= new ApplicationUser{Id=Guid.NewGuid().ToString() }
            },

             new FinishedQuest
            {
                //Id=1,  AddTime = DateTime.Now.ToString("HH:mm:ss"), AddDate = DateTime.Now.AddDays(-1).ToString("dd/MM/yyyy"), Description="Eat breakfast", DoneDate = DateTime.Now.ToString("dd/MM/yyyy"),
                //DoneTime = DateTime.Now.ToString("HH:mm:ss"), Name = "Everydaty quest", Points = 10, Users= new ApplicationUser{Id=Guid.NewGuid().ToString() }
            }
        };

        public async Task<IEnumerable<FinishedQuest>> GetAllFinishedQuestsAsync()
        {
            return FinishedQuests;
        }

        public async Task<IEnumerable<FinishedQuest>> GetUserFinishedQuestsAsync(string userId)
        {
            return FinishedQuests.FindAll(p => p.Users.Id == userId);
        }

        public Task<bool> RemoveFinishedQuestAsync(int id)
        {
            var index = FinishedQuests.FindIndex(p => p.Id == id);
            if (index > -1)
            {
                FinishedQuests.RemoveAt(index);
                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }
    }
}