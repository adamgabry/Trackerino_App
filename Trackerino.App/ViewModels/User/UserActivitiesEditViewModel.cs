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
    [QueryProperty(nameof(User), nameof(User)), QueryProperty(nameof(Project), nameof(Project))]
    public partial class UserActivitiesEditViewModel : ViewModelBase
    {
        private readonly IActivityFacade _activityFacade;
        private readonly IUserProjectActivityFacade _userProjectActivityFacade;
        private readonly IUserProjectActivityModelMapper _userProjectActivityModelMapper;

        public UserDetailModel? User { get; set; }

        public ProjectDetailModel? Project { get; set; }

        public List<ActivityTag> ActivityTags { get; set; }
        public ObservableCollection<ActivityListModel> Activities { get; set; } = new();

        public ActivityListModel? ActivitySelected { get; set; }

        public UserProjectActivityDetailModel? ActivityNew { get; private set; }

        public UserActivitiesEditViewModel(
            IActivityFacade activityFacade,
            IUserProjectActivityFacade userProjectActivityFacade,
            IUserProjectActivityModelMapper userProjectActivityModelMapper,
            IMessengerService messengerService)
            : base(messengerService)
        {
            _activityFacade = activityFacade;
            _userProjectActivityFacade = userProjectActivityFacade;
            _userProjectActivityModelMapper = userProjectActivityModelMapper;

            ActivityTags = new List<ActivityTag>((ActivityTag[])Enum.GetValues(typeof(ActivityTag)));
        }

        protected override async Task LoadDataAsync()
        {
            await base.LoadDataAsync();

            Activities.Clear();
            var ingredients = await _activityFacade.GetAsync();
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
                _userProjectActivityModelMapper.MapToExistingDetailModel(ActivityNew, ActivitySelected);

                await _userProjectActivityFacade.SaveAsync(ActivityNew, User.Id, Project.Id);
                User.Activities.Add(_userProjectActivityModelMapper.MapToListModel(ActivityNew));

                ActivityNew = GetActivityNew();

                MessengerService.Send(new UserActivitiesAddMessage());
            }
        }

        [RelayCommand]
        private async Task UpdateActivityAsync(UserProjectActivityListModel? model)
        {
            if (model is not null
                && User is not null)
            {
                await _userProjectActivityFacade.SaveAsync(model, User.Id, Project.Id);

                MessengerService.Send(new UserActivitiesEditMessage());
            }
        }

        [RelayCommand]
        private async Task RemoveActivityAsync(UserProjectActivityListModel model)
        {
            if (User is not null)
            {
                await _activityFacade.DeleteAsync(model.Id);
                User.Activities.Remove(model);

                MessengerService.Send(new UserActivitiesDeleteMessage());
            }
        }

        private UserProjectActivityDetailModel GetActivityNew()
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
