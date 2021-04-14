using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using TaskManager.Models;

namespace TaskManager.DTOs
{
    public class DisplayUser
    {
        public string Id { get; set; }
        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public virtual ICollection<FinishedQuest> FinishedQuests { get; set; }
        public virtual ICollection<Group> Groups { get; set; }
        public virtual ICollection<IdentityRole> Roles { get; set; }
    }
}