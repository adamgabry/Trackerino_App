﻿using CommunityToolkit.Mvvm.Input;
using Trackerino.App.Messages;
using Trackerino.App.Services.Interfaces;
using Trackerino.App.Services;
using Trackerino.BL.Facades.Interfaces;
using Trackerino.BL.Models;
using Trackerino.DAL.Common;

namespace Trackerino.App.ViewModels
{
    [QueryProperty(nameof(Activity), nameof(Activity))]
    public partial class ActivityEditViewModel : ViewModelBase
    {
        private readonly IActivityFacade _activityFacade;
        private readonly INavigationService _navigationService;

        public List<ActivityTag> ActivityTags { get; set; }

        public ActivityDetailModel Activity { get; init; } = ActivityDetailModel.Empty;

        public ActivityEditViewModel(
            IActivityFacade activityFacade,
            INavigationService navigationService,
            IMessengerService messengerService)
            : base(messengerService)
        {
            ActivityTags = new List<ActivityTag>((ActivityTag[])Enum.GetValues(typeof(ActivityTag)));
            _activityFacade = activityFacade;
            _navigationService = navigationService;
        }

        [RelayCommand]
        private async Task SaveAsync()
        {
            await _activityFacade.SaveAsync(Activity);

            MessengerService.Send(new ActivityEditMessage { ActivityId = Activity.Id });

            _navigationService.SendBackButtonPressed();
        }
    }
}
