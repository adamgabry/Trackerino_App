using Microsoft.EntityFrameworkCore;
using Trackerino.BL.Facades.Interfaces;
using Trackerino.BL.Mappers.Interfaces;
using Trackerino.BL.Models;
using Trackerino.DAL.Entities;
using Trackerino.DAL.Mappers;
using Trackerino.DAL.Repositories;
using Trackerino.DAL.UnitOfWork;

namespace Trackerino.BL.Facades;

public class UserProjectFacade : 
    FacadeBase<UserProjectEntity, UserProjectListModel,
        UserProjectDetailModel, UserProjectEntityMapper>,
    IUserProjectFacade
{
    private readonly IUserProjectModelMapper _userProjectModelMapper;

    public UserProjectFacade(
        IUnitOfWorkFactory unitOfWorkFactory,
        IUserProjectModelMapper userProjectModelMapper)
        : base(unitOfWorkFactory, userProjectModelMapper) =>
        _userProjectModelMapper = userProjectModelMapper;

    public async Task SaveAsync(UserProjectDetailModel model, Guid userId)
    {
        UserProjectEntity entity = _userProjectModelMapper.MapToEntity(model, userId);

        await using IUnitOfWork uow = UnitOfWorkFactory.Create();
        IRepository<UserProjectEntity> repository =
            uow.GetRepository<UserProjectEntity, UserProjectEntityMapper>();

        if (await repository.ExistsAsync(entity))
        {
            await repository.UpdateAsync(entity);
            await uow.CommitAsync();
        }
    }

    public async Task SaveAsync(UserProjectListModel model, Guid userId)
    {
        UserProjectEntity entity = _userProjectModelMapper.MapToEntity(model, userId);

        await using IUnitOfWork uow = UnitOfWorkFactory.Create();
        IRepository<UserProjectEntity> repository =
            uow.GetRepository<UserProjectEntity, UserProjectEntityMapper>();

        await repository.InsertAsync(entity);
        await uow.CommitAsync();
    }

    public async Task<IEnumerable<UserProjectListModel>> GetFilteredByUserAsync(Guid id)
    {
        await using IUnitOfWork uow = UnitOfWorkFactory.Create();
        List<UserProjectEntity> entities = await uow
            .GetRepository<UserProjectEntity, UserProjectEntityMapper>()
            .Get()
            .ToListAsync();

        List<UserProjectEntity> filteredEntities = entities.Where(e => e.UserId == id).ToList();
            return ModelMapper.MapToListModel(filteredEntities);
    }
}