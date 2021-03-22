using System;
using System.ComponentModel.DataAnnotations;

namespace TaskManager.Models
{
    public abstract class QuestBase
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [Timestamp]
        public DateTime AddTimestamp { get; set; } // task add time

        [Required]
        [Range(1, 10)]
        public int Points { get; set; }
    }
}