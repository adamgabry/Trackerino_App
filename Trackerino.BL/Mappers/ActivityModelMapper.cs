using System.Collections.ObjectModel;
using Trackerino.BL.Mappers.Interfaces;
using Trackerino.BL.Models;
using Trackerino.DAL.Common;
using Trackerino.DAL.Entities;

namespace Trackerino.BL.Mappers
{
    public class ActivityModelMapper: ModelMapperBase<ActivityEntity, ActivityListModel, ActivityDetailModel>,IActivityModelMapper
    {
        private readonly IUserModelMapper _userModelMapper;
        private readonly IProjectModelMapper _projectModelMapper;

        public ActivityModelMapper(IUserModelMapper userModelMapper, IProjectModelMapper projectModelMapper)
        {
            _userModelMapper = userModelMapper;
            _projectModelMapper = projectModelMapper;
        }

        public override ActivityListModel MapToListModel(ActivityEntity? entity)
            => entity is null
                ? ActivityListModel.Empty
                : new ActivityListModel
                {
                    Id = entity.Id,
                    StartDateTime = entity.StartDateTime,
                    EndDateTime = entity.EndDateTime,
                    Tag = entity.Tag,
                    Description = entity.Description,
                };

        public override ActivityDetailModel MapToDetailModel(ActivityEntity? entity)
            => entity is null
                ? ActivityDetailModel.Empty 
                : new ActivityDetailModel
                {
                    Id = entity.Id, 
                    Description = entity.Description,
                    StartDateTime = entity.StartDateTime,
                    EndDateTime = entity.EndDateTime,
                    Tag = entity.Tag,
                    User = _userModelMapper.MapToListModel(entity.User),
                    UserId = entity.UserId,
                    Project = _projectModelMapper.MapToListModel(entity.Project),
                    ProjectId = entity.ProjectId,
                };

        public override ActivityEntity MapToEntity(ActivityDetailModel model)
            => new() { 
                Id = model.Id,
                Description = model.Description,
                StartDateTime = model.StartDateTime,
                EndDateTime = model.EndDateTime,
                Tag = model.Tag,
                ProjectId = model.ProjectId,
                UserId = model.UserId,
            };
    }
}