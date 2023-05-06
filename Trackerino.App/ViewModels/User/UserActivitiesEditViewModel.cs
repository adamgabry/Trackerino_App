using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trackerino.App.Messages;
using Trackerino.App.Services.Interfaces;
using Trackerino.BL.Facades.Interfaces;
using Trackerino.BL.Mappers.Interfaces;
using Trackerino.BL.Models;
using Trackerino.DAL.Common;

namespace Trackerino.App.ViewModels
{
    [QueryProperty(nameof(User), nameof(User))]
    public partial class UserActivitiesEditViewModel : ViewModelBase
    {
        private readonly IUserFacade userFacade;
        private readonly IActivityFacade activityFacade;
        private readonly IActivityModelMapper activityModelMapper;

        public UserDetailModel? User { get; set; }

        public List<ActivityTag> ActivityTags { get; set; }
        public ObservableCollection<ActivityListModel> Activities { get; set; } = new();

        public ActivityListModel? ActivitySelected { get; set; }

        public ActivityDetailModel? ActivityNew { get; private set; }

        public UserActivitiesEditViewModel(
            IUserFacade userFacade,
            IActivityFacade activityFacade,
            IActivityModelMapper activityModelMapper,
            IMessengerService messengerService)
            : base(messengerService)
        {
            this.userFacade = userFacade;
            this.activityFacade = activityFacade;
            this.activityModelMapper = activityModelMapper;

            ActivityTags = new List<ActivityTag>((ActivityTag[])Enum.GetValues(typeof(ActivityTag)));
        }

        protected override async Task LoadDataAsync()
        {
            await base.LoadDataAsync();

            Activities.Clear();
            var ingredients = await activityFacade.GetAsync();
            foreach (var ingredient in ingredients)
            {
                Activities.Add(ingredient);
                ActivityNew = GetActivityNew();
            }
        }

        [RelayCommand]
        private async Task AddNewActivityToUserAsync()
        {
            if (ActivityNew is not null
                && ActivitySelected is not null
                && User is not null)
            {
                activityModelMapper.MapToExistingDetailModel(ActivityNew, ActivitySelected);

                await activityModelMapper.SaveAsync(ActivityNew, ActivitySelected);
                User.Activities.Add(activityModelMapper.MapToListModel(ActivityNew));

                ActivityNew = GetActivityNew();

                messengerService.Send(new UserActivitiesAddMessage());
            }
        }

        [RelayCommand]
        private async Task UpdateActivityAsync(ActivityListModel? model)
        {
            if (model is not null
                && User is not null)
            {
                await activityFacade.SaveAsync(model, User.Id);

                messengerService.Send(new UserActivitiesEditMessage());
            }
        }

        [RelayCommand]
        private async Task RemoveActivityAsync(ActivityListModel model)
        {
            if (User is not null)
            {
                await activityFacade.DeleteAsync(model.Id);
                User.Activities.Remove(model);

                messengerService.Send(new UserActivitiesDeleteMessage());
            }
        }

        private ActivityDetailModel GetActivityNew()
        {
            var activityFirst = Activities.First();
            return new()
            {
                Id = Guid.NewGuid(),
                ActivityId = activityFirst.Id,
                StartDateTime = default,
                EndDateTime = default,
                Tag = ActivityTag.None,
                Description = string.Empty
            };
        }
    }
}
