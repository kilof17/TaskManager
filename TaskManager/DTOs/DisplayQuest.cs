using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManager.DTOs
{
    public class DisplayQuest
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime Added_ISO8601 { get; set; }

        public int Points { get; set; }

        public bool InProgress { get; set; }

        public DateTime Expiry_ISO8601 { get; set; }

        public bool IsItExpiried { get; set; }
    }
}