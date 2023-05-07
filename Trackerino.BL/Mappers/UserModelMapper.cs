using Trackerino.BL.Mappers.Interfaces;
using Trackerino.BL.Models;
using Trackerino.DAL.Entities;

namespace Trackerino.BL.Mappers
{
    public class UserModelMapper : ModelMapperBase<UserEntity, UserListModel, UserDetailModel>, IUserModelMapper
    {
        private readonly IUserProjectActivityModelMapper _userProjectActivityModelMapper;
        private readonly IUserProjectModelMapper _userProjectModelMapper;

        public UserModelMapper(IUserProjectActivityModelMapper userProjectActivityModelMapper, IUserProjectModelMapper userProjectModelMapper)
        {
            _userProjectActivityModelMapper = userProjectActivityModelMapper;
            _userProjectModelMapper = userProjectModelMapper;
        }

        public override UserListModel MapToListModel(UserEntity? entity)
            => entity is null
                ? UserListModel.Empty
                : new UserListModel
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    Surname = entity.Surname,
                    ImageUrl = entity.ImageUrl
                };

        public override UserDetailModel MapToDetailModel(UserEntity? entity)
            => entity is null ? UserDetailModel.Empty : new UserDetailModel
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    Surname = entity.Surname,
                    ImageUrl = entity.ImageUrl,
                    Activities = _userProjectActivityModelMapper.MapToListModel(entity.Activities).ToObservableCollection(),
                    Projects = _userProjectModelMapper.MapToListModel(entity.Projects).ToObservableCollection()
                };

        public override UserEntity MapToEntity(UserDetailModel model)
            => new()
            {
                Id = model.Id,
                Name = model.Name,
                Surname = model.Surname,
                ImageUrl = model.ImageUrl,
                Projects = (ICollection<UserProjectEntity>)model.Projects,
                Activities = (ICollection<ActivityEntity>)model.Activities
            };
    }
}