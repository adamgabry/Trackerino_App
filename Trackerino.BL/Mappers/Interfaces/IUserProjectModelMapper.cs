using System.Collections.ObjectModel;
using Trackerino.BL.Models;
using Trackerino.DAL.Entities;

namespace Trackerino.BL.Mappers.Interfaces
{
    public interface IUserProjectModelMapper:IModelMapper<UserProjectEntity, UserProjectListModel, UserProjectDetailModel>
    {
        UserProjectListModel MapToListModel(UserProjectDetailModel detailModel);
        UserProjectEntity MapToEntity(UserProjectDetailModel model, Guid userId);
        void MapToExistingDetailModel(UserProjectDetailModel existingDetailModel, UserProjectListModel userProject);
        UserProjectEntity MapToEntity(UserProjectListModel model, Guid userId);
    }
}