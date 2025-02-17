﻿using Microsoft.EntityFrameworkCore;
using StudentManagementMVC.Models.Entities;

namespace StudentManagementMVC.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Student> Students { get; set; }
    }
}
