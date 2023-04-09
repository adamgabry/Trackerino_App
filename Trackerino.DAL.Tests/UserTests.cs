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
        public Guid UserGuid = Guid.NewGuid();
        [Fact]
        public void NewUser_Add_Added()
        {
            var user = new UserEntity
            {
                Id = UserGuid,
                Name = "Pepa",
                Surname = "Novak",
                Projects = null,
            };
            _dbContextSUT.Users.Add(user);
            _dbContextSUT.SaveChanges();
            Assert.Equal(_dbContextSUT.Users.Find(UserGuid),user);
        }
    }
}