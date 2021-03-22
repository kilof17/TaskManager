﻿using Microsoft.EntityFrameworkCore;
using TaskManager.Models;

namespace TaskManager.Data
{
    public class TaskManagerDbContext : DbContext
    {
        public TaskManagerDbContext(DbContextOptions<TaskManagerDbContext> options) : base(options)
        {
        }

        public DbSet<Quest> Quests { get; set; }
        public DbSet<FinishedQuest> FinishedQuests { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Setup> Setups { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Group> Groups { get; set; }
    }
}