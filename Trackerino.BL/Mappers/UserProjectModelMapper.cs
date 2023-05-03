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
            => entity?.User is null
                ? UserProjectListModel.Empty
                : new UserProjectListModel
                {
                    Id = entity.Id,
                    UserId = entity.UserId,
                    ProjectId = entity.ProjectId,
                    User = _userModelMapper.MapToListModel(entity.User),
                    Project = _projectModelMapper.MapToListModel(entity.Project)

                };

        public override UserProjectDetailModel MapToDetailModel(UserProjectEntity? entity)
            => entity?.User is null
                ? UserProjectDetailModel.Empty
                : new UserProjectDetailModel
                {
                    Id = entity.Id,
                    UserId = entity.UserId,
                    ProjectId = entity.ProjectId,
                    User = _userModelMapper.MapToListModel(entity.User),
                    Project = _projectModelMapper.MapToListModel(entity.Project)

                };
        public UserProjectListModel MapToListModel(UserProjectDetailModel detailModel)
            => new()
            {
                Id = detailModel.Id,
                UserId = detailModel.UserId,
                ProjectId = detailModel.ProjectId,
                User = detailModel.User,
                Project = detailModel.Project,

            };

        public void MapToExistingDetailModel(UserProjectDetailModel existingDetailModel,
            UserProjectListModel userProject)
        {
            existingDetailModel.Id = userProject.Id;
            existingDetailModel.UserId = userProject.UserId;
            existingDetailModel.ProjectId = userProject.ProjectId;
        }

        public override UserProjectEntity MapToEntity(UserProjectDetailModel model)
            => throw new NotImplementedException("This method is unsupported. Use the other overload.");


        public UserProjectEntity MapToEntity(UserProjectDetailModel model, Guid userId)
            => new()
            {
                Id = model.Id,
                UserId = model.UserId,
                ProjectId = model.ProjectId
            };

        public UserProjectEntity MapToEntity(UserProjectListModel model, Guid userId)
            => new()
            {
                Id = model.Id,
                UserId = model.UserId,
                ProjectId = model.ProjectId,
            };
    }
}