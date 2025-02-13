﻿using Microsoft.EntityFrameworkCore;
using Trackerino.DAL.DemoSeeds;
using Trackerino.DAL.TestSeeds;
using Trackerino.DAL.Entities;

namespace Trackerino.DAL
{
    public class TrackerinoDbContext : DbContext
    {
        private readonly bool _seedDemoData;
        private readonly bool _seedTestingData;
        public TrackerinoDbContext(DbContextOptions contextOptions, bool seedDemoData, bool seedTestingData)
            : base(contextOptions){
            _seedDemoData = seedDemoData;
            _seedTestingData = seedTestingData;
        }

        public DbSet<UserProjectEntity> UserProject=> Set<UserProjectEntity>();
        public DbSet<UserEntity> Users => Set<UserEntity>();
        public DbSet<ProjectEntity> Projects => Set<ProjectEntity>();
        public DbSet<ActivityEntity> Activities => Set<ActivityEntity>();
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserEntity>()
                .HasMany(i => i.Projects)
                .WithOne(i => i.User)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<ProjectEntity>()
                .HasMany(i => i.Users)
                .WithOne(i => i.Project)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<UserEntity>()
                .HasMany(i => i.Activities)
                .WithOne(i => i.User);
            modelBuilder.Entity<ProjectEntity>()
                .HasMany(i => i.Activities)
                .WithOne(i => i.Project);
            
            if (_seedDemoData)
            {
                UserSeeds.Seed(modelBuilder);
                ProjectSeeds.Seed(modelBuilder);
                ActivitySeeds.Seed(modelBuilder);
                UserProjectSeeds.Seed(modelBuilder);
            }
            if (_seedTestingData)
            {
                
                TestUserSeeds.Seed(modelBuilder);
                TestProjectSeeds.Seed(modelBuilder);
                TestActivitySeeds.Seed(modelBuilder);
                TestUserProjectSeeds.Seed(modelBuilder);
            }
        }
    }
}
