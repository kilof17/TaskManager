using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TaskManager.Models
{
    public class Quest : QuestBase
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public bool InProgress { get; set; } // TODO: task is in the progress, can be canceled

        [Timestamp]
        public DateTime ExpiryTime { get; set; }  // nullable, can never expiry

        [Required]
        public bool IsItExpiried { get; set; }

        public virtual ICollection<Group> Groups { get; set; }
    }
}