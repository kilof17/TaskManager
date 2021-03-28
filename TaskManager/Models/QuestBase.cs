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

        /// <summary>
        /// The time when quest was added
        /// </summary>
        [Required]
        public string AddTime { get; set; }

        /// <summary>
        /// The date when quest was added
        /// </summary>
        [Required]
        public string AddDate { get; set; }

        /// <summary>
        /// Number of points to get for finish the quest
        /// </summary>
        [Required]
        [Range(1, 10)]
        public int Points { get; set; }
    }
}