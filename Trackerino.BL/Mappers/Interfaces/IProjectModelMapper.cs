using Trackerino.BL.Models;
using Trackerino.DAL.Entities;

namespace Trackerino.BL.Mappers.Interfaces
{
    public interface IProjectModelMapper:IModelMapper<ProjectEntity, ProjectListModel, ProjectDetailModel>
    {
    }
}