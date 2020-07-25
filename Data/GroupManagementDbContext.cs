using Data.Configurations;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    public class GroupManagementDbContext : DbContext
    {
        public DbSet<GroupEntity> Groups { get; set; }

        public GroupManagementDbContext(DbContextOptions<GroupManagementDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("public");
            modelBuilder.ApplyConfiguration(new GroupEntityConfiguration());
        }

    }
}
