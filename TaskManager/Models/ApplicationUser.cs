using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManager.Models
{
    public class ApplicationUser : IdentityUser
    {
        [StringLength(20)]
        public string FirstName { get; set; }

        [StringLength(30)]
        public string LastName { get; set; }

        public int ScoredPoints { get; set; }

        public virtual ICollection<FinishedQuest> FinishedQuests { get; set; }
        public virtual ICollection<Group> Groups { get; set; }
        public virtual Setup Setups { get; set; }
    }
}