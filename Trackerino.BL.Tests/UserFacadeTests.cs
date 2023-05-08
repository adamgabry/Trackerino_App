using System.Collections.ObjectModel;
using Microsoft.EntityFrameworkCore;
using Trackerino.BL.Facades;
using Trackerino.BL.Facades.Interfaces;
using Trackerino.BL.Models;
using Trackerino.Common.Tests;
using Trackerino.Common.Tests.Seeds;

/*      UserEntityWithNoActivities,
        UserEntityWithNoProjects,
        UserEntity1,
        UserEntity2,
        UserEntity,
        UserEntityUpdate,
        UserEntityDelete);
*/

namespace Trackerino.BL.Tests;
public sealed class UserFacadeTests : FacadeTestsBase
{
    private readonly IUserFacade _userFacadeSUT;

    public UserFacadeTests() : base()
    {
        
    }
}