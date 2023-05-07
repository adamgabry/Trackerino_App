using Trackerino.BL.Facades.Interfaces;
using Trackerino.BL.Mappers.Interfaces;
using Trackerino.BL.Models;
using Trackerino.DAL.Entities;
using Trackerino.DAL.Mappers;
using Trackerino.DAL.Repositories;
using Trackerino.DAL.UnitOfWork;

namespace Trackerino.BL.Facades;

public class UserProjectActivityFacade :
    FacadeBase<ActivityEntity, UserProjectActivityListModel,
        UserProjectActivityDetailModel, ActivityEntityMapper>,
    IUserProjectActivityFacade
{
    private readonly IUserProjectActivityModelMapper _userProjectActivityModelMapper;

    public UserProjectActivityFacade(
        IUnitOfWorkFactory unitOfWorkFactory,
        IUserProjectActivityModelMapper userProjectActivityModelMapper)
        : base(unitOfWorkFactory, userProjectActivityModelMapper) =>
        _userProjectActivityModelMapper = userProjectActivityModelMapper;

    public async Task SaveAsync(UserProjectActivityDetailModel model, Guid userId, Guid projectId)
    {
        ActivityEntity entity = _userProjectActivityModelMapper.MapToEntity(model, userId, projectId);

        await using IUnitOfWork uow = UnitOfWorkFactory.Create();
        IRepository<ActivityEntity> repository =
            uow.GetRepository<ActivityEntity, ActivityEntityMapper>();

        if (await repository.ExistsAsync(entity))
        {
            await repository.UpdateAsync(entity);
            await uow.CommitAsync();
        }
    }

    public async Task SaveAsync(UserProjectActivityListModel model, Guid userId, Guid projectId)
    {
        ActivityEntity entity = _userProjectActivityModelMapper.MapToEntity(model, userId, projectId);

        await using IUnitOfWork uow = UnitOfWorkFactory.Create();
        IRepository<ActivityEntity> repository =
            uow.GetRepository<ActivityEntity, ActivityEntityMapper>();

        await repository.InsertAsync(entity);
        await uow.CommitAsync();
    }
}