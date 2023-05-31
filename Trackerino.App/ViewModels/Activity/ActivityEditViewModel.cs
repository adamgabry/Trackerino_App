using CommunityToolkit.Mvvm.Input;
using Trackerino.App.Messages;
using Trackerino.App.Services.Interfaces;
using Trackerino.App.Services;
using Trackerino.BL.Facades.Interfaces;
using Trackerino.BL.Models;
using System;

namespace Trackerino.App.ViewModels
{
    [QueryProperty(nameof(Activity), nameof(Activity))]
    public partial class ActivityEditViewModel : ViewModelBase
    {
        private readonly IActivityFacade _activityFacade;
        private readonly INavigationService _navigationService;

        //public DateTime StartDate { get; set; }
        //public TimeSpan StartTime { get; set; }
        //public DateTime EndDate { get; set; }
        //public TimeSpan EndTime { get; set; }


        public ActivityDetailModel Activity { get; init; } = ActivityDetailModel.Empty;
        
        public ActivityEditViewModel(
            IActivityFacade activityFacade,
            INavigationService navigationService,
            IMessengerService messengerService)
            : base(messengerService)
        {
            //StartDate = Activity.StartDateTime.TimeOfDay;
            //StartTime = Activity.StartDateTime.TimeOfDay;

            //EndDate = Activity.EndDateTime.Date;
            //EndTime = Activity.EndDateTime.TimeOfDay;

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