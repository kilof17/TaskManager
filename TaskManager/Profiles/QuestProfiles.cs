using AutoMapper;
using TaskManager.DTOs;
using TaskManager.Models;

namespace TaskManager.Profiles
{
    public class QuestProfiles : Profile
    {
        public QuestProfiles()
        {
            CreateMap<CreateQuest, Quest>();
            CreateMap<Quest, DisplayQuest>();
        }
    }
}