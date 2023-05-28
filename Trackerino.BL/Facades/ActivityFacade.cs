using Trackerino.BL.Facades.Interfaces;
using Trackerino.BL.Mappers.Interfaces;
using Trackerino.BL.Models;
using Trackerino.DAL.Entities;
using Trackerino.DAL.Mappers;
using Trackerino.DAL.UnitOfWork;

namespace Trackerino.BL.Facades;

public class ActivityFacade : FacadeBase<ActivityEntity, ActivityListModel, ActivityDetailModel, ActivityEntityMapper>,
    IActivityFacade
{
    public ActivityFacade(
        IUnitOfWorkFactory unitOfWorkFactory,
        IActivityModelMapper modelMapper)
        : base(unitOfWorkFactory, modelMapper)
    {

    }
    public async Task<IEnumerable<ActivityListModel>> GetFilteredAsync(DateTime startDate, DateTime endDate)
    {
        IEnumerable<ActivityListModel> activities = await GetAsync(); // Get all activities

        // Filter the activities based on start and end dates
        IEnumerable<ActivityListModel> filteredActivities = activities
            .Where(activity => activity.StartDateTime >= startDate && activity.EndDateTime <= endDate);

        return filteredActivities;
    }
}