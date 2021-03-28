using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManager.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(50)]
        [Required(AllowEmptyStrings = false)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MaxLength(255)]
        public string Password { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(50)]
        public string Surname { get; set; }

        [Required]
        public bool IsActivate { get; set; }

        [Required]
        public System.Guid UniqueUserCode { get; set; }

        public virtual ICollection<FinishedQuest> FinishedQuests { get; set; }
        public virtual ICollection<Group> Groups { get; set; }

        public virtual Role Roles { get; set; }
        public virtual Setup Setups { get; set; }
    }
}