using System;
using System.ComponentModel.DataAnnotations;

namespace TaskManager.Models
{
    public class FinishedQuest : QuestBase
    {
        [Required]
        public DateTime Done_ISO8601 { get; set; }

        public virtual ApplicationUser Users { get; set; }
    }
}