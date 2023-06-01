using System.Collections.ObjectModel;
using Trackerino.BL.Mappers.Interfaces;
using Trackerino.BL.Models;
using Trackerino.DAL.Entities;

namespace Trackerino.BL.Mappers
{
    public class ProjectModelMapper : ModelMapperBase<ProjectEntity, ProjectListModel, ProjectDetailModel>, IProjectModelMapper
    {
        private readonly IProjectUserModelMapper _projectUserModelMapper;
        private readonly IUserProjectActivityModelMapper _userProjectActivityModelMapper;

        public ProjectModelMapper(IProjectUserModelMapper projectUserModelMapper, IUserProjectActivityModelMapper userProjectActivityModelMapper)
        {
            _projectUserModelMapper = projectUserModelMapper;
            _userProjectActivityModelMapper = userProjectActivityModelMapper;
        }

        public override ProjectListModel MapToListModel(ProjectEntity? entity)
            => entity is null
                ? ProjectListModel.Empty
                : new ProjectListModel
                {
                    Id = entity.Id,
                    Name = entity.Name,
                };

        public override ProjectDetailModel MapToDetailModel(ProjectEntity? entity)
        {
            if (entity is null)
                return ProjectDetailModel.Empty;

            var detailModel = new ProjectDetailModel
            {
                Id = entity.Id,
                Name = entity.Name
            };

            var users = entity.Users;
            var activities = entity.Activities;

            if (users.Count > 0)
            {
                detailModel.Users = _projectUserModelMapper.MapToListModel(users).ToObservableCollection();
            }
            else
            {
                detailModel.Users = new ObservableCollection<ProjectUserListModel>();
                // Optionally, you can add a placeholder item or perform other actions when there are no users
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


        public override ProjectEntity MapToEntity(ProjectDetailModel model)
            => new()
            {
                Id = model.Id,
                Name = model.Name,
            };
    }
}
