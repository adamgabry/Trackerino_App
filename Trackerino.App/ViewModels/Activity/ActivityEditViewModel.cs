﻿using CommunityToolkit.Mvvm.Input;
using Trackerino.App.Messages;
using Trackerino.App.Services;
using Trackerino.BL.Facades;
using Trackerino.BL.Models;
using Trackerino.DAL.Common;

namespace Trackerino.App.ViewModels
{
    [QueryProperty(nameof(Activity), nameof(Activity))]
    public partial class ActivityEditViewModel : ViewModelBase
    {
        private readonly IProjectFacade _projectFacade;
        private readonly IActivityFacade _activityFacade;
        private readonly INavigationService _navigationService;
        private readonly IAlertService _alertService;


        public List<ActivityTag> ActivityTags { get; set; }

        public IEnumerable<ProjectListModel> Projects { get; set; } = null!;

        public IEnumerable<ActivityListModel> Activities { get; set; } = null!;

        public DateTime StartDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public DateTime EndDate { get; set; }
        public TimeSpan EndTime { get; set; }
        public Guid ProjectId { get; set; }
        public ActivityDetailModel Activity { get; init; } = ActivityDetailModel.Empty;
        
        public ActivityEditViewModel(
            IProjectFacade projectFacade,
            IActivityFacade activityFacade,
            INavigationService navigationService,
            IMessengerService messengerService, IAlertService alertService)
            : base(messengerService)
        {
            ActivityTags = new List<ActivityTag>((ActivityTag[])Enum.GetValues(typeof(ActivityTag)));
            _projectFacade = projectFacade;
            _activityFacade = activityFacade;
            _navigationService = navigationService;
            _alertService = alertService;
        }

        protected override async Task LoadDataAsync()
        {
            await base.LoadDataAsync();
            await UpdatePickers();
            ProjectId = Activity.ProjectId;
            Projects = await _projectFacade.GetAsync();

        }

        [RelayCommand]
        private Task ActivityToProjectAsync(Guid projectId)
        {
            ProjectId = projectId;
            return Task.CompletedTask;
        }
    
        [RelayCommand]
        private async Task SaveAsync()
        {
            //Get values from pickers
            Activity.ProjectId = ProjectId;
            DateTime start = StartDate.Date;
            DateTime end = EndDate.Date;
            Activity.StartDateTime = start.AddMinutes(StartTime.TotalMinutes);
            Activity.EndDateTime = end.AddMinutes(EndTime.TotalMinutes);
            if (!(Activity.StartDateTime < Activity.EndDateTime))
            {
                await _alertService.DisplayAsync("Activity can not be edited", "Start date must be before End Date");
            }
            else
            {
                //check for overlaps
                string userIdString = Preferences.Get("ActiveUser", string.Empty);
                IEnumerable<ActivityListModel> existingActivities =
                    await _activityFacade.GetFilteredByUserAsync(Guid.Parse(userIdString));
                if (!CheckActivityOverlap(existingActivities))
                {
                    await _activityFacade.SaveAsync(Activity);
                    MessengerService.Send(new ActivityEditMessage { ActivityId = Activity.Id });
                    _navigationService.SendBackButtonPressed();
                }
                else
                {
                    await _alertService.DisplayAsync("Activity can not be edited",
                        "You can do 1 activity at the same time");
                }
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
