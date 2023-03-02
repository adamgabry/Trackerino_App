using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Trackerino.DAL.Entities;

namespace Trackerino.DAL
{
    public class TrackerinoDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
        public TrackerinoDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<UserEntity> Users => Set<UserEntity>();
        public DbSet<ProjectEntity> Projects => Set<ProjectEntity>();
        public DbSet<ActivityEntity> Activities => Set<ActivityEntity>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserEntity>()
                .HasMany(i => i.Activities)
                .WithOne(i => i.User)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ProjectEntity>()
                .HasMany(i => i.Users)
                .WithOne(i => i.Project)
                .OnDelete(DeleteBehavior.Cascade);
        }

    }
}
