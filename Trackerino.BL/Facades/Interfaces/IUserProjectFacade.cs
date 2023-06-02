using Trackerino.BL.Models;

namespace Trackerino.BL.Facades;

public interface IUserProjectFacade
{
    Task SaveAsync(UserProjectDetailModel model, Guid userId);
    Task SaveAsync(UserProjectListModel model, Guid userId);
    Task DeleteAsync(Guid id);
    Task<IEnumerable<UserProjectListModel>> GetFilteredByUserAsync(Guid id);
}