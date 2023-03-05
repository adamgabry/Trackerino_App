using Trackerino.DAL.Entities;

namespace Trackerino.DAL.Tests
{
    public class UserTests
    {
        private readonly TrackerinoDbContext _dbContextSUT;

        public UserTests()
        {
            _dbContextSUT = new Trackerino.DAL.Factories.DefaultFactory().CreateDbContext(new[] { "" });
        }

        [Fact]
        public void NewUser_Add_Added()
        {
            var user = new UserEntity
            {
                Name = "Pepa",
                Surname = "Novak",
                Projects = null,
            };
            _dbContextSUT.Users.Add(user);
            _dbContextSUT.SaveChanges();
        }
    }
}