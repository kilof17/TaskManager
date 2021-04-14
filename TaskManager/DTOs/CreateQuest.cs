using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManager.DTOs
{
    public class CreateQuest
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public int Points { get; set; }

        public DateTime Expiry_ISO8601 { get; set; }
    }
}