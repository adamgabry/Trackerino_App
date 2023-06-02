using Trackerino.BL.Models;
using Trackerino.DAL.Entities;

namespace Trackerino.BL.Mappers
{
    public interface IActivityModelMapper : IModelMapper<ActivityEntity, ActivityListModel, ActivityDetailModel>
    {
    }
}