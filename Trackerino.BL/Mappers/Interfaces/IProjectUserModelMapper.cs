using Trackerino.BL.Models;
using Trackerino.DAL.Entities;

namespace Trackerino.BL.Mappers
{
    public interface IProjectUserModelMapper:IModelMapper<UserProjectEntity, ProjectUserListModel, ProjectUserDetailModel>
    {
        ProjectUserListModel MapToListModel(ProjectUserDetailModel detailModel);
        UserProjectEntity MapToEntity(ProjectUserDetailModel model, Guid projectId);
        void MapToExistingDetailModel(ProjectUserDetailModel existingDetailModel, UserListModel user);
        UserProjectEntity MapToEntity(ProjectUserListModel model, Guid projectId);
    }
}