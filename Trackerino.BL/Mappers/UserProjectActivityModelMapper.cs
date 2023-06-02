using Trackerino.BL.Mappers.Interfaces;
using Trackerino.BL.Models;
using Trackerino.DAL.Entities;

namespace Trackerino.BL.Mappers
{
    public class UserProjectActivityModelMapper : ModelMapperBase<ActivityEntity, UserProjectActivityListModel, UserProjectActivityDetailModel>, IUserProjectActivityModelMapper
    {


        public override UserProjectActivityListModel MapToListModel(ActivityEntity? entity)
            => entity?.Project is null
                ? UserProjectActivityListModel.Empty
                : new UserProjectActivityListModel
                {
                    Id = entity.Id,
                    StartDateTime = entity.StartDateTime,
                    EndDateTime = entity.EndDateTime,
                    Tag = entity.Tag
                };

        public override UserProjectActivityDetailModel MapToDetailModel(ActivityEntity? entity)
            => entity?.Project is null
                ? UserProjectActivityDetailModel.Empty
                : new UserProjectActivityDetailModel
                {
                    Id = entity.Id,
                    StartDateTime = entity.StartDateTime,
                    EndDateTime = entity.EndDateTime,
                    Description = entity.Description,
                    Tag = entity.Tag

                };
        public UserProjectActivityListModel MapToListModel(UserProjectActivityDetailModel detailModel)
            => new()
            {
                Id = detailModel.Id,
                StartDateTime = detailModel.StartDateTime,
                EndDateTime = detailModel.EndDateTime,
                Tag = detailModel.Tag


            };

        public void MapToExistingDetailModel(UserProjectActivityDetailModel existingDetailModel,
            ActivityListModel activity)
        {
            existingDetailModel.Id = activity.Id;
            existingDetailModel.StartDateTime = activity.StartDateTime;
            existingDetailModel.EndDateTime = activity.EndDateTime;
            existingDetailModel.Description = activity.Description;

        }

        public override ActivityEntity MapToEntity(UserProjectActivityDetailModel model)
            => throw new NotImplementedException("This method is unsupported. Use the other overload.");


        public ActivityEntity MapToEntity(UserProjectActivityDetailModel model, Guid userId, Guid projectId)
            => new()
            {
                Id = model.Id,
                StartDateTime = model.StartDateTime,
                EndDateTime = model.EndDateTime,
                Tag = model.Tag,
                Description = model.Description,
                ProjectId = projectId,
                UserId = userId
            };

        public ActivityEntity MapToEntity(UserProjectActivityListModel model, Guid userId, Guid projectId)
            => new()
            {
                Id = model.Id,
                StartDateTime = model.StartDateTime,
                EndDateTime = model.EndDateTime,
                Tag = model.Tag,
                ProjectId = projectId,
                UserId = userId

            };
    }
}