using System.ComponentModel.DataAnnotations;

namespace TaskManager.DTOs
{
    public class UserLogin
    {
        [Required]
        [StringLength(15, MinimumLength = 5)]
        public string Login { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 8)]
        public string Password { get; set; }
    }
}