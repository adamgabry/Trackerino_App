using Microsoft.EntityFrameworkCore;
using Trackerino.BL.Mappers;
using Trackerino.BL.Models;
using Trackerino.DAL.Entities;
using Trackerino.DAL.Mappers;
using Trackerino.DAL.UnitOfWork;

namespace Trackerino.BL.Facades;

public class ProjectFacade : FacadeBase<ProjectEntity, ProjectListModel, ProjectDetailModel, ProjectEntityMapper>,
    IProjectFacade
{
    public ProjectFacade(
        IUnitOfWorkFactory unitOfWorkFactory,
        IProjectModelMapper modelMapper)
        : base(unitOfWorkFactory, modelMapper)
    {

    }

    public override async Task<ProjectDetailModel?> GetAsync(Guid id)
    {
        await using IUnitOfWork uow = UnitOfWorkFactory.Create();

        IQueryable<ProjectEntity> query = uow.GetRepository<ProjectEntity, ProjectEntityMapper>().Get();

        if (string.IsNullOrWhiteSpace(IncludesNavigationPathDetail) is false)
        {
            query = query.Include(IncludesNavigationPathDetail);
        }

        ProjectEntity? entity = await query.SingleOrDefaultAsync(e => e.Id == id);

        IQueryable<UserProjectEntity> query2 = uow.GetRepository<UserProjectEntity, UserProjectEntityMapper>().Get();

        if (string.IsNullOrWhiteSpace(IncludesNavigationPathDetail) is false)
        {
            query2 = query2.Include(IncludesNavigationPathDetail);
        }

        IQueryable<ActivityEntity> query3 = uow.GetRepository<ActivityEntity, ActivityEntityMapper>().Get();

        if (string.IsNullOrWhiteSpace(IncludesNavigationPathDetail) is false)
        {
            query2 = query2.Include(IncludesNavigationPathDetail);
        }


        if (entity == null)
        {
            return null;

        }

        await query2.Where(e => e.ProjectId == id).ToListAsync();
        await query3.Where(e => e.ProjectId == id).ToListAsync();
        return ModelMapper.MapToDetailModel(entity);
    }
}