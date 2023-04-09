using Trackerino.DAL.Entities;

namespace Trackerino.DAL.Mappers;

public class ActivityEntityMapper : IEntityMapper<ActivityEntity>
{
    public void MapToExistingEntity(ActivityEntity existingEntity, ActivityEntity newEntity)
    {
        existingEntity.StartDateTime = newEntity.StartDateTime;
        existingEntity.EndDateTime = newEntity.EndDateTime;
        existingEntity.Description = newEntity.Description;
        existingEntity.Tag = newEntity.Tag;
    }
}
