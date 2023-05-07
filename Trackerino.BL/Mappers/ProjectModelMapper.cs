using Trackerino.BL.Mappers.Interfaces;
using Trackerino.BL.Models;
using Trackerino.DAL.Entities;

namespace Trackerino.BL.Mappers
{
    public class ProjectModelMapper : ModelMapperBase<ProjectEntity, ProjectListModel, ProjectDetailModel>, IProjectModelMapper
    {
        private readonly IUserProjectModelMapper _userProjectModelMapper;
        private readonly IActivityModelMapper _activityModelMapper;

        public ProjectModelMapper(IUserProjectModelMapper userProjectModelMapper, IActivityModelMapper activityModelMapper)
        {
            _userProjectModelMapper = userProjectModelMapper;
            _activityModelMapper = activityModelMapper;
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
            => entity is null
                ? ProjectDetailModel.Empty
                : new ProjectDetailModel
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    Users = _userProjectModelMapper.MapToListModel(entity.Users).ToObservableCollection(),
                    Activities = _activityModelMapper.MapToListModel(entity.Activities).ToObservableCollection()
                };

        public override ProjectEntity MapToEntity(ProjectDetailModel model)
            => new()
            {
                Id = model.Id,
                Name = model.Name,
                Activities = (ICollection<ActivityEntity>)model.Activities,
                Users = (ICollection<UserProjectEntity>)model.Users
            };
    }
}
