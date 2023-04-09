using Trackerino.DAL.Entities;

namespace Trackerino.DAL.Mappers;

public class UserProjectEntityMapper : IEntityMapper<UserProjectEntity>
{
    public void MapToExistingEntity(UserProjectEntity existingEntity, UserProjectEntity newEntity)
    {
        existingEntity.UserId = newEntity.UserId;
        existingEntity.ProjectId = newEntity.ProjectId;
    }
}
