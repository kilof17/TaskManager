using System;
using System.ComponentModel.DataAnnotations;

namespace TaskManager.Models
{
    public class FinishedQuest : QuestBase
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Timestamp]
        public DateTime DoneTimestamp { get; set; }

        public virtual User Users { get; set; }
    }
}