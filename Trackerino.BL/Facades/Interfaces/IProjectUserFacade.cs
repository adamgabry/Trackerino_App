using Trackerino.BL.Models;
using Trackerino.DAL.Entities;

namespace Trackerino.BL.Facades.Interfaces;

public interface IProjectUserFacade
{
    Task SaveAsync(ProjectUserDetailModel model, Guid projectId);
    Task SaveAsync(ProjectUserListModel model, Guid projectId);
    Task DeleteAsync(Guid id);
}