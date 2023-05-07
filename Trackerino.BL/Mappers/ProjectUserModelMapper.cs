using Trackerino.BL.Mappers.Interfaces;
using Trackerino.BL.Models;
using Trackerino.DAL.Entities;

namespace Trackerino.BL.Mappers
{
    public class ProjectUserModelMapper : ModelMapperBase<UserProjectEntity, ProjectUserListModel, ProjectUserDetailModel>, IProjectUserModelMapper
    {
        private readonly IUserModelMapper _userModelMapper;
        private readonly IProjectModelMapper _projectModelMapper;

        public ProjectUserModelMapper(IUserModelMapper userModelMapper, IProjectModelMapper projectModelMapper)
        {
            _userModelMapper = userModelMapper;
            _projectModelMapper = projectModelMapper;
        }

        public override ProjectUserListModel MapToListModel(UserProjectEntity? entity)
            => entity?.User is null
                ? ProjectUserListModel.Empty
                : new ProjectUserListModel
                {
                    Id = entity.Id,
                    UserId = entity.UserId,
                    UserName = entity.User.Name,
                    UserSurname = entity.User.Surname,
                    UserImageUrl = entity.User.ImageUrl

                };

        public override ProjectUserDetailModel MapToDetailModel(UserProjectEntity? entity)
            => entity?.User is null
                ? ProjectUserDetailModel.Empty
                : new ProjectUserDetailModel
                {
                    Id = entity.Id,
                    UserId = entity.UserId,
                    UserName = entity.User.Name,
                    UserSurname = entity.User.Surname,
                    UserImageUrl = entity.User.ImageUrl

                };
        public ProjectUserListModel MapToListModel(ProjectUserDetailModel detailModel)
            => new()
            {
                Id = detailModel.Id,
                UserId = detailModel.UserId,
                UserName =detailModel.UserName,
                UserSurname = detailModel.UserSurname,
                UserImageUrl = detailModel.UserImageUrl

            };

        public void MapToExistingDetailModel(ProjectUserDetailModel existingDetailModel,
            UserListModel user)
        {
            existingDetailModel.UserId = user.Id;
            existingDetailModel.UserName = user.Name;
        }

        public override UserProjectEntity MapToEntity(ProjectUserDetailModel model)
            => throw new NotImplementedException("This method is unsupported. Use the other overload.");


        public UserProjectEntity MapToEntity(ProjectUserDetailModel model, Guid projectId)
            => new()
            {
                Id = model.Id,
                ProjectId = projectId,
                UserId = model.UserId
            };

        public UserProjectEntity MapToEntity(ProjectUserListModel model, Guid projectId)
            => new()
            {
                Id = model.Id,
                ProjectId = projectId,
                UserId = model.UserId
            };
    }
}