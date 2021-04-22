using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TaskManager.Models
{
    public class Quest : QuestBase
    {
        [Required]
        public bool InProgress { get; set; }

        public DateTime? Expiry_ISO8601 { get; set; }

        [Required]
        public bool Persistent { get; set; }
    }
}