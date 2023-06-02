using Trackerino.BL.Models;

namespace Trackerino.BL.Facades;

public interface IProjectUserFacade
{
    Task SaveAsync(ProjectUserDetailModel model, Guid projectId);
    Task SaveAsync(ProjectUserListModel model, Guid projectId);
    Task DeleteAsync(Guid id);
}