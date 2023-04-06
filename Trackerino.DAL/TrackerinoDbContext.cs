using Microsoft.EntityFrameworkCore;
using Trackerino.DAL.Entities;
using Trackerino.DAL.Factories;
namespace Trackerino.DAL
{
    public class TrackerinoDbContext : DbContext
    {
        private readonly bool _seedDemoData;
        public TrackerinoDbContext(DbContextOptions contextOptions, bool seedDemoData = false) : base(contextOptions) =>
            _seedDemoData = seedDemoData;

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
        }

    }
}
