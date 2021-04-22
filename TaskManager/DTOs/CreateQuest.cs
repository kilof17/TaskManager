using Swashbuckle.AspNetCore.Annotations;
using System;
using System.ComponentModel.DataAnnotations;

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

        public DateTime? Expiry_ISO8601 { get; set; }

        public bool Persistent { get => !Expiry_ISO8601.HasValue; private set => Persistent = true; }
    }
}