using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TaskManager.Models
{
    public abstract class QuestBase
    {
        public QuestBase()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Key]
        public string Id { get; private set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public DateTime Added_ISO8601 { get; set; }

        [Required]
        [Range(1, 10)]
        public int Points { get; set; }

        public virtual ICollection<Group> Groups { get; set; }
    }
}