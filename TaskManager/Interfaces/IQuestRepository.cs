using System.Collections.Generic;
using TaskManager.Models;

namespace TaskManager.Interfaces
{
    public interface IQuestRepository
    {
        bool SaveChanges();

        void CreateQuest(Quest task);

        void RemoveQuest(int id);

        void UpdateQuest(int id, Quest task);

        IEnumerable<Quest> GetAllQuests();

        Quest GetQuest(int id);

        void MarkQuestAsFinished(Quest quest);

        void RevertQuestInProgressFlag(int id);
    }
}