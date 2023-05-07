using Microsoft.EntityFrameworkCore;
using Trackerino.DAL.Entities;

namespace Trackerino.DAL
{
    public class TrackerinoDbContext : DbContext
    {
        private readonly bool _seedDemoData;
        public TrackerinoDbContext(DbContextOptions contextOptions, bool seedDemoData)
            : base(contextOptions) =>
            _seedDemoData = seedDemoData;

        public DbSet<UserProjectEntity> UserProject=> Set<UserProjectEntity>();
        public DbSet<UserEntity> Users => Set<UserEntity>();
        public DbSet<ProjectEntity> Projects => Set<ProjectEntity>();
        public DbSet<ActivityEntity> Activities => Set<ActivityEntity>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ActivityEntity>()
                .HasOne(i => i.Project)
                .WithMany(i => i.Activities)
                .HasForeignKey(i => i.ProjectId);
            modelBuilder.Entity<UserEntity>()
                .HasMany(i => i.Projects)
                .WithOne(i => i.User)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<ProjectEntity>()
                .HasMany(i => i.Users)
                .WithOne(i => i.Project)
                .HasForeignKey(i => i.ProjectId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<UserEntity>()
                .HasMany(i => i.Activities)
                .WithOne(i => i.User)
                .HasForeignKey(i => i.UserId);
            modelBuilder.Entity<ProjectEntity>()
                .HasMany(i => i.Activities)
                .WithOne(i => i.Project);

            // if (_seedTestingData)
            // {
            //     ProjectSeeds.Seed(modelBuilder);
            //     ActivitySeeds.Seed(modelBuilder);
            //     UserSeeds.Seed(modelBuilder);
            //     UserProjectSeeds.Seed(modelBuilder);
            // }
            // if (_seedDemoData)
            // {
            //     ActitySeeds.Seed(modelBuilder);
            //     UserSeeds.Seed(modelBuilder);
            //     UserProjectSeeds.Seed(modelBuilder);
            //     ProjectSeeds.Seed(modelBuilder);
            // }
        }
    }
}
