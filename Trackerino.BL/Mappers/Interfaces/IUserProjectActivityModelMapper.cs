using Trackerino.BL.Models;
using Trackerino.DAL.Entities;

namespace Trackerino.BL.Mappers
{
    public interface IUserProjectActivityModelMapper:IModelMapper<ActivityEntity, UserProjectActivityListModel, UserProjectActivityDetailModel>
    {
        UserProjectActivityListModel MapToListModel(UserProjectActivityDetailModel detailModel);
        ActivityEntity MapToEntity(UserProjectActivityDetailModel model, Guid userId, Guid projectId);
        void MapToExistingDetailModel(UserProjectActivityDetailModel existingDetailModel, ActivityListModel activity);
        ActivityEntity MapToEntity(UserProjectActivityListModel model, Guid userId, Guid projectId);
    }
}