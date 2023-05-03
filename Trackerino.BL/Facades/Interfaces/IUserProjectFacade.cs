using Trackerino.BL.Models;
using Trackerino.DAL.Entities;

namespace Trackerino.BL.Facades.Interfaces;

public interface IUserProjectFacade
{
    Task SaveAsync(UserProjectDetailModel model, Guid userId);
    Task SaveAsync(UserProjectListModel model, Guid userId);
    Task DeleteAsync(Guid id);
}