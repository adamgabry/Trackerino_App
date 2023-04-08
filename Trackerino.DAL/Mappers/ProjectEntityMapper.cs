using Trackerino.DAL.Entities;

namespace Trackerino.DAL.Mappers;

public class ProjectEntityMapper : IEntityMapper<ProjectEntity>
{
    public void MapToExistingEntity(ProjectEntity existingEntity, ProjectEntity newEntity) 
    {
        existingEntity.Name = newEntity.Name;
    }
}
