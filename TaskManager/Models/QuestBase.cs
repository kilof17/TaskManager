﻿using System;
using System.ComponentModel.DataAnnotations;

namespace TaskManager.Models
{
    public abstract class QuestBase
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public DateTime Added_ISO8601 { get; set; }

        [Required]
        [Range(1, 10)]
        public int Points { get; set; }
    }
}