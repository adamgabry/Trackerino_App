using Trackerino.BL.Mappers.Interfaces;
using Trackerino.BL.Models;
using Trackerino.DAL.Entities;

namespace Trackerino.BL.Mappers
{
    public class UserProjectModelMapper : ModelMapperBase<UserProjectEntity, UserProjectListModel, UserProjectDetailModel>, IUserProjectModelMapper
    {
        private readonly IUserModelMapper _userModelMapper;
        private readonly IProjectModelMapper _projectModelMapper;

        public UserProjectModelMapper(IUserModelMapper userModelMapper, IProjectModelMapper projectModelMapper)
        {
            _userModelMapper = userModelMapper;
            _projectModelMapper = projectModelMapper;
        }

        public override UserProjectListModel MapToListModel(UserProjectEntity? entity)
            => entity?.Project is null
                ? UserProjectListModel.Empty
                : new UserProjectListModel
                {
                    Id = entity.Id,
                    ProjectId = entity.ProjectId,
                    ProjectName = entity.Project.Name

                };

        public override UserProjectDetailModel MapToDetailModel(UserProjectEntity? entity)
            => entity?.Project is null
                ? UserProjectDetailModel.Empty
                : new UserProjectDetailModel
                {
                    Id= entity.Id,
                    ProjectId = entity.ProjectId,
                    ProjectName = entity.Project.Name

                };
        public UserProjectListModel MapToListModel(UserProjectDetailModel detailModel)
            => new()
            {
                Id = detailModel.Id,
                ProjectName = detailModel.ProjectName,
                ProjectId = detailModel.ProjectId,

            };

        public void MapToExistingDetailModel(UserProjectDetailModel existingDetailModel,
            ProjectListModel project)
        {
            existingDetailModel.ProjectId = project.Id;
            existingDetailModel.ProjectName = project.Name;
        }

        public override UserProjectEntity MapToEntity(UserProjectDetailModel model)
            => throw new NotImplementedException("This method is unsupported. Use the other overload.");


        public UserProjectEntity MapToEntity(UserProjectDetailModel model, Guid userId)
            => new()
            {
                Id = model.Id,
                UserId = userId,
                ProjectId = model.ProjectId
            };

        public UserProjectEntity MapToEntity(UserProjectListModel model, Guid userId)
            => new()
            {
                Id = model.Id,
                UserId = userId,
                ProjectId = model.ProjectId,
            };
    }
}