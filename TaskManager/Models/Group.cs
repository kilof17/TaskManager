using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TaskManager.Models
{
    public class Group
    {
        public Group()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Key]
        public string Id { get; private set; }

        [Required]
        public string GroupName { get; set; }

        public virtual ICollection<Quest> Quests { get; set; }
        public virtual ICollection<ApplicationUser> AspNetUsers { get; set; }
    }
}