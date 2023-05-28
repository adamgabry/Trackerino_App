using Trackerino.BL.Models;
using Trackerino.DAL.Entities;

namespace Trackerino.BL.Facades.Interfaces;

public interface IActivityFacade : IFacade<ActivityEntity, ActivityListModel, ActivityDetailModel>
{
    Task<IEnumerable<ActivityListModel>> GetFilteredAsync(DateTime startDate, DateTime endDate);
}
