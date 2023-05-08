using Microsoft.EntityFrameworkCore;
using Trackerino.DAL.Entities;
namespace Trackerino.DAL
{
    public class TrackerinoDbContext : DbContext
    {
        private readonly bool _seedDemoData;
        public TrackerinoDbContext(DbContextOptions contextOptions, bool seedDemoData = false)
            : base(contextOptions) =>
            _seedDemoData = seedDemoData;

        public DbSet<UserProjectEntity> UserProject=> Set<UserProjectEntity>();
        public DbSet<UserEntity> Users => Set<UserEntity>();
        public DbSet<ProjectEntity> Projects => Set<ProjectEntity>();
        public DbSet<ActivityEntity> Activities => Set<ActivityEntity>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
<<<<<<< Updated upstream

=======
>>>>>>> Stashed changes
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
<<<<<<< Updated upstream
                .WithOne(i => i.User);
=======
                .WithOne(i => i.User)
                .HasForeignKey(i => i.UserId);
            modelBuilder.Entity<ActivityEntity>()
                .HasOne(i => i.Project)
                .WithMany(i => i.Activities)
                .HasForeignKey(i => i.ProjectId);
>>>>>>> Stashed changes
            modelBuilder.Entity<ProjectEntity>()
                .HasMany(i => i.Activities)
                .WithOne(i => i.Project);

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
