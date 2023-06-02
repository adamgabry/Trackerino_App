using Trackerino.BL.Mappers;
using Trackerino.BL.Models;
using Trackerino.DAL.Entities;
using Trackerino.DAL.Mappers;
using Trackerino.DAL.Repositories;
using Trackerino.DAL.UnitOfWork;

namespace Trackerino.BL.Facades
{
    public class ProjectUserFacade :
        FacadeBase<UserProjectEntity, ProjectUserListModel,
            ProjectUserDetailModel, UserProjectEntityMapper>,
        IProjectUserFacade
    {
        private readonly IProjectUserModelMapper _projectUserModelMapper;

        public ProjectUserFacade(
            IUnitOfWorkFactory unitOfWorkFactory,
            IProjectUserModelMapper projectUserModelMapper)
            : base(unitOfWorkFactory, projectUserModelMapper) =>
        _projectUserModelMapper = projectUserModelMapper;

        public async Task SaveAsync(ProjectUserDetailModel model, Guid projectId)
        {
            UserProjectEntity entity = _projectUserModelMapper.MapToEntity(model, projectId);

            await using IUnitOfWork uow = UnitOfWorkFactory.Create();
            IRepository<UserProjectEntity> repository =
                uow.GetRepository<UserProjectEntity, UserProjectEntityMapper>();

            if (await repository.ExistsAsync(entity))
            {
                await repository.UpdateAsync(entity);
                await uow.CommitAsync();
            }
        }

        public async Task SaveAsync(ProjectUserListModel model, Guid projectId)
        {
            UserProjectEntity entity = _projectUserModelMapper.MapToEntity(model, projectId);

            await using IUnitOfWork uow = UnitOfWorkFactory.Create();
            IRepository<UserProjectEntity> repository =
                uow.GetRepository<UserProjectEntity, UserProjectEntityMapper>();

            await repository.InsertAsync(entity);
            await uow.CommitAsync();
        }
    }
} 