﻿using HomeworkCrud.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace HomeworkCrud.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Group> Groups { get; set; }
    }
}
