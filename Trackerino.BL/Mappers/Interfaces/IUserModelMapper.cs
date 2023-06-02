using Trackerino.BL.Models;
using Trackerino.DAL.Entities;

namespace Trackerino.BL.Mappers
{
    public interface IUserModelMapper : IModelMapper<UserEntity, UserListModel, UserDetailModel>
    {
        
    }
}