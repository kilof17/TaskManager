using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManager.Models
{
    public class Setup
    {
        [Key]
        [ForeignKey("Users")]
        public string Id { get; set; }

        public bool ReciveNewTaskAddedEmail { get; set; }

        [Required]
        public virtual ApplicationUser Users { get; set; }
    }
}