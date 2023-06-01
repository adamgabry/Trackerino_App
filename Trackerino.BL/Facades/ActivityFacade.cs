using Microsoft.EntityFrameworkCore;
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
            .Where(activity => activity.StartDateTime <= endDate && activity.EndDateTime >= startDate);

        return filteredActivities;
    }

    public async Task<IEnumerable<ActivityListModel>> GetFilteredByUserAsync(Guid id)
    {
        await using IUnitOfWork uow = UnitOfWorkFactory.Create();
        List<ActivityEntity> entities = await uow
            .GetRepository<ActivityEntity, ActivityEntityMapper>()
            .Get()
            .ToListAsync();
        
        return ModelMapper.MapToListModel(entities.Where(e => e.UserId == id));
    }
    public override async Task<ActivityDetailModel?> GetAsync(Guid id)
    {
        await using IUnitOfWork uow = UnitOfWorkFactory.Create();

        IQueryable<ActivityEntity> query = uow.GetRepository<ActivityEntity, ActivityEntityMapper>().Get();

        if (string.IsNullOrWhiteSpace(IncludesNavigationPathDetail) is false)
        {
            query = query.Include(IncludesNavigationPathDetail);
        }

        ActivityEntity? entity = await query.SingleOrDefaultAsync(e => e.Id == id);

        IQueryable<UserEntity> query2 = uow.GetRepository<UserEntity, UserEntityMapper>().Get();

        if (string.IsNullOrWhiteSpace(IncludesNavigationPathDetail) is false)
        {
            query2 = query2.Include(IncludesNavigationPathDetail);
        }

        IQueryable<ProjectEntity> query3 = uow.GetRepository<ProjectEntity, ProjectEntityMapper>().Get();

        if (string.IsNullOrWhiteSpace(IncludesNavigationPathDetail) is false)
        {
            query2 = query2.Include(IncludesNavigationPathDetail);
        }


        if (entity == null)
        {
            return null;

        }

        entity.User = await query2.SingleOrDefaultAsync(e => e.Id == entity.UserId);
        entity.Project = await query3.SingleOrDefaultAsync(e => e.Id == entity.ProjectId);
        return ModelMapper.MapToDetailModel(entity);
    }
}