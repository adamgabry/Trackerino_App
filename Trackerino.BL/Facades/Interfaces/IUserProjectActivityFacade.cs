using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trackerino.BL.Models;

namespace Trackerino.BL.Facades.Interfaces
{ 
    public interface IUserProjectActivityFacade
    {
        Task SaveAsync(UserProjectActivityDetailModel model, Guid userId, Guid projectId);
        Task SaveAsync(UserProjectActivityListModel model, Guid userId, Guid projectId);
        Task DeleteAsync(Guid id);
    }
}
