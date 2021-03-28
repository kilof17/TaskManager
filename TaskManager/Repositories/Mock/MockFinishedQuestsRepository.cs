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
                Id=0,  AddTime = DateTime.Now.ToString("HH:mm:ss"), AddDate = DateTime.Now.ToString("dd/MM/yyyy"), Description="Eat breakfast", DoneDate = DateTime.Now.ToString("dd/MM/yyyy"),
                DoneTime = DateTime.Now.ToString("HH:mm:ss"), Name = "Everydaty quest", Points = 10
            },

             new FinishedQuest
            {
                Id=1,  AddTime = DateTime.Now.ToString("HH:mm:ss"), AddDate = DateTime.Now.AddDays(-1).ToString("dd/MM/yyyy"), Description="Eat breakfast", DoneDate = DateTime.Now.ToString("dd/MM/yyyy"),
                DoneTime = DateTime.Now.ToString("HH:mm:ss"), Name = "Everydaty quest", Points = 10
            }
        };

        public IEnumerable<FinishedQuest> GetAllFinishedQuests()
        {
            return FinishedQuests;
        }
    }
}