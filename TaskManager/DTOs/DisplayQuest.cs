using System;
using System.ComponentModel.DataAnnotations;

namespace TaskManager.DTOs
{
    public class DisplayQuest
    {
        [Required]
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public DateTime Added_ISO8601 { get; set; }

        [Required]
        public int Points { get; set; }

        [Required]
        public bool InProgress { get; set; }

        public DateTime Expiry_ISO8601 { get; set; }

        [Required]
        public bool Persistent { get; set; }

        [Required]
        public bool IsItExpiried { get; set; }
    }
}