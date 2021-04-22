using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManager.Models
{
    public class Setup
    {
        public Setup()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Key]
        [ForeignKey("Users")]
        public string Id { get; private set; }

        public bool ReciveNewTaskAddedEmail { get; set; }

        [Required]
        public virtual ApplicationUser Users { get; set; }
    }
}