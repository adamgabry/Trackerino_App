using Trackerino.BL.Models;
using Trackerino.DAL.Entities;

namespace Trackerino.BL.Mappers.Interfaces
{
    public interface IUserProjectModelMapper:IModelMapper<UserProjectEntity, UserProjectListModel, ActivityDetailModel>
    {
        UserProjectListModel MapToListModel(UserProjectDetailModel detailModel);
        UserProjectDetailModel MapToDetailModel(UserProjectListModel listModel);
        UserProjectEntity MapToEntity(UserProjectDetailModel model, Guid userId);
        void MapToExistingDetailModel(UserProjectDetailModel existingDetailModel, UserProjectListModel userProject);
        UserProjectEntity MapToEntity(UserProjectListModel model, Guid userId);
    }
}