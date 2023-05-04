using Trackerino.BL.Mappers.Interfaces;
using Trackerino.BL.Models;
using Trackerino.DAL.Entities;

namespace Trackerino.BL.Mappers
{
    public class UserModelMapper : ModelMapperBase<UserEntity, UserListModel, UserDetailModel>, IUserModelMapper
    {
        private readonly IActivityModelMapper _activityModelMapper;
        private readonly IUserProjectModelMapper _userProjectModelMapper;

        public UserModelMapper(IActivityModelMapper activityModelMapper, IUserProjectModelMapper userProjectModelMapper)
        {
            _activityModelMapper = activityModelMapper;
            _userProjectModelMapper = userProjectModelMapper;
        }

        public override UserListModel MapToListModel(UserEntity? entity)
            => entity is null
                ? UserListModel.Empty
                : new UserListModel
                {
                    UserId = entity.Id,
                    Name = entity.Name,
                    Surname = entity.Surname,
                    ImageUrl = entity.ImageUrl
                };

        public override UserDetailModel MapToDetailModel(UserEntity? entity)
            => entity is null
                ? UserDetailModel.Empty
                : new UserDetailModel
                {
                    UserId = entity.Id,
                    Name = entity.Name,
                    Surname = entity.Surname,
                    ImageUrl = entity.ImageUrl,
                    Activities = _activityModelMapper.MapToListModel(entity.Activities).ToObservableCollection(),
                    Projects = _userProjectModelMapper.MapToListModel(entity.Projects).ToObservableCollection()
                };

        public override UserEntity MapToEntity(UserDetailModel model)
            => new()
            {
                Id = model.UserId,
                Name = model.Name,
                Surname = model.Surname,
                ImageUrl = model.ImageUrl,
                Projects = (ICollection<UserProjectEntity>)model.Projects,
                Activities = (ICollection<ActivityEntity>)model.Activities
            };
    }
}