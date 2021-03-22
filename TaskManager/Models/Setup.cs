using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManager.Models
{
    public class Setup
    {
        [Key]
        [ForeignKey("Users")]
        public int Id { get; set; }

        public bool ReciveNewTaskAddedEmail { get; set; }

        [Required]
        public virtual User Users { get; set; }
    }
}