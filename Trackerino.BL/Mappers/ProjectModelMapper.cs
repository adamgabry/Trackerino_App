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
            => entity is null
                ? ProjectDetailModel.Empty
                : new ProjectDetailModel
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    Users = _projectUserModelMapper.MapToListModel(entity.Users).ToObservableCollection(),
                    Activities = _userProjectActivityModelMapper.MapToListModel(entity.Activities).ToObservableCollection(),
                };

        public override ProjectEntity MapToEntity(ProjectDetailModel model)
            => new()
            {
                Id = model.Id,
                Name = model.Name,
            };
    }
}
