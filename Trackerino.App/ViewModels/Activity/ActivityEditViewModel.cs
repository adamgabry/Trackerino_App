using CommunityToolkit.Mvvm.Input;
using Trackerino.App.Messages;
using Trackerino.App.Services.Interfaces;
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
        private readonly IAlertService _alertService;


        public List<ActivityTag> ActivityTags { get; set; }

        public IEnumerable<ActivityListModel> Activities { get; set; } = null!;

        public DateTime StartDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public DateTime EndDate { get; set; }
        public TimeSpan EndTime { get; set; }


        public ActivityDetailModel Activity { get; init; } = ActivityDetailModel.Empty;
        
        public ActivityEditViewModel(
            IActivityFacade activityFacade,
            INavigationService navigationService,
            IMessengerService messengerService, IAlertService alertService)
            : base(messengerService)
        {
            ActivityTags = new List<ActivityTag>((ActivityTag[])Enum.GetValues(typeof(ActivityTag)));
            _activityFacade = activityFacade;
            _navigationService = navigationService;
            _alertService = alertService;
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
            string userIdString = Preferences.Get("ActiveUser", string.Empty);
            IEnumerable<ActivityListModel> existingActivities = await _activityFacade.GetFilteredByUserAsync(Guid.Parse(userIdString));
            if (!CheckActivityOverlap(existingActivities))
            {
                await _activityFacade.SaveAsync(Activity);
                MessengerService.Send(new ActivityEditMessage { ActivityId = Activity.Id });
                _navigationService.SendBackButtonPressed();
            }
            else
            {
                await _alertService.DisplayAsync("Activity can not be edited", "You can do 1 activity at the same time");
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
