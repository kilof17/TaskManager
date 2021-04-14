using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManager.DTOs
{
    public class UserRole
    {
        [Required]
        public string RoleName { get; set; }

        [Required]
        public string UserId { get; set; }
    }
}