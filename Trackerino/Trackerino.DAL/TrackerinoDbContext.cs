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
        public DbSet<UserEntity> Users => Set<UserEntity>();
        public DbSet<ProjectEntity> Projects => Set<ProjectEntity>();
        public DbSet<ActivityEntity> Activities => Set<ActivityEntity>();
    }
}
