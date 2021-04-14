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
        public bool InProgress { get; set; }

        public DateTime Expiry_ISO8601 { get; set; }

        [Required]
        public bool IsItExpiried { get; set; } // TODO: Possiblity restore task -> setup new expiry date and time

        public virtual ICollection<Group> Groups { get; set; }
    }
}