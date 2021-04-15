using AutoMapper;
using TaskManager.Models;

namespace TaskManager.Profiles
{
    public class FinishedQuestProfiles : Profile
    {
        public FinishedQuestProfiles()
        {
            CreateMap<Quest, FinishedQuest>();
            CreateMap<FinishedQuest, Quest>();
        }
    }
}