using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TaskManager.Models
{
    public class Quest : QuestBase
    {
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// The flag to mark quest as task in progress
        /// </summary>
        [Required]
        public bool InProgress { get; set; } // TODO: task is in the progress, can be canceled

        public string ExpiryTime { get; set; }

        public string ExpiryDate { get; set; }

        /// <summary>
        /// Flag to mark task like out of date
        /// </summary>
        [Required]
        public bool IsItExpiried { get; set; } // TODO: Possiblity restore task -> setup new expiry date and time

        public virtual ICollection<Group> Groups { get; set; }
    }
}