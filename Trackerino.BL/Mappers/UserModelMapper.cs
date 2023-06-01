using System.Collections.ObjectModel;
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
        {
            if (entity is null)
                return UserDetailModel.Empty;

            var projects = entity.Projects;
            var activities = entity.Activities;

            var detailModel = new UserDetailModel
            {
                Id = entity.Id,
                Name = entity.Name,
                Surname = entity.Surname,
                ImageUrl = entity.ImageUrl
            };

            if (projects.Count > 0)
            {
                detailModel.Projects = _userProjectModelMapper.MapToListModel(projects).ToObservableCollection();
            }
            else
            {
                detailModel.Projects = new ObservableCollection<UserProjectListModel>();
                // Optionally, you can add a placeholder item or perform other actions when there are no projects
            }

            if (activities.Count > 0)
            {
                detailModel.Activities = _userProjectActivityModelMapper.MapToListModel(activities).ToObservableCollection();
            }
            else
            {
                detailModel.Activities = new ObservableCollection<UserProjectActivityListModel>();
                // Optionally, you can add a placeholder item or perform other actions when there are no activities
            }

            return detailModel;
        }


        public override UserEntity MapToEntity(UserDetailModel model)
            => new()
            {
                Id = model.Id,
                Name = model.Name,
                Surname = model.Surname,
                ImageUrl = model.ImageUrl,
            };
    }
}