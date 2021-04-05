using System;
using System.ComponentModel.DataAnnotations;

namespace TaskManager.Models
{
    public class FinishedQuest : QuestBase
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string DoneTime { get; set; }

        [Required]
        public string DoneDate { get; set; }

        public virtual ApplicationUser Users { get; set; }
    }
}