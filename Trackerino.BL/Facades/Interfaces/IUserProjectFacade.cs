using Trackerino.BL.Models;
using Trackerino.DAL.Entities;

namespace Trackerino.BL.Facades.Interfaces;

public interface IUserProjectFacade : IFacade<UserProjectEntity, UserProjectListModel, UserProjectDetailModel>
{
    Task SaveAsync(UserProjectDetailModel model, Guid userId);
    Task SaveAsync(UserProjectListModel model, Guid userId);
    Task DeleteAsync(Guid id);
}
