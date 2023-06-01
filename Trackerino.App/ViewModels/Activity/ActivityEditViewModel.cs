using CommunityToolkit.Mvvm.Input;
using Trackerino.App.Messages;
using Trackerino.App.Services.Interfaces;
using Trackerino.App.Services;
using Trackerino.BL.Facades.Interfaces;
using Trackerino.BL.Models;
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace Trackerino.App.ViewModels
{
    [QueryProperty(nameof(Activity), nameof(Activity))]
    public partial class ActivityEditViewModel : ViewModelBase
    {
        private readonly IActivityFacade _activityFacade;
        private readonly INavigationService _navigationService;

        public IEnumerable<ActivityListModel> Activities { get; set; } = null!;

        public DateTime StartDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public DateTime EndDate { get; set; }
        public TimeSpan EndTime { get; set; }


        public ActivityDetailModel Activity { get; init; } = ActivityDetailModel.Empty;
        
        public ActivityEditViewModel(
            IActivityFacade activityFacade,
            INavigationService navigationService,
            IMessengerService messengerService)
            : base(messengerService)
        {
            _activityFacade = activityFacade;
            _navigationService = navigationService;

        }

        protected override async Task LoadDataAsync()
        {
            await base.LoadDataAsync();
            await UpdatePickers();
        }

        [RelayCommand]
        private async Task SaveAsync()
        {
            //Get values from pickers
            DateTime start = StartDate.Date;
            DateTime end = EndDate.Date;
            Activity.StartDateTime = start.AddMinutes(StartTime.TotalMinutes);
            Activity.EndDateTime = end.AddMinutes(EndTime.TotalMinutes);
            //check for overlaps
            IEnumerable<ActivityListModel> existingActivities = await _activityFacade.GetAsync();
            if (!CheckActivityOverlap(existingActivities))
            {
                await _activityFacade.SaveAsync(Activity);
                MessengerService.Send(new ActivityEditMessage { ActivityId = Activity.Id });
                _navigationService.SendBackButtonPressed();
            }
        }
        private bool CheckActivityOverlap(IEnumerable<ActivityListModel> existingActivities)
        {
            foreach (var i in existingActivities)
            {
                if (i.Id != Activity.Id)
                {
                    if (Activity.StartDateTime>= i.StartDateTime && Activity.StartDateTime < i.EndDateTime)
                        return true;
                    if (Activity.EndDateTime > i.StartDateTime && Activity.EndDateTime <= i.EndDateTime)
                        return true;
                    if(Activity.StartDateTime <= i.StartDateTime && Activity.EndDateTime >= i.EndDateTime)
                        return true;
                }
            }
            return false;
        }
        private Task UpdatePickers()
        {
            StartDate = Activity.StartDateTime.Date;
            StartTime = Activity.StartDateTime.TimeOfDay;

            EndDate = Activity.EndDateTime.Date;
            EndTime = Activity.EndDateTime.TimeOfDay;
            return Task.CompletedTask;
        }
    }
}